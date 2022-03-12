using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll_Web_App.Models
{
    public class Employee
    {
        public int Id { get; set; }//employee id
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }

        //public string SIN { get; set; }//social insurance num, should be private


        //maybe need to add more

        public Employee()
        {

        }

    }
}
