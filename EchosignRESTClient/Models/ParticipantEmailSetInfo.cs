using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class ParticipantEmailSetInfo
    {
        /// <summary>
        /// The email address of the user to whom the reminder was sent. This may either be the sender or the recipient of the document depending on the selected workflow, and on whose turn it was to sign. In the current release, the reminder is sent to that user that is currently expected to sign a given document
        /// </summary>
        public ParticipantEmailInfo[] participantEmail { get; set; }
    }
}
