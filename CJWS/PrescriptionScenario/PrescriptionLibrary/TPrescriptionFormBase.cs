using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionLibrary
{
    public class TPrescriptionFormBase
    {
        public string PrescriptionNumber { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.Now.Date;
        public bool Chargeable { get; set; }
        public bool Chargefree { get; set; }

        public bool Noctu { get; set; }
        public bool AutIdem { get; set; }

        public bool TreatmentInLabel { get; set; }
        public bool TreatmentOffLabel { get; set; }

        public double Total { get; set; }
        public double CoPayment { get; set; }

        public List<TPrescriptionItem> PrescriptionItems { get; set; } = new List<TPrescriptionItem>();

    }
}
