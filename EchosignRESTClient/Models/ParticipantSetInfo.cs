using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class ParticipantSetInfo
    {
        /// <summary>
        /// The unique identifier of the participant set
        /// </summary>
        public string participantSetId { get; set; }
        /// <summary>
        /// Information about the members of the recipient set
        /// </summary>
        public List<ParticipantInfo> participantSetMemberInfos { get; set; }
        /// <summary>
        /// ['SENDER' or 'SIGNER' or 'APPROVER' or 'DELEGATE_TO_SIGNER' or 'DELEGATE_TO_APPROVER' or 
        /// 'CC' or 'DELEGATE' or 'SHARE' or 'OTHER']: The current roles of the participant set. 
        /// A participant set can have one or more roles
        /// </summary>
        public string[] roles { get; set; }
        /// <summary>
        ///  ['WAITING_FOR_MY_SIGNATURE' or 'WAITING_FOR_MY_APPROVAL' or 'WAITING_FOR_MY_DELEGATION' or 'OUT_FOR_SIGNATURE' or 
        ///  'SIGNED' or 'APPROVED' or 'RECALLED' or 'HIDDEN' or 'NOT_YET_VISIBLE' or 'WAITING_FOR_FAXIN' or 'ARCHIVED' or 'UNKNOWN' 
        ///  or 'PARTIAL' or 'FORM' or 'WAITING_FOR_AUTHORING' or 'OUT_FOR_APPROVAL' or 'WIDGET' or 'EXPIRED' or 'WAITING_FOR_MY_REVIEW' 
        ///  or 'IN_REVIEW' or 'OTHER']: The status of the participant set with respect to the widget
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// The name of the participant set. Returned only, if the API caller is the sender of agreement
        /// </summary>
        public string participantSetName { get; set; }
        /// <summary>
        /// Private message for the participants in the set
        /// </summary>
        public string privateMessage { get; set; }
        /// <summary>
        /// ['PASSWORD' or 'WEB_IDENTITY' or 'KBA' or 'PHONE' or 'OTHER']: Security options that apply to the participant
        /// </summary>
        public string[] securityOptions { get; set; }
        /// <summary>
        /// Index indicating sequential signing group (specified for hybrid routing)
        /// </summary>
        public int signingOrder { get; set; }
    }
}
