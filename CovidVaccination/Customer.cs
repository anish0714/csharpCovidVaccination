using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CovidVaccination
{
    [XmlInclude(typeof(EighteenToFourtyFive))]
    [XmlInclude(typeof(FourtyFiveToSixty))]
    [XmlInclude(typeof(SixtyPlus))]
    public abstract class Customer : ICustomer
    {
        private string custName;
        private string custAgeGroup;
        private string custVaccine;
        private string custAadhar;
        private string custScheduleTime;
        
        public string CustName { get => custName; set => custName = value; }
        public string CustAgeGroup { get => custAgeGroup; set => custAgeGroup = value; }
        public string CustVaccine { get => custVaccine; set => custVaccine = value; }
        public string CustAadhar { get => custAadhar; set => custAadhar = value; }
        public string CustScheduleTime { get => custScheduleTime; set => custScheduleTime = value; }
        public Customer() { }
        public Customer(string custName, string custAgeGroup, string custVaccine, string custAadhar, string custScheduleTime)
        {
            this.CustName = custName;
            this.CustAgeGroup = custAgeGroup;
            this.CustVaccine = custVaccine;
            this.CustAadhar = custAadhar;
            this.CustScheduleTime = custScheduleTime;
        }

        

        public abstract string Vaccine();

        public bool ValidateName(string name)
        {
            string[] splitCustName = name.Split(' ');
            int falseCount = 0;
            if (name.Length == 0)
            {
                falseCount++;
            }
            else
            {
                for (int i = 0; i < splitCustName.Length; i++)
                {
                    char[] custNameArrChar = splitCustName[i].ToCharArray();
                    for (int j = 0; j < custNameArrChar.Length; j++)
                    {
                        if (!char.IsLetter(custNameArrChar[j]))
                        {
                            falseCount++;
                        }
                    }
                }
            }
            return (falseCount > 0 ? false : true);
        }
        //----------------------------------PASSPORT----------------------------------
        public bool ValidateAadharNumber(string aadharCard)
        {
            bool isAadharValid = false;

            if ((aadharCard != null) && (aadharCard.Length == 14))
            {
                string[] adharArr = aadharCard.Split(' ');
                char[] adharChar = aadharCard.ToCharArray();
                if (adharArr.Length == 3)
                {
                    for (int i = 0; i < adharChar.Length; i++)
                    {
                        if (char.IsDigit(adharChar[i]) || (adharChar[i] == ' '))
                        {
                            isAadharValid = true;
                        }
                        else
                        {
                            isAadharValid = false;
                            break;
                        }
                    }
                }

            }
            return isAadharValid;
        }
    }
}
