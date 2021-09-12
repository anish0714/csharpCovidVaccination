using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace CovidVaccination
{
    class AadharCardValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //REUSING NAME VALIDATION FROM CUSTOMER OBJECT
            ICustomer cust = new SixtyPlus();
            if (!cust.ValidateAadharNumber(value.ToString()))
            {
                return new ValidationResult(false, "Entered aadhar number is incorrect");
            }
            return ValidationResult.ValidResult;
           
        }
    }
}
