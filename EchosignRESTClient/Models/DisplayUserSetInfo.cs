using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class DisplayUserSetInfo
    {
        /// <summary>
        /// Displays the info about user set
        /// </summary>
        public List<DisplayUserInfo> displayUserSetMemberInfos { get; set; }
        /// <summary>
        /// The name of the display user set. Returned only, if the API caller is the sender of agreement.
        /// </summary>
        public string displayUserSetName { get; set; }
    }
}
