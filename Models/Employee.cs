using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll_Web_App.Models
{
    public enum MaritialStatus
    {
        Single,
        Married
            //could add comomon in low and some other stuff,
            //also consider making a dict
    }

    public class Employee
    {
        public int Id { get; set; }//employee id
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }

        public string SIN { get; set; }//social insurance num, should be private
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public int MyProperty { get; set; }

        //date of birth and address can OOP modeled?
        public int hourlyRate { get; set; }
        public int annualRate { get; set; }
        public MaritialStatus maritialStatus { get; set; }

        //maybe need to add more

        public Employee()
        {

        }

    }
}
