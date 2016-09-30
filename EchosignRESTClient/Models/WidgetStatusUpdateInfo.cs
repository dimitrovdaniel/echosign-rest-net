using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class WidgetStatusUpdateInfo
    {
        /// <summary>
        /// Display this custom message to the user when the widget is accessed. 
        /// Note that this can contain wiki markup to include clickable links in 
        /// the message. This is required if redirectUrl is not provided. Both 
        /// message and redirectUrl can not be specified
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// ['DISABLE' or 'ENABLE']: The status to which the widget is to be updated. 
        /// The possible values for this variable are ENABLE and DISABLE
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// Redirect the user to this URL when the widget is accessed. This is required 
        /// if message is not provided. Both message and redirectUrl can not be specified
        /// </summary>
        public string redirectUrl { get; set; }
    }
}
