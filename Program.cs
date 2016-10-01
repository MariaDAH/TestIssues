using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IssuesTracker
{
    class Program
    {
        static void Main(string[] args)
        {
       
            int input = 0;
            string inputN1 = "";

            do
            {
                Console.WriteLine("Simple IBAN trust method\n\t 1) IBAN Check\n\t 2) Quit\n\t ", input);
                Console.Write("Enter Selection: ");
                input = Convert.ToInt32(Console.ReadLine());

                if (input == 2)
                {
                    Console.WriteLine();
                }
                else if (input > 2)
                {
                    Console.WriteLine("Invalid Menu Selection.\t Try Again");
                }

                else
                {
                    Console.Write("Enter IBAN: ");
                    inputN1 = Valid(Console.ReadLine());

                    switch (input)
                    {
                        case 1:
                            //Console.WriteLine("\tResults: {0}", ValidateBankAccount(inputN1));
                            Console.WriteLine("\tResults: {0}", CheckIban(inputN1,true).ToString());
                            break;
                    }
                    Console.WriteLine("Press any key...");
                    Console.ReadKey();
                    Console.Clear();
                }


            }
            while (input != 2 && input < 2);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        } //End of main

        public static string Valid(string validIban)
        {
            string validEntry = "";

            bool result = string.IsNullOrEmpty(validIban);
            if (result)
            {
                validEntry = "";
            }
            else
            {
                validEntry = validIban.ToString();
            }

            return validEntry;

        }

        public static StatusData CheckIban(string iban, bool cleanText)
        {
            if (cleanText) // remove empty space & convert all uppercase
                iban = Regex.Replace(iban, @"\s", "").ToUpper();

            if (Regex.IsMatch(iban, @"\W"))
                return new StatusData(false, "The IBAN contains illegal characters.");

            if (!Regex.IsMatch(iban, @"^\D\D\d\d.+"))
                return new StatusData(false, "The structure of IBAN is wrong.");

            if (Regex.IsMatch(iban, @"^\D\D00.+|^\D\D01.+|^\D\D99.+"))
                return new StatusData(false, "The check digits of IBAN are wrong.");

            string countryCode = iban.Substring(0, 2);

            IbanData currentIbanData = (from id in IBANList()
                                        where id.CountryCode == countryCode
                                        select id).FirstOrDefault();

            if (currentIbanData == null)
                return new StatusData(false,
                  string.Format("IBAN for country {0} currently is not avaliable.",
                                countryCode));

            if (iban.Length != currentIbanData.Lenght)
                return new StatusData(false,
                  string.Format("The IBAN of {0} needs to be {1} characters long.",
                                countryCode, currentIbanData.Lenght));

            if (!Regex.IsMatch(iban.Remove(0, 4), currentIbanData.RegexStructure))
                return new StatusData(false,
                  "The country specific structure of IBAN is wrong.");

            string modifiedIban = iban.ToUpper().Substring(4) + iban.Substring(0, 4);
            modifiedIban = Regex.Replace(modifiedIban, @"\D",
                                          m => ((int)m.Value[0] - 55).ToString());

            int remainer = 0;
            while (modifiedIban.Length >= 7)
            {
                remainer = int.Parse(remainer + modifiedIban.Substring(0, 7)) % 97;
                modifiedIban = modifiedIban.Substring(7);
            }
            remainer = int.Parse(remainer + modifiedIban) % 97;

            if (remainer != 1)
                return new StatusData(false, "The IBAN is incorrect.");
               


            return new StatusData(true, "The IBAN seems to be correct.");
        }


        public static List<IbanData> IBANList()
        {
            List<IbanData> newList = new List<IbanData>();
            newList.Add(new IbanData("AD", 24,
              @"\d{8}[a-zA-Z0-9]{12}", false,
              "AD1200012030200359100100"));

            //.... other countries

            newList.Add(new IbanData("TR", 26,
              @"\d{5}[a-zA-Z0-9]{17}", false,
              "TR330006100519786457841326"));

            return newList;
        }
        
    }
}
