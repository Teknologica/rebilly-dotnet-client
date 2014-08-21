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
        /// <summary>
        /// Subscription's URL endpoint
        /// </summary>
        public const string SUBSCRIPTION_URL = "subscriptions/";

        /**
         * Subscriptions change types
         */
        public const string SWITCH_AT_NEXT_REBILL = "AT_NEXT_REBILL";
        public const string SWITCH_NOW_WITH_PRORATA_REFUND = "NOW_WITH_PRORATA_REFUND";
        public const string SWITCH_NOW_WITHOUT_REFUND = "NOW_WITHOUT_REFUND";

        /***
         * Subscriptions cancellation types
         */
        public const string CANCEL_AT_NEXT_REBILL = "AT_NEXT_REBILL";
        public const string CANCEL_NOW = "NOW_WITHOUT_REFUND";
        public const string CANCEL_NOW_WITH_PRORATA_REFUND = "NOW_WITH_PRORATA_REFUND";
        public const string CANCEL_NOW_FULL_REFUND = "NOW_WITH_FULL_REFUND";

        public string subscriptionId = null;
        public string lookupSubscriptionId = null;
        public string websiteId = null;
        public string planId = null;
        public string switchWhen = null;
        public string cancelBehaviour = null;
        public string quantity = null;
        public string rebillTime = null;
        public string processorAccountId = null;
        public RebillyAddressInfo deliveryAddress = null;
        public RebillyThreeDSecure threeDSecure = null;

        public RebillyResponse create()
        {
            this.setApiController(SUBSCRIPTION_URL);
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
