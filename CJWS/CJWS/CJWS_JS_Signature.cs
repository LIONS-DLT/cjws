using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CJWS
{
    public class CJWS_JS_Signature
    {
        [JsonPropertyName("protected")]
        public byte[] Header { get; set; } = new byte[0];
        public byte[] Signature { get; set; } = new byte[0];

        public CJWS_JS_Signature_Header HeaderObject
        {
            get { return CJWS_JS_Signature_Header.FromByteArray(this.Header); }
            set { this.Header = value.ToByteArray(); }
        }


        public bool Verify(byte[] payload)
        {
            var header = this.HeaderObject;

            byte[] dataToVerify = Encoding.UTF8.GetBytes(CJWS.EncodeUrlBase64(header.ToByteArray()) + "." + CJWS.EncodeUrlBase64(payload));

            using (X509Certificate2 cert = CJWS.LoadCertificate(this.HeaderObject))
            {
                if (!cert.Verify())
                    return false;
                if (header.Algorithm == CJWSAlgorithm.RS256)
                {
                    return cert.GetRSAPublicKey()!.VerifyData(dataToVerify, this.Signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
                else if (header.Algorithm == CJWSAlgorithm.RS384)
                {
                    return cert.GetRSAPublicKey()!.VerifyData(dataToVerify, this.Signature, HashAlgorithmName.SHA384, RSASignaturePadding.Pkcs1);
                }
                else if (header.Algorithm == CJWSAlgorithm.RS512)
                {
                    return cert.GetRSAPublicKey()!.VerifyData(dataToVerify, this.Signature, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
                }
                else if (header.Algorithm == CJWSAlgorithm.ES256)
                {
                    return cert.GetECDsaPublicKey()!.VerifyData(dataToVerify, this.Signature, HashAlgorithmName.SHA256);
                }
                else if (header.Algorithm == CJWSAlgorithm.ES384)
                {
                    return cert.GetECDsaPublicKey()!.VerifyData(dataToVerify, this.Signature, HashAlgorithmName.SHA384);
                }
                else if (header.Algorithm == CJWSAlgorithm.ES512)
                {
                    return cert.GetECDsaPublicKey()!.VerifyData(dataToVerify, this.Signature, HashAlgorithmName.SHA512);
                }
            }
            return false;
        }
    }
}
