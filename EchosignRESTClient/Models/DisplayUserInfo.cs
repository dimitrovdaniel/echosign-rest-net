using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class DisplayUserInfo
    {
        /// <summary>
        /// Displays the user's email
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Displays the name of the user's company, if available
        /// </summary>
        public string company { get; set; }
        /// <summary>
        /// Displays the user's full name, if available
        /// </summary>
        public string fullName { get; set; }
    }
}
