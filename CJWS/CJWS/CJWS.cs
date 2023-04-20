using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection.PortableExecutable;

namespace CJWS
{
    public abstract class CJWS
    {
        public virtual byte[] Header { get; set; } = new byte[0];

        public byte[] Payload { get; set; } = new byte[0];


        /// <summary>
        /// Returns the header as URL-encoded base64 string.
        /// </summary>
        public string GetHeaderString()
        {
            return CJWS.EncodeUrlBase64(this.Header);
        }

        /// <summary>
        /// Returns the payload as URL-encoded base64 string.
        /// </summary>
        public string GetPayloadString()
        {
            return CJWS.EncodeUrlBase64(this.Payload);
        }


        /// <summary>
        /// Helper method for deserializing payloads to an object of a specific type.
        /// </summary>
        public static T DeserializePayload<T>(byte[] payload)
        {
            return JsonSerializer.Deserialize<T>(payload, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            })!;
        }

        /// <summary>
        /// Helper method for serializing an object to a payload byte array.
        /// </summary>
        public static byte[] SerializePayload<T>(T payload)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            }));
        }

        /// <summary>
        /// Sets the payload by serializing the given objekt to Json byte array.
        /// </summary>
        public void SetPayloadObject<T>(T payload)
        {
            this.Payload = SerializePayload(payload);
        }

        /// <summary>
        /// Returns the Json-payload deserialized to the given type.
        /// </summary>
        public T GetPayloadObject<T>()
        {
            return DeserializePayload<T>(this.Payload);
        }



        /// <summary>
        /// Encodes a byte array to a URL-encoded base64 string.
        /// </summary>
        public static string EncodeUrlBase64(byte[] data)
        {
            return Convert.ToBase64String(data).Replace('/', '_').Replace('+', '-').Replace("=", "");
        }

        /// <summary>
        /// Decodes a URL-encoded base64 string to a byte array.
        /// </summary>
        public static byte[] DecodeUrlBase64(string data)
        {
            data = data.Replace('_', '/').Replace('-', '+');
            int trail = 4 - data.Length % 4;
            if (trail > 0 && trail < 4)
                data += new string('=', trail);
            return Convert.FromBase64String(data.Replace('_', '/').Replace('-', '+'));
        }

        public abstract bool Verify();

        /// <summary>
        /// Sign the document using the given certificate and hash algorithm.
        /// </summary>
        public void Sign(string certificatePath, string password, HashAlgorithmName hashAlgorithm)
        {
            using (X509Certificate2 cert = new X509Certificate2(certificatePath, password))
            {
                Sign(cert, hashAlgorithm);
            }
        }

        /// <summary>
        /// Sign the document using the given certificate and hash algorithm.
        /// </summary>
        public void Sign(byte[] certificate, string password, HashAlgorithmName hashAlgorithm)
        {
            using (X509Certificate2 cert = new X509Certificate2(certificate, password))
            {
                Sign(cert, hashAlgorithm);
            }
        }

        /// <summary>
        /// Sign the document using the given certificate and hash algorithm.
        /// </summary>
        public abstract void Sign(X509Certificate2 certificate, HashAlgorithmName hashAlgorithm);

        public abstract string Serialize();

        public static X509Certificate2 LoadCertificate(ICJWSCertificateInfo certificateInfo)
        {
            if (certificateInfo.Certificate != null && certificateInfo.Certificate.Length > 0)
            {
                return new X509Certificate2(certificateInfo.Certificate);
            }
            else if (!string.IsNullOrEmpty(certificateInfo.CertificateUrl))
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    byte[] data = httpClient.GetAsync(certificateInfo.CertificateUrl).Result.Content.ReadAsByteArrayAsync().Result;
                    return new X509Certificate2(data);
                }
            }
            else
                throw new Exception("No certificate given.");
        }

        public static CJWSHeaderInfo ExtractHeaderFromString(string cjwsString)
        {
            string header = cjwsString.Substring(0, cjwsString.IndexOf('.'));
            return CJWSHeaderInfo.FromString(header);
        }
        public static CJWSHeaderInfo ExtractHeaderFromStream(Stream stream)
        {
            string header = "";

            using (StreamReader rd = new StreamReader(stream))
            {
                while (!rd.EndOfStream)
                {
                    char c = (char)rd.Read();
                    if (c == '.')
                        break;

                    header += c;
                }
            }

            return CJWSHeaderInfo.FromString(header);
        }
        public static CJWSHeaderInfo ExtractHeaderFromFile(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                CJWSHeaderInfo header = ExtractHeaderFromStream(fs);
                fs.Close();
                fs.Dispose();

                return header;
            }
        }
    }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CJWSAlgorithm
    {
        // HMAC (HS256, HS384, HS512) not usable because the receiver would need the secret.

        // RSA
        RS256,
        RS384,
        RS512,

        //ECC
        ES256,
        ES384,
        ES512,
    }

    public interface ICJWSCertificateInfo
    {
        byte[] Certificate { get; set; }

        string CertificateUrl { get; set; } 
    }

    public interface ICJWSHeader
    {
        string Type { get; set; }

        string ContentType { get; set; }

        string DisplayText { get; set; }

        byte[] ToByteArray();

        string ToJsonString();
    }

    public class CJWSHeaderInfo
    {
        /// <summary>
        /// The type of the document (should always be 'jwd').
        /// </summary>
        [JsonPropertyName("typ")]
        public string Type { get; set; } = "cjws1";

        /// <summary>
        /// The content type of the document (json for serialized objects as payload, binary for anything else).
        /// </summary>
        [JsonPropertyName("cty")]
        public string ContentType { get; set; } = string.Empty;

        /// <summary>
        /// The display name of the document.
        /// </summary>
        [JsonPropertyName("dsp")]
        public string DisplayText { get; set; } = string.Empty;


        /// <summary>
        /// Deserializes a URL-encoded base64 string to a CJWS2Header object.
        /// </summary>
        public static CJWSHeaderInfo FromString(string headerString)
        {
            byte[] data = CJWS2.DecodeUrlBase64(headerString);
            string json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<CJWSHeaderInfo>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
            })!;
        }
    }
}