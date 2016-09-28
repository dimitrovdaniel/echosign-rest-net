using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class AgreementCreationResponse
    {
        /// <summary>
        /// The unique identifier that can be used to query status and download signed documents
        /// </summary>
        public string agreementId { get; set; }
        /// <summary>
        /// Javascript snippet suitable for an embedded page taking a user to a URL
        /// </summary>
        public string embeddedCode { get; set; }
        /// <summary>
        /// Expiration date for autologin. This is based on the user setting, API_AUTO_LOGIN_LIFETIME
        /// </summary>
        public DateTime expiration { get; set; }
        /// <summary>
        ///  Standalone URL to direct end users to
        /// </summary>
        public string url { get; set; }
    }
}
