using System;
using System.Net;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Rebilly
{
    public class RebillyCharge
    {
        /// <summary>
        /// amount to be charged
        /// </summary>
        public string amount = null;
        /// <summary>
        /// website id charge happen
        /// </summary>
        public string websiteId = null;
        /// <summary>
        /// 3 letters currency code
        /// </summary>
        public string currency = null;
        /// <summary>
        /// processor account id to use for the charge
        /// </summary>
        public string processorAccountId = null;
        /// <summary>
        /// delivery address for charge
        /// </summary>
        public RebillyAddressInfo deliveryAddress = null;
        /// <summary>
        /// Custom Fields
        /// </summary>
        public List<RebillyCustomField> customField = null;
    }
}
