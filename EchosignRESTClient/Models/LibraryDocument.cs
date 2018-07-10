using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class DocumentLibraryItem
    {
        public string libraryDocumentId { get; set; }
        public string name { get; set; }
    }

    public class DocumentLibraryItems
    {
        public DocumentLibraryItem[] libraryDocumentList { get; set; }
    }
}
