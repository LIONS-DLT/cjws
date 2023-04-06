using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionLibrary
{
    public class TPrescriptionItem
    {
        public string Product { get; set; } = string.Empty;
        public double Factor { get; set; }
        public double Fee { get; set; }
    }
}
