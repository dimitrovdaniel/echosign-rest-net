using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class RequestFormField
    {
        public FormFieldLocation[] locations { get; set; }

        public string name { get; set; }

        public string defaultValue { get; set; }

        public bool required { get; set; }

        public bool readOnly { get; set; }
    }

    public class FormFieldLocation
    {
        public double height { get; set; }
        public double left { get; set; }
        public int pageNumber { get; set; }
        public double top { get; set; }
        public double width { get; set; }
    }
}
