using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CovidVaccination
{
    class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //REUSING NAME VALIDATION FROM CUSTOMER OBJECT
            ICustomer cust = new SixtyPlus();
            if (!cust.ValidateName(value.ToString()))

            {
                return new ValidationResult(false, "Entered name is incorrect");

            }
            return ValidationResult.ValidResult;
            throw new NotImplementedException();
        }
    }
}
