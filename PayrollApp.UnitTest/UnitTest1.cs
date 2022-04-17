using Xunit;
using Payroll_Web_App.Models;
using System;

namespace Payroll.Tests
{

    public class UnitTestPayrollCalc
    {
        [Theory]
        [InlineData(10000, 2005)] //data is yearly salary and expected tax
        [InlineData(0, 0)]
        [InlineData(60000, 13243.08)]//these are gnarly to do by hand
        //[InlineData(120000, )]
        //should add a test for each bracket as well as zero salary made
        public void TestExpectedTax (int salary, double expectedTax)
        {
            var emp = new Employee();
            emp.annualRate = salary; //create and set new employee required fields
            double calculatedTax = PayCalculator.CalcIncomeTaxDeduct(emp);//run calculator
            Assert.Equal(expectedTax, calculatedTax);
        }


        [Theory]
        [InlineData(1.1111, 1.11)]
        [InlineData (0.559, 0.56)]
        [InlineData(1000.333, 1000.33)]
        public void TestTrim(double intake, double outtake)
        {
            Assert.Equal(outtake, PayCalculator.TrimTwoDecimalPlaces(intake));
        }


        [Theory]
        [InlineData(0, PayCalculator.TaxBracketType.Federal, 49020)] 
        [InlineData(1, PayCalculator.TaxBracketType.Provincial, 90287)]
        [InlineData(2, PayCalculator.TaxBracketType.Federal, 53938)]
        [InlineData(3, PayCalculator.TaxBracketType.Provincial, 70000)]//todo test largest bracket
        [InlineData(4, PayCalculator.TaxBracketType.Federal, Int32.MaxValue - 216511)]

        public void TestFindDeduct(int index, PayCalculator.TaxBracketType type, int expected)
        {
            Assert.Equal(expected, PayCalculator.FindDeduct(index, type));
        }
    }
}