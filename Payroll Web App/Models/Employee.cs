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

        //personal info
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SIN { get; set; }//social insurance num, should be private
        public string email { get; set; }
        public string phoneNumber { get; set; } //i dont really care
        public MaritialStatus maritialStatus { get; set; }
        //date of birth and address can OOP modeled?
        //i dont want to focus on adding too much info tho

        //company info
        public string Position { get; set; }
        public string department { get; set; }
        public int hourlyRate { get; set; } //this should be double nooo?
        public int annualRate { get; set; }
        

        //maybe need to add more

        public Employee()
        {

        }

        //todo: add method to get next avalable employee id


    }
}
