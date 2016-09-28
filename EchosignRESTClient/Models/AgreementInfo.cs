using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class AgreementInfo
    {
        /// <summary>
        /// A resource identifier that can be used to uniquely identify the agreement resource in other apis
        /// </summary>
        public string agreementId { get; set; }
        /// <summary>
        /// An ID which uniquely identifies the current version of the document
        /// </summary>
        public string latestVersionId { get; set; }
        /// <summary>
        /// The locale associated with this agreement - for example, en_US or fr_FR
        /// </summary>
        public string locale { get; set; }
        /// <summary>
        /// Information about whether the agreement can be modified
        /// </summary>
        public bool modifiable { get; set; }
        /// <summary>
        /// The name of the document, specified by the sender
        /// </summary>
        public string name { get; set; }
        /// <summary>
        ///  ['OUT_FOR_SIGNATURE' or 'WAITING_FOR_REVIEW' or 'SIGNED' or 'APPROVED' or 'ABORTED' or 'DOCUMENT_LIBRARY' or 
        ///  'WIDGET' or 'EXPIRED' or 'ARCHIVED' or 'PREFILL' or 'AUTHORING' or 'WAITING_FOR_FAXIN' or 'WAITING_FOR_VERIFICATION' 
        ///  or 'WIDGET_WAITING_FOR_VERIFICATION' or 'WAITING_FOR_PAYMENT' or 'OUT_FOR_APPROVAL' or 'OTHER']: The current status of the document
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Whether vaulting was enabled for the agreement
        /// </summary>
        public bool vaultingEnabled { get; set; }
        /// <summary>
        /// The date after which the document can no longer be signed, if an expiration date is configured. The value is nil if an expiration date is not set for the document
        /// </summary>
        public DateTime expiration { get; set; }
        /// <summary>
        /// The message associated with the document that the sender has provided
        /// </summary>
        public string message { get; set; }
        /// <summary>
        ///  ['OPEN_PROTECTED' or 'OTHER']: Security information about the document that specifies whether or not a password is required to view and sign the document
        /// </summary>
        public string[] securityOptions { get; set; }
        /// <summary>
        /// An ordered list of the events in the audit trail of this document
        /// </summary>
        public List<DocumentHistoryEvent> events { get; set; }
        /// <summary>
        /// Information about who needs to act next for this document - for example, if the agreement is in status OUT_FOR_SIGNATURE or OUT_FOR_APPROVAL, 
        /// this will be the next signer or approver. If the AgreementStatus is a terminal state, this array is empty
        /// </summary>
        public List<NextParticipantSetInfo> nextParticipantSetInfos { get; set; }
        /// <summary>
        /// Information about all the participant sets of this document
        /// </summary>
        public List<ParticipantSetInfo> participantSetInfos { get; set; }
    }
}
