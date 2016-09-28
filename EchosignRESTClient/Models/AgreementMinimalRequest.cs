using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    /// <summary>
    /// Information about the agreement that you want to send and authoring options that you want to apply at the time of sending
    /// </summary>
    public class AgreementMinimalRequest
    {
        /// <summary>
        ///  Information about the document you want to send,
        /// </summary>
        public DocumentCreationInfo documentCreationInfo { get; set; }
        /// <summary>
        ///  Options for authoring and sending the agreement
        /// </summary>
        public InteractiveOptions options { get; set; }
    }
}
