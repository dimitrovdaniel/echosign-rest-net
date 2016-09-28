using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class AgreementStatusUpdateInfo
    {
        public string value { get; set; }
        public string comment { get; set; }
        public bool notifySigner { get; set; }
    }
}
