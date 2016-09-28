using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EchosignRESTClient.Models;

namespace EchosignRESTClient.ErrorHandling
{
    public class EchosignOAuthException : Exception
    {
        public EchosignOAuthException(EchosignError error)
        {
            this.Error = error;
        }

        public EchosignError Error { get; set; }
    }
}
