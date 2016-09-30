using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class WidgetCreationInfo
    {
        /// <summary>
        ///  A list of one or more files (or references to files) that will be used to create the widget. 
        ///  If more than one file is provided, they will be combined before the widget is created. Library 
        ///  documents are not permitted. 
        ///  Note: Only one of the four parameters in every FileInfo object must be specified
        /// </summary>
        public List<FileInfo> fileInfos { get; set; }
        /// <summary>
        /// The name of the widget that will be used to identify it, in emails and on the website
        /// </summary>
        public string name { get; set; }
        /// <summary>
        ///  ['SENDER_SIGNATURE_NOT_REQUIRED' or 'SENDER_SIGNS_LAST']: Selects the workflow you would 
        ///  like to use - whether the sender needs to sign before the recipient, after the recipient,
        ///  or not at all. The possible values for this variable are SENDER_SIGNATURE_NOT_REQUIRED 
        ///  or SENDER_SIGNS_LAST
        /// </summary>
        public string signatureFlow { get; set; }
        /// <summary>
        /// Indicates that authoring is requested prior to sending the document
        /// </summary>
        public bool authoringRequested { get; set; }
        /// <summary>
        /// Security options
        /// </summary>
        public WidgetSecurityOption securityOptions { get; set; }
        /// <summary>
        /// Counter signers information, counter signers will be emailed and prompted to sign
        /// </summary>
        public List<CounterSignerSetInfo> counterSignerSetInfos { get; set; }
    }
}
