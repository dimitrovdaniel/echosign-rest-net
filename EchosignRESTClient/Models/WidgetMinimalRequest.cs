using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class WidgetMinimalRequest
    {
        /// <summary>
        /// Information about the widget being created
        /// </summary>
        public WidgetCreationInfo widgetCreationInfo { get; set; }
    }
}
