using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class UserCreationInfo
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        /// <summary>
        /// (optional)
        /// </summary>
        public string company { get; set; }
        /// <summary>
        /// (optional)
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// (optional)
        /// </summary>
        public string phone { get; set; }
    }
}
