using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Message_Service.Models
{
    public class SIR : Email
    {
        public string SortCode { get; set; }
        public string NatureOfIncident { get; set; }

        public SIR(string id, string sender, string subject, string sortcode, string noi, string text)
        {
            ID = id;
            Sender = sender;
            Subject = subject;
            SortCode = sortcode;
            NatureOfIncident = noi;
            MessageText = text;
        }
    }
}
