using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class AgreementDocuments
    {
        /// <summary>
        /// A list of objects representing the documents
        /// </summary>
        public List<AgreementDocument> documents { get; set; }
        /// <summary>
        /// A list of supporting documents. This is returned only if there are any supporting document in the agreement
        /// </summary>
        public List<AgreementSupportingDocument> supportingDocuments { get; set; }
    }
}
