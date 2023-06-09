﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace CJWS
{
    public class CJWS1Header : ICJWSCertificateInfo, ICJWSHeader
    {
        /// <summary>
        /// The type of the document (should always be 'cjws1').
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
        /// The algorithm used to create the signature (this property is informative).
        /// </summary>
        [JsonPropertyName("alg")]
        public CJWSAlgorithm Algorithm { get; set; } = CJWSAlgorithm.RS256;

        /// <summary>
        /// The document's date as string inf the fornmat 'yyyy/MM/dd'.
        /// </summary>
        [JsonPropertyName("day")]
        public string Date { get; set; } = DateTime.Now.ToString("yyyy/MM/dd");

        /// <summary>
        /// The certificate file (x5c) as byte array.
        /// </summary>
        [JsonPropertyName("x5c")]
        public byte[] Certificate { get; set; } = new byte[0];

        /// <summary>
        /// The certificate file (crt) as URL.
        /// </summary>
        [JsonPropertyName("x5u")]
        public string CertificateUrl { get; set; } = string.Empty;

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
            return CJWS1.EncodeUrlBase64(Encoding.UTF8.GetBytes(this.ToJsonString()));
        }

        /// <summary>
        /// Deserializes a URL-encoded base64 string to a CJWS2Header object.
        /// </summary>
        public static CJWS1Header FromString(string headerString)
        {
            byte[] data = CJWS2.DecodeUrlBase64(headerString);
            string json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<CJWS1Header>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
            })!;
        }

        public byte[] ToByteArray()
        {
            return Encoding.UTF8.GetBytes(this.ToJsonString());
        }
        public static CJWS1Header FromByteArray(byte[] headerData)
        {
            string json = Encoding.UTF8.GetString(headerData);
            return JsonSerializer.Deserialize<CJWS1Header>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
            })!;
        }
    }

}
