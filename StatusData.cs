using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesTracker
{
    public class StatusData
    {
        public bool IsValid;
        public string Message;

        public StatusData(bool valid, string message)
        {
            this.IsValid = valid;
            this.Message = message;
        }

        public override string ToString()
        {
            return base.ToString() + ": " + this.Message.ToString();
        }


    }
}
