using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesTracker
{
    public class IbanData
    {
        public string CountryCode;
        public int Lenght;
        public string RegexStructure;
        public bool IsEU924;
        public string Sample;

       
    public IbanData(string countryCode, int lenght, string regex, bool iseu, string sample)
    {
        this.CountryCode = countryCode;
        this.Lenght = lenght;
        this.RegexStructure = regex;
        this.IsEU924 = iseu;
        this.Sample = sample;
    }


    }



}
