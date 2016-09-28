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
    }
}
