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
    public class RebillyMeteredBilling : RebillyRequest
    {
        public const string METERED_BILLING_URL = "meteredBilling/";
        
        public string itemId = null;

        public string type = null;

        public string amount = null;

        public string quantity = null;

        public string description = null;

        public RebillyResponse retrieve()
        {
            this.setApiController(METERED_BILLING_URL + itemId);

            return this.sendGetRequest();
        }
    }
}
