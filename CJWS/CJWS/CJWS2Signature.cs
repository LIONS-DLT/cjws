using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CJWS
{
    public class CJWS2Signature : ICJWSCertificateInfo
    {
        /// <summary>
        /// The algorithm used to create the signature (this property is informative).
        /// </summary>
        [JsonPropertyName("alg")]
        public CJWSAlgorithm Algorithm { get; set; } = CJWSAlgorithm.RS256;

        /// <summary>
        /// The certificate file (crt) as byte array.
        /// </summary>
        [JsonPropertyName("x5c")]
        public byte[] Certificate { get; set; } = new byte[0];

        /// <summary>
        /// The certificate file (crt) as URL.
        /// </summary>
        [JsonPropertyName("x5u")]
        public string CertificateUrl { get; set; } = string.Empty;

        /// <summary>
        /// The signature date as string inf the fornmat 'yyyy/MM/dd'.
        /// </summary>
        [JsonPropertyName("day")]
        public string Date { get; set; } = DateTime.Now.ToString("yyyy/MM/dd");

        /// <summary>
        /// The digital signature as byte array.
        /// </summary>
        [JsonPropertyName("sig")]
        public byte[] Signature { get; set; } = new byte[0];

        /// <summary>
        /// Serializes the CJWS2Signature object to a Json string.
        /// </summary>
        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            });
        }

        /// <summary>
        /// Serializes the CJWS2Signature object to a URL-encoded base64 string.
        /// </summary>
        public override string ToString()
        {
            return CJWS2.EncodeUrlBase64(Encoding.UTF8.GetBytes(this.ToJsonString()));
        }

        /// <summary>
        /// Deserializes a URL-encoded base64 string to a CJWS2Signature object.
        /// </summary>
        public static CJWS2Signature FromString(string headerString)
        {
            byte[] data = CJWS2.DecodeUrlBase64(headerString);
            string json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<CJWS2Signature>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            })!;
        }

        /// <summary>
        /// Verifies the certificate by its X.509 chain and the signature for the given payload.
        /// </summary>
        public bool Verify(byte[] payload, bool validateCertificate = true)
        {
            byte[] dataToVerify = Encoding.UTF8.GetBytes(CJWS.EncodeUrlBase64(payload) + "." + this.Date);

            using (X509Certificate2 cert = CJWS.LoadCertificate(this))
            {
                if(validateCertificate && !cert.Verify())
                    return false;
                if (this.Algorithm == CJWSAlgorithm.RS256)
                {
                    return cert.GetRSAPublicKey()!.VerifyData(dataToVerify, this.Signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
                else if (this.Algorithm == CJWSAlgorithm.RS384)
                {
                    return cert.GetRSAPublicKey()!.VerifyData(dataToVerify, this.Signature, HashAlgorithmName.SHA384, RSASignaturePadding.Pkcs1);
                }
                else if (this.Algorithm == CJWSAlgorithm.RS512)
                {
                    return cert.GetRSAPublicKey()!.VerifyData(dataToVerify, this.Signature, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
                }
                else if (this.Algorithm == CJWSAlgorithm.ES256)
                {
                    return cert.GetECDsaPublicKey()!.VerifyData(dataToVerify, this.Signature, HashAlgorithmName.SHA256);
                }
                else if (this.Algorithm == CJWSAlgorithm.ES384)
                {
                    return cert.GetECDsaPublicKey()!.VerifyData(dataToVerify, this.Signature, HashAlgorithmName.SHA384);
                }
                else if (this.Algorithm == CJWSAlgorithm.ES512)
                {
                    return cert.GetECDsaPublicKey()!.VerifyData(dataToVerify, this.Signature, HashAlgorithmName.SHA512);
                }
            }
            return false;
        }
    }



}
