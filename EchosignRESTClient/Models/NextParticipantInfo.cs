using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class NextParticipantInfo
    {
        /// <summary>
        /// The email address of the next participant
        /// </summary>
        public string email { get; set; }
        /// <summary>
        ///  The date since which the document has been waiting for the participant to take action
        /// </summary>
        public DateTime waitingSince { get; set; }
        /// <summary>
        /// The name of the next participant, if available
        /// </summary>
        public string name { get; set; }
    }
}
