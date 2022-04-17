using System.Collections.Generic;
using System;
using System.Linq;

namespace Payroll_Web_App.Models
{
    //thread safe implementation of a singleton
    public sealed class PayCalculator
    {
        private static PayCalculator instance = null;
        private static readonly object padlock = new object();

        PayCalculator()
        {
        }

        public static PayCalculator Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PayCalculator();
                    }
                    return instance;
                }
            }
        }

        //vars here
        public enum TaxBracketType
        {
            Federal,
            Provincial
        }

        //got this data from wealthsimple
        //federal tax bracket info
        static readonly Dictionary<int, double> FedTaxBrackets = new Dictionary<int, double>
        {
            {49020, 0.15},
            {98040, 0.205},
            {151978, 0.26},
            {216511, 0.29},
            {Int32.MaxValue, 0.33} //this is anything above 216511
        };

        //ontario/provinacial bracket info
        public static readonly Dictionary<int, double> ProvTaxBrackets = new Dictionary<int, double>
        {
            {45142, 0.0505},
            {90287, 0.0915},
            {150000, 0.1116},
            {220000, 0.1216},
            {Int32.MaxValue, 0.1316} //this is anything above 220000
        };

        //methods here

        
        //this just looks like garbage ngl
        //public static double CalcEmployableIncome(Employee employee, double vacPayRate = 0.00, bool addVacationPay = false)
        //{
        //    double gross = employee.hourlyRate * 40 * 52;//standard 40 hrs x 52 weeks, consider making this changable
        //    double vacPay = gross * vacPayRate;
        //    return gross + vacPay;
        //}

        //todo: vacpay calculator
        //add vacation pay is 6% for 5 yrs and 4% for less years
        //todo: add employed duration here or from the employee model


        //Gets total tax deducted from Federal and Provincial tax brackets
        public static double CalcIncomeTaxDeduct(Employee employee)
        {
            return CalcIncomeTaxSingle(employee.annualRate, TaxBracketType.Federal) +
                CalcIncomeTaxSingle(employee.annualRate, TaxBracketType.Provincial);
        }


        //get income tax of either provincial or federal bracket. Called twice by CalcIncomeTaxDeduct
        public static double CalcIncomeTaxSingle(double totalSalary, TaxBracketType taxType)
        {
            Dictionary<int, double> rates = SetRates(taxType);

            double final = 0;
            int index = 0;
            double currentBracket = 0;

            while (totalSalary > 0)
            {
                //i wanna refactor the if else into a funct/3 funcs
                if (totalSalary > rates.ElementAt(index).Key) //if we exceed the bracket we calc only the whole bracket
                {
                    currentBracket = rates.ElementAt(index).Key * rates.ElementAt(index).Value;
                }
                else //dont exceed bracket so calculate with remaining
                {
                    currentBracket = totalSalary * rates.ElementAt(index).Value;
                }

                totalSalary -= FindDeduct(index, taxType);
                final += currentBracket;

                index++;
                Console.WriteLine(index);
            }
            return TrimTwoDecimalPlaces(final);
        }

        //I guess I can't unit test this
        //Helper function to get the respective rates dict with a taxtype enum
        public static Dictionary<int, double> SetRates (TaxBracketType taxType)//wtf this doesnt do anything????
        {
            if (taxType == TaxBracketType.Federal)
                return FedTaxBrackets;
            return ProvTaxBrackets;
        }

        /// <summary>
        /// After calculating the tax for a bracket, this finds the width of the bracket calculated, 
        /// This width is then used to find the remaining amount of tax to be calculated in the following brackets
        /// </summary>
        /// <param name="index"></param>
        /// <param name="taxType"></param>
        /// <returns></returns>
        public static double FindDeduct(int index, TaxBracketType taxType)
        {
            Dictionary<int, double> rates = SetRates(taxType);
            if (index < 2) return rates.ElementAt(index).Key; //for 0 and 1 tiers nothing funny
            return rates.ElementAt(index).Key - rates.ElementAt(index - 1).Key;//otherwise find width between current and last entry
        }


        public static double TrimTwoDecimalPlaces(double value)
        {
            return Math.Round(value, 2);
        }
    }
}
