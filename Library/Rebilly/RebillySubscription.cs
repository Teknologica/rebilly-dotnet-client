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
    public class RebillySubscription : RebillyRequest
    {
        public string subscriptionId = null;
        public string lookupSubscriptionId = null;
        public string websiteId = null;
        public string planId = null;
        public string switchWhen = null;
        public string cancelBehaviour = null;
        public string quantity = null;
        public RebillyAddressInfo deliveryAddress = null;
        public string rebillTime = null;
        public List<RebillyMeteredBilling> meteredBilling = null;
        
        public const string SWITCH_AT_NEXT_REBILL = "AT_NEXT_REBILL";
        public const string SWITCH_NOW_WITH_PRORATA_REFUND = "NOW_WITH_PRORATA_REFUND";
        public const string SWITCH_NOW_WITHOUT_REFUND = "NOW_WITHOUT_REFUND";

        public const string CANCEL_AT_NEXT_REBILL = "AT_NEXT_REBILL";
        public const string CANCEL_NOW = "NOW_WITHOUT_REFUND";
        public const string CANCEL_NOW_WITH_PRORATA_REFUND = "NOW_WITH_PRORATA_REFUND";
        public const string CANCEL_NOW_FULL_REFUND = "NOW_WITH_FULL_REFUND";
        public const string CANCEL_NOW_ALL_REFUND = "NOW_WITH_ALL_CHARGES_REFUND";

        public RebillyResponse createMeteredBilling()
        {
            this.setApiController(RebillyMeteredBilling.METERED_BILLING_URL);
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="dispute">Subscription object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(RebillySubscription subscription)
        {
            string data = JsonConvert.SerializeObject(subscription, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
