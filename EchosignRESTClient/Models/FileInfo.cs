using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class FileInfo
    {
        /// <summary>
        /// The documentID as returned from the transient document creation API
        /// </summary>
        public string transientDocumentId { get; set; }
    }
}
