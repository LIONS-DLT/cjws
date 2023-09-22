using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmunitionLibrary
{
    public class AmmunitionReturnReceipt
    {
        public string Request { get; set; } = string.Empty;
        public string Issuance { get; set; } = string.Empty;
        public int RoundsReturned { get; set; }
    }
}
