using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    /// <summary>
    /// Information about the alternate participant
    /// </summary>
    public class AlternateParticipantInfo
    {
        /// <summary>
        /// The email of the alternate participant. This is required if fax is not provided. Both fax and email can not be provided
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// The private message for the alternate participant
        /// </summary>
        public string privateMessage { get; set; }
        /// <summary>
        /// The country code for the alternate participant
        /// </summary>
        public string countryCode { get; set; }
        /// <summary>
        /// The phone number for the alternate participant
        /// </summary>
        public string phone { get; set; }
    }
}
