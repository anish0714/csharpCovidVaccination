using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidVaccination
{
    public class EighteenToFourtyFive : Customer
    {
        public EighteenToFourtyFive() { }
        public EighteenToFourtyFive(string custName, string custAgeGroup, string custVaccine, string custAadhar, string custScheduleTime) : base(custName, custAgeGroup, custVaccine, custAadhar, custScheduleTime)
        { }
        public override string Vaccine()
        {
            return "COVIDSHIELD";
        }
    }
}
