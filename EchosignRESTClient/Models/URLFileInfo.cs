using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class URLFileInfo
    {
        /// <summary>
        /// The mime type of the referenced file, used to determine if the file can be accepted and the necessary conversion steps can be performed
        /// </summary>
        public string mimeType { get; set; }
        /// <summary>
        /// The original system file name of the document being sent - used to name attachments, and to infer the mime type if one is not explicitly specified
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// A publicly accessible URL for retrieving the raw file content
        /// </summary>
        public string url { get; set; }
    }
}
