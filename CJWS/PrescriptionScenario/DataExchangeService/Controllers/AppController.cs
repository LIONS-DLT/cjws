using Microsoft.AspNetCore.Mvc;
using System;

namespace DataExchangeService.Controllers
{
    public class AppController : Controller
    {
        private static Dictionary<string, DataMessage> _data = new Dictionary<string, DataMessage>();

        private static string[] apiKeys = new string[] { "SGVsbG8gUHJlc2NyaXB0aW9uIEV4YW1wbGUh" };

        [HttpPost]
        public IActionResult Upload(string? id, string? key)
        {
            if (string.IsNullOrEmpty(id))
            {
                if (!apiKeys.Contains(key))
                    return StatusCode(StatusCodes.Status403Forbidden);

                DataMessage msg = new DataMessage();

                int length = (int)this.Request.ContentLength!;
                byte[] data = new byte[length];
                int bytesRead = 0;

                while (bytesRead < length)
                    bytesRead += this.Request.Body.Read(data, bytesRead, length - bytesRead);

                msg.Data = data;
                msg.Status = DataMessageStatus.Uploaded;

                lock (_data)
                {
                    _data.Add(msg.ID, msg);
                }

                string url = this.Url.Action("Retrieve", "App", new { id = msg.ID }, this.Request.Scheme)!;

                return Json(new { retrieveUrl = url });
            }
            else
            {
                DataMessage? msg;
                lock (_data)
                {
                    _data.TryGetValue(id, out msg);
                }
                if (msg == null)
                    return StatusCode(StatusCodes.Status404NotFound);

                int length = (int)this.Request.ContentLength!;
                byte[] data = new byte[length];
                int bytesRead = 0;

                while (bytesRead < length)
                    bytesRead += this.Request.Body.Read(data, bytesRead, length - bytesRead);

                msg.Data = data;
                msg.Status = DataMessageStatus.Uploaded;

                string url = this.Url.Action("Retrieve", "App", new { id = msg.ID }, this.Request.Scheme)!;

                return Json(new { retrieveUrl = url });
            }
        }

        [HttpGet]
        public IActionResult Register(string key)
        {
            if (!apiKeys.Contains(key))
                return StatusCode(StatusCodes.Status403Forbidden);

            DataMessage msg = new DataMessage();
            msg.Status = DataMessageStatus.Registered;

            lock (_data)
            {
                _data.Add(msg.ID, msg);
            }

            string uploadUrl = this.Url.Action("Upload", "App", new { id = msg.ID }, this.Request.Scheme)!;
            string retrieveUrl = this.Url.Action("Retrieve", "App", new { id = msg.ID }, this.Request.Scheme)!;

            return Json(new { retrieveUrl = retrieveUrl, uploadUrl = uploadUrl });
        }

        [HttpGet]
        public IActionResult Retrieve(string id)
        {
            DataMessage? msg;
            lock (_data)
            {
                _data.TryGetValue(id, out msg);
            }

            if (msg == null)
                return StatusCode(StatusCodes.Status404NotFound);

            if(msg.Status == DataMessageStatus.Registered)
                return StatusCode(StatusCodes.Status404NotFound);

            byte[] data = msg.Data;
            lock (_data)
            {
                _data.Remove(id);
            }

            return File(data, "application/octet-stream");
        }
    }

    public class DataMessage
    {
        public string ID {  get; set; } = GenerateRandomUniqueID();

        public byte[] Data { get; set; } = new byte[0];
        public DataMessageStatus Status { get; set; } = DataMessageStatus.Registered;
        public DateTime Expires { get; set; } = DateTime.Now.AddMinutes(10);


        private static Random random = new Random();
        public static string GenerateRandomUniqueID()
        {
            byte[] guidPart = Guid.NewGuid().ToByteArray();
            byte[] idBytes = new byte[guidPart.Length + 8];
            for (int i = 0; i < guidPart.Length; i++)
            {
                idBytes[i] = guidPart[i];
            }
            for (int i = 0; i < 8; i++)
            {
                idBytes[guidPart.Length + i] = (byte)random.Next(0, 256);
            }
            return Convert.ToBase64String(idBytes).Replace('+', '-').Replace('/', '_').Replace("=", "");
        }

    }
    public enum DataMessageStatus
    {
        Registered,
        Uploaded,
    }
}
