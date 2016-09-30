using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class CounterSignerInfo
    {
        /// <summary>
        /// Email of the recipient
        /// </summary>
        public string email { get; set; }
        /// <summary>
        ///  Security options that apply to the counter signers
        /// </summary>
        public List<WidgetSecurityOption> securityOptions { get; set; }
    }
}
