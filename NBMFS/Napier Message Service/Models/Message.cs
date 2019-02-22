using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Message_Service.Models
{
    public abstract class Message
    {
        public string ID { get; set; }
        public string Sender { get; set; }
        public string MessageText { get; set; }
    }
}
