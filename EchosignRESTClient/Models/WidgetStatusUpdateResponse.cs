using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class WidgetStatusUpdateResponse
    {
        /// <summary>
        ///  ['OK' or 'ALREADY_DISABLED' or 'ALREADY_ENABLED']: The result of the attempt to disable or enable the widget
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// String result message if there was no error
        /// </summary>
        public string message { get; set; }
    }
}
