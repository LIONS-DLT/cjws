using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionLibrary
{
    public class TPrescriptionForm : TPrescriptionCarbonCopy
    {
        public string PatientFirstName { get; set; } = string.Empty;
        public string PatientLastName { get; set; } = string.Empty;
        public string PatientDateOfBirth { get; set; } = string.Empty;
        public string PatientInsuranceId { get; set; } = string.Empty;
        public string PatientInsuranceStatus { get; set; } = string.Empty;

        public string InsuranceName { get; set; } = string.Empty;
        public string InsuranceId { get; set; } = string.Empty;

        public string PhysiciansOfficeId { get; set; } = string.Empty;
        public string PhysiciansId { get; set; } = string.Empty;

        public TPrescriptionCarbonCopy CreateCarbonCopy()
        {
            TPrescriptionCarbonCopy copy = new TPrescriptionCarbonCopy();

            foreach(PropertyInfo property in copy.GetType().GetProperties())
            {
                object? val = this.GetType().GetProperty(property.Name)!.GetValue(this);
                property.SetValue(copy, val);
            }

            //foreach (TPrescriptionItem item in this.PrescriptionItems)
            //{
            //    copy.PrescriptionItems.Add(new TPrescriptionItem()
            //    {
            //        Factor = item.Factor,
            //        Fee = item.Fee,
            //        Product = item.Product,
            //    });
            //}

            return copy;
        }
    }
}
