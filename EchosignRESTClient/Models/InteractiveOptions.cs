using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class InteractiveOptions
    {
        /// <summary>
        /// Indicates that authoring is requested prior to sending the document
        /// </summary>
        public bool authoringRequested { get; set; }
        /// <summary>
        ///  If user settings allow, automatically logs the user in
        /// </summary>
        public bool autoLoginUser { get; set; }
    }
}
