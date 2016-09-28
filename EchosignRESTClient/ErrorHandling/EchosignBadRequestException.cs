using EchosignRESTClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.ErrorHandling
{
    public class EchosignBadRequestException : Exception
    {
        public EchosignBadRequestException(EchosignErrorCode error)
        {
            this.Error = error;
        }

        public EchosignErrorCode Error { get; set; }
    }
}
