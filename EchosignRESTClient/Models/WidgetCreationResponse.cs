using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class WidgetCreationResponse
    {
        /// <summary>
        /// Javascript snippet suitable for an embedded page taking a user to a URL
        /// </summary>
        public string javascript { get; set; }
        /// <summary>
        /// Javascript snippet suitable for an embedded page of the redirected URL that can be used by widget creators
        /// </summary>
        public string nextPageEmbeddedCode { get; set; }
        /// <summary>
        /// Redirect URL once the widget is created
        /// </summary>
        public string nextPageUrl { get; set; }
        /// <summary>
        /// Standalone URL to direct end users to
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// The unique identifier of widget which can be used to retrieve the data entered by the signers
        /// </summary>
        public string widgetId { get; set; }
    }
}
