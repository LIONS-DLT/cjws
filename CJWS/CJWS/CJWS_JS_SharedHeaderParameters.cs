using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CJWS
{
    public class CJWS_JS_SharedHeaderParameters : ICJWSHeader
    {
        /// <summary>
        /// The type of the document (should always be 'cjws-js').
        /// </summary>
        [JsonPropertyName("typ")]
        public string Type { get; set; } = "cjws-js";

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
        /// Serializes the CJWS_JS_SharedHeaderParameters object to a Json string.
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
        /// Serializes the CJWS_JS_SharedHeaderParameters object to a URL-encoded base64 string.
        /// </summary>
        public override string ToString()
        {
            return CJWS.EncodeUrlBase64(Encoding.UTF8.GetBytes(this.ToJsonString()));
        }

        /// <summary>
        /// Deserializes a URL-encoded base64 string to a CJWS_JS_SharedHeaderParameters object.
        /// </summary>
        public static CJWS_JS_SharedHeaderParameters FromString(string headerString)
        {
            byte[] data = CJWS.DecodeUrlBase64(headerString);
            string json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<CJWS_JS_SharedHeaderParameters>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
            })!;
        }


        public byte[] ToByteArray()
        {
            return Encoding.UTF8.GetBytes(this.ToJsonString());
        }
        public static CJWS_JS_SharedHeaderParameters FromByteArray(byte[] headerData)
        {
            string json = Encoding.UTF8.GetString(headerData);
            return JsonSerializer.Deserialize<CJWS_JS_SharedHeaderParameters>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
            })!;
        }
    }
}
