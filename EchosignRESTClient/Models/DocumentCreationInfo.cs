using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class DocumentCreationInfo
    {
        /// <summary>
        /// A list of one or more files (or references to files) that will be sent out for signature. 
        /// If more than one file is provided, they will be combined into one PDF before being sent out. 
        /// Note: Only one of the four parameters in every FileInfo object must be specified
        /// </summary>
        public List<FileInfo> fileInfos { get; set; }
        /// <summary>
        /// The name of the agreement that will be used to identify it, in emails and on the website
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// A list of one or more recipient sets. A recipient set may have one or more recipients. 
        /// If any member of the recipient set signs, the agreement is considered signed by the recipient set.
        /// Note: If signatureFlow is set to SENDER_SIGNS_ONLY, this parameter is optional
        /// </summary>
        public List<RecipientInfo> recipientSetInfos { get; set; }

        /// <summary>
        /// ['ESIGN' or 'WRITTEN']: Specifies the type of signature you would like to request - written or e-signature
        /// </summary>
        public string signatureType { get; set; }

        /// <summary>
        /// ['SENDER_SIGNATURE_NOT_REQUIRED' or 'SENDER_SIGNS_LAST' or 'SENDER_SIGNS_FIRST' or 'SEQUENTIAL' or 'PARALLEL' or 'SENDER_SIGNS_ONLY']: Selects the 
        /// workflow you would like to use - whether the sender needs to sign only, before the recipient, after the recipient, or not at all.
        /// Note: leave unspecified for hybrid routing
        /// </summary>
        public string signatureFlow { get; set; }

        /// <summary>
        /// A list of one or more email addresses that you want to copy on this transaction. 
        /// The email addresses will each receive an email at the beginning of the transaction 
        /// and also when the final document is signed. The email addresses will also receive a 
        /// copy of the document, attached as a PDF file
        /// </summary>
        public List<string> ccs { get; set; }

        /// <summary>
        /// A publicly accessible url to which Adobe Sign will do an HTTP GET operation every time there is a new agreement event.
        /// Your GET url will receive the following parameters: documentKey={AGREEMENT_ID}&status={STATUS}&eventType={EVENT_TYPE}
        /// </summary>
        public string callbackInfo { get; set; }

        /// <summary>
        /// The number of days that remain before the document expires. You cannot sign the document after it expires
        /// </summary>
        public int daysUntilSigningDeadline { get; set; }

        /// <summary>
        ///  ['DAILY_UNTIL_SIGNED' or 'WEEKLY_UNTIL_SIGNED']: Optional parameter that sets how often you want 
        ///  to send reminders to the recipients. 
        /// </summary>
        public string reminderFrequency { get; set; }
    }
}
