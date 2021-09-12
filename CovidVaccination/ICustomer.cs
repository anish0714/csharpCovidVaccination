using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidVaccination
{
    interface ICustomer
    {
        string CustName { get; set; }
        string CustAgeGroup { get; set; }
        string CustVaccine { get; set; }
        string CustAadhar { get; set; }

        bool ValidateName(string name);
        bool ValidateAadharNumber(string aadharNumber);
        string Vaccine();
    }
}
