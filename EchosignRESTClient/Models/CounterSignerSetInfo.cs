using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchosignRESTClient.Models
{
    public class CounterSignerSetInfo
    {
        /// <summary>
        /// Information about the members of the counter signer set, currently we support only one member
        /// </summary>
        public List<CounterSignerInfo> counterSignerSetMemberInfos { get; set; }
        /// <summary>
        ///  ['SIGNER' or 'APPROVER' or 'DELEGATE_TO_SIGNER' or 'DELEGATE_TO_APPROVER']: 
        ///  Specify the role of counter signer set
        /// </summary>
        public string counterSignerSetRole { get; set; }
    }
}
