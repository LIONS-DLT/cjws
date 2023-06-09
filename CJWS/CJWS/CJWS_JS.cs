﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CJWS
{
    /// <summary>
    /// Class for the serialization/deserialization of a JWS in JSON serialization using the CJWS ruleset.
    /// This format is fully compatible with the JWS specification.
    /// </summary>
    public class CJWS_JS : CJWS
    {
        /// <summary>
        /// Should be ignored, since JWS-JS doesn't have a top level header.
        /// </summary>
        [JsonIgnore]
        public override byte[] Header { get => base.Header; set => base.Header = value; }

        /// <summary>
        /// Shared header parameters, that are added to the signature headers when using the 'Sign' method.
        /// </summary>
        [JsonIgnore]
        public CJWS_JS_SharedHeaderParameters SharedHeaderParameters { get; set; } = new CJWS_JS_SharedHeaderParameters();

        /// <summary>
        /// The signatures of this object
        /// </summary>
        public List<CJWS_JS_Signature> Signatures { get; set; } = new List<CJWS_JS_Signature>();


        public CJWS_JS()
        {
            this.SharedHeaderParameters = new CJWS_JS_SharedHeaderParameters();
        }

        public CJWS_JS(CJWS_JS_SharedHeaderParameters header)
        {
            this.SharedHeaderParameters = header;
        }

        public CJWS_JS(byte[] payload, string contentType)
        {
            this.Payload = payload;
            this.SharedHeaderParameters = new CJWS_JS_SharedHeaderParameters() { ContentType = contentType };
        }


        /// <summary>
        /// Serializes a CJWS_JS to a JSON string.
        /// </summary>
        public override string Serialize()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
            });
        }

        /// <summary>
        /// Deserializes a CJWS_JS from a JSON string.
        /// </summary>
        public static CJWS_JS Deserialize(string jsonString)
        {
            CJWS_JS result = JsonSerializer.Deserialize<CJWS_JS>(jsonString, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
            })!;

            CJWS_JS_Signature? signature = result.Signatures.FirstOrDefault();

            if(signature != null)
            {
                var header = signature.HeaderObject;
                result.SharedHeaderParameters.Type = header.Type;
                result.SharedHeaderParameters.ContentType = header.ContentType;
                result.SharedHeaderParameters.DisplayText = header.DisplayText;
            }

            return result;
        }


        /// <summary>
        /// Sign the document using the given certificate and hash algorithm.
        /// </summary>
        public override void Sign(X509Certificate2 certificate, HashAlgorithmName hashAlgorithm)
        {
            CJWS_JS_Signature signature = new CJWS_JS_Signature();

            ECDsa? key_ecdsa = certificate.GetECDsaPrivateKey();
            RSA? key_rsa = certificate.GetRSAPrivateKey();

            CJWS_JS_Signature_Header header = new CJWS_JS_Signature_Header();
            header.Type = this.SharedHeaderParameters.Type;
            header.Type = this.SharedHeaderParameters.ContentType;
            header.Type = this.SharedHeaderParameters.DisplayText;

            if (key_rsa != null)
            {
                if (hashAlgorithm == HashAlgorithmName.SHA256)
                    header.Algorithm = CJWSAlgorithm.RS256;
                else if (hashAlgorithm == HashAlgorithmName.SHA384)
                    header.Algorithm = CJWSAlgorithm.RS384;
                else if (hashAlgorithm == HashAlgorithmName.SHA512)
                    header.Algorithm = CJWSAlgorithm.RS512;
                else
                    throw new NotSupportedException("hash algorithm not supported.");
            }
            else if (key_ecdsa != null)
            {
                if (hashAlgorithm == HashAlgorithmName.SHA256)
                    header.Algorithm = CJWSAlgorithm.ES256;
                else if (hashAlgorithm == HashAlgorithmName.SHA384)
                    header.Algorithm = CJWSAlgorithm.ES384;
                else if (hashAlgorithm == HashAlgorithmName.SHA512)
                    header.Algorithm = CJWSAlgorithm.ES512;
                else
                    throw new NotSupportedException("hash algorithm not supported.");
            }
            else
                throw new NotSupportedException("signature algorithm not supported.");

            header.Certificate = certificate.Export(X509ContentType.Cert);
            signature.HeaderObject = header;

            byte[] dataToSign = Encoding.UTF8.GetBytes(CJWS.EncodeUrlBase64(header.ToByteArray()) + "." + this.GetPayloadString());

            if (key_rsa != null)
            {
                signature.Signature = key_rsa.SignData(dataToSign, hashAlgorithm, RSASignaturePadding.Pkcs1);
            }
            else if (key_ecdsa != null)
            {
                signature.Signature = key_ecdsa.SignData(dataToSign, hashAlgorithm);
            }
            else
                throw new NotSupportedException("algorithm not supported.");

            this.Signatures.Add(signature);
        }

        /// <summary>
        /// Verifies all certificates by its X.509 chain and all signatures for the given payload.
        /// </summary>
        public override bool Verify()
        {
            if (this.Signatures.Count == 0)
                return false;

            foreach (var signature in this.Signatures)
            {
                if (!signature.Verify(this.Payload))
                    return false;
            }

            return true;
        }
    }
}
