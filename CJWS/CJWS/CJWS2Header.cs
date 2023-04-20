using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CJWS
{
    public class CJWS2Header : ICJWSHeader
    {
        /// <summary>
        /// The type of the document (should always be 'cjws2').
        /// </summary>
        [JsonPropertyName("typ")]
        public string Type { get; set; } = "cjws2";

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
        /// Serializes the CJWS2Header object to a Json string.
        /// </summary>
        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
            });
        }

        /// <summary>
        /// Serializes the CJWS2Header object to a URL-encoded base64 string.
        /// </summary>
        public override string ToString()
        {
            return CJWS2.EncodeUrlBase64(Encoding.UTF8.GetBytes(this.ToJsonString()));
        }

        /// <summary>
        /// Deserializes a URL-encoded base64 string to a CJWS2Header object.
        /// </summary>
        public static CJWS2Header FromString(string headerString)
        {
            byte[] data = CJWS2.DecodeUrlBase64(headerString);
            string json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<CJWS2Header>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
            })!;
        }


        public byte[] ToByteArray()
        {
            return Encoding.UTF8.GetBytes(this.ToJsonString());
        }
        public static CJWS2Header FromByteArray(byte[] headerData)
        {
            string json = Encoding.UTF8.GetString(headerData);
            return JsonSerializer.Deserialize<CJWS2Header>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
            })!;
        }
    }
}
