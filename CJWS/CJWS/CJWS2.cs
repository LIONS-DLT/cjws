using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace CJWS
{
    /// <summary>
    /// Class for the serialization/deserialization of an extended JWS in compact serialization using the CJWS ruleset for multiple signatures.
    /// This format is NOT compatible with the JWS specification.
    /// </summary>
    public class CJWS2 : CJWS
    {
        public CJWS2() 
        { 
            this.HeaderObject = new CJWS2Header(); 
        }

        public CJWS2(CJWS2Header header)
        {
            this.HeaderObject = header;
        }

        public CJWS2(byte[] payload, string contentType)
        {
            this.Payload = payload;
            this.HeaderObject = new CJWS2Header() { ContentType = contentType };
        }

        public CJWS2Header HeaderObject
        {
            get { return CJWS2Header.FromByteArray(this.Header); }
            set { this.Header = value.ToByteArray(); }
        }

        public List<CJWS2Signature> Signatures { get; private set; } = new List<CJWS2Signature>();

        /// <summary>
        /// Sign the document using the given certificate and hash algorithm.
        /// </summary>
        public override void Sign(X509Certificate2 certificate, HashAlgorithmName hashAlgorithm)
        {
            CJWS2Signature signature = new CJWS2Signature();

            ECDsa? key_ecdsa = certificate.GetECDsaPrivateKey();
            RSA? key_rsa = certificate.GetRSAPrivateKey();

            if (key_rsa != null)
            {
                if (hashAlgorithm == HashAlgorithmName.SHA256)
                    signature.Algorithm = CJWSAlgorithm.RS256;
                else if (hashAlgorithm == HashAlgorithmName.SHA384)
                    signature.Algorithm = CJWSAlgorithm.RS384;
                else if (hashAlgorithm == HashAlgorithmName.SHA512)
                    signature.Algorithm = CJWSAlgorithm.RS512;
                else
                    throw new NotSupportedException("hash algorithm not supported.");
            }
            else if (key_ecdsa != null)
            {
                if (hashAlgorithm == HashAlgorithmName.SHA256)
                    signature.Algorithm = CJWSAlgorithm.ES256;
                else if (hashAlgorithm == HashAlgorithmName.SHA384)
                    signature.Algorithm = CJWSAlgorithm.ES384;
                else if (hashAlgorithm == HashAlgorithmName.SHA512)
                    signature.Algorithm = CJWSAlgorithm.ES512;
                else
                    throw new NotSupportedException("hash algorithm not supported.");
            }
            else
                throw new NotSupportedException("signature algorithm not supported.");

            signature.Certificate = certificate.Export(X509ContentType.Cert);

            byte[] dataToSign = Encoding.UTF8.GetBytes(this.GetPayloadString() + "." + signature.Date);

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
        /// Serializes a CJWS2 to a CJWS string.
        /// </summary>
        public override string Serialize()
        {
            string result = this.GetHeaderString() + "." + this.GetPayloadString();
            foreach (CJWS2Signature signature in this.Signatures)
            {
                result += "." + signature.ToString();
            }

            return result;
        }

        /// <summary>
        /// Deserializes a CJWS2 from a CJWS string.
        /// </summary>
        public static CJWS2 Deserialize(string jwdString)
        {
            string[] parts = jwdString.Split('.');
            CJWS2 result = new CJWS2();
            result.Header = DecodeUrlBase64(parts[0]);
            result.Payload = DecodeUrlBase64(parts[1]);

            for (int i = 2; i < parts.Length; i++)
            {
                result.Signatures.Add(CJWS2Signature.FromString(parts[i]));
            }

            return result;
        }


        /// <summary>
        /// Verifies all certificates by its X.509 chain and all signatures for the given payload.
        /// </summary>
        public override bool Verify()
        {
            if (this.Signatures.Count == 0)
                return false;

            foreach (CJWS2Signature signature in this.Signatures)
            {
                if (!signature.Verify(this.Payload))
                    return false;
            }

            return true;
        }

    }

}
