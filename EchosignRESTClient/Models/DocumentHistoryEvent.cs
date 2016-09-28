using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class DocumentHistoryEvent
    {
        /// <summary>
        /// Email address of the user that initiated the event
        /// </summary>
        public string actingUserEmail { get; set; }
        /// <summary>
        /// The IP address of the user that initiated the event
        /// </summary>
        public string actingUserIpAddress { get; set; }
        /// <summary>
        /// The date of the audit event
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// A description of the audit event
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Email address of the user that initiated the event
        /// </summary>
        public string participantEmail { get; set; }
        /// <summary>
        ///  ['CREATED' or 'UPLOADED_BY_SENDER' or 'FAXED_BY_SENDER' or 'AGREEMENT_MODIFIED' or 'USER_ACK_AGREEMENT_MODIFIED' or 
        ///  'PRESIGNED' or 'SIGNED' or 'ESIGNED' or 'DIGSIGNED' or 'APPROVED' or 'OFFLINE_SYNC' or 'FAXIN_RECEIVED' or 'SIGNATURE_REQUESTED'
        ///  or 'APPROVAL_REQUESTED' or 'RECALLED' or 'REJECTED' or 'EXPIRED' or 'EXPIRED_AUTOMATICALLY' or 'SHARED' or 'EMAIL_VIEWED' or 
        ///  'AUTO_CANCELLED_CONVERSION_PROBLEM' or 'SIGNER_SUGGESTED_CHANGES' or 'SENDER_CREATED_NEW_REVISION' or 'PASSWORD_AUTHENTICATION_FAILED' 
        ///  or 'KBA_AUTHENTICATION_FAILED' or 'KBA_AUTHENTICATED' or 'WEB_IDENTITY_AUTHENTICATED' or 'WEB_IDENTITY_SPECIFIED' or 'EMAIL_BOUNCED' 
        ///  or 'WIDGET_ENABLED' or 'WIDGET_DISABLED' or 'DELEGATED' or 'AUTO_DELEGATED' or 'REPLACED_SIGNER' or 'VAULTED' or 'DOCUMENTS_DELETED' 
        ///  or 'OTHER']: Type of agreement event
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// An ID which uniquely identifies the version of the document associated with this audit event
        /// </summary>
        public string versionId { get; set; }
        /// <summary>
        /// The event comment. For RECALLED or REJECTED, the reason given by the user that initiates the event. For DELEGATE or SHARE, the message from the acting user to the participant
        /// </summary>
        public string comment { get; set; }
        /// <summary>
        /// Location of the device that created the event (This value may be null due to limited privileges)
        /// </summary>
        public DeviceLocation deviceLocation { get; set; }
        /// <summary>
        /// A unique identifier linking offline events to synchronization events (specified for offline signing events and synchronization events, else null)
        /// </summary>
        public string synchronizationId { get; set; }
        /// <summary>
        /// The identifier assigned by the vault provider for the vault event (if vaulted, otherwise null)
        /// </summary>
        public string vaultEventId { get; set; }
    }
}
