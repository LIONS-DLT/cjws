using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;

namespace CJWS
{
    /// <summary>
    /// Class for the serialization/deserialization of a JWS in compact serialization using the CJWS ruleset for single signatures.
    /// This format is fully compatible with the JWS specification.
    /// </summary>
    public class CJWS1 : CJWS
    {
        public CJWS1Header HeaderObject
        {
            get { return CJWS1Header.FromByteArray(this.Header); }
            set { this.Header = value.ToByteArray(); }
        }

        public CJWS1()
        {
            this.HeaderObject = new CJWS1Header();
        }

        public CJWS1(CJWS1Header header)
        {
            this.HeaderObject = header;
        }


        public CJWS1(byte[] payload, string contentType)
        {
            this.Payload = payload;
            this.HeaderObject = new CJWS1Header() { ContentType = contentType };
        }

        /// <summary>
        /// The digital signature as byte array.
        /// </summary>
        public byte[] Signature { get; set; } = new byte[0];

        /// <summary>
        /// Returns the payload as URL-encoded base64 string.
        /// </summary>
        public string GetSignatureString()
        {
            return CJWS2.EncodeUrlBase64(this.Signature);
        }

        /// <summary>
        /// Sign the document using the given certificate and hash algorithm.
        /// </summary>
        public override void Sign(X509Certificate2 certificate, HashAlgorithmName hashAlgorithm)
        {
            ECDsa? key_ecdsa = certificate.GetECDsaPrivateKey();
            RSA? key_rsa = certificate.GetRSAPrivateKey();

            CJWS1Header header = this.HeaderObject;

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

            this.HeaderObject = header;

            byte[] dataToSign = Encoding.ASCII.GetBytes(this.GetHeaderString() + "." + this.GetPayloadString());

            if (key_rsa != null)
            {
                this.Signature = key_rsa.SignData(dataToSign, hashAlgorithm, RSASignaturePadding.Pkcs1);
            }
            else if (key_ecdsa != null)
            {
                this.Signature = key_ecdsa.SignData(dataToSign, hashAlgorithm);
            }
            else
                throw new NotSupportedException("algorithm not supported.");
        }

        /// <summary>
        /// Serializes a CJWS2 to a CJWS string.
        /// </summary>
        public override string Serialize()
        {
            return this.GetHeaderString() + "." + this.GetPayloadString() + "." + this.GetSignatureString();
        }

        /// <summary>
        /// Deserializes a CJWS2 from a CJWS string.
        /// </summary>
        public static CJWS1 Deserialize(string jwdString)
        {
            string[] parts = jwdString.Split('.');
            CJWS1 result = new CJWS1();
            result.Header = DecodeUrlBase64(parts[0]);
            result.Payload = DecodeUrlBase64(parts[1]);
            result.Signature = DecodeUrlBase64(parts[2]);

            return result;
        }

        /// <summary>
        /// Verifies the certificate by its X.509 chain and the signature for the given heafer and payload.
        /// </summary>
        public override bool Verify()
        {
            CJWS1Header header = this.HeaderObject;
            byte[] dataToValidate = Encoding.ASCII.GetBytes(this.GetHeaderString() + "." + this.GetPayloadString());

            using (X509Certificate2 cert = CJWS.LoadCertificate(header))
            {
                if (!cert.Verify())
                    return false;
                if (header.Algorithm == CJWSAlgorithm.RS256)
                {
                    return cert.GetRSAPublicKey()!.VerifyData(dataToValidate, this.Signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
                else if (header.Algorithm == CJWSAlgorithm.RS384)
                {
                    return cert.GetRSAPublicKey()!.VerifyData(dataToValidate, this.Signature, HashAlgorithmName.SHA384, RSASignaturePadding.Pkcs1);
                }
                else if (header.Algorithm == CJWSAlgorithm.RS512)
                {
                    return cert.GetRSAPublicKey()!.VerifyData(dataToValidate, this.Signature, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
                }
                else if (header.Algorithm == CJWSAlgorithm.ES256)
                {
                    return cert.GetECDsaPublicKey()!.VerifyData(dataToValidate, this.Signature, HashAlgorithmName.SHA256);
                }
                else if (header.Algorithm == CJWSAlgorithm.ES384)
                {
                    return cert.GetECDsaPublicKey()!.VerifyData(dataToValidate, this.Signature, HashAlgorithmName.SHA384);
                }
                else if (header.Algorithm == CJWSAlgorithm.ES512)
                {
                    return cert.GetECDsaPublicKey()!.VerifyData(dataToValidate, this.Signature, HashAlgorithmName.SHA512);
                }
            }
            return false;
        }

    }
}
