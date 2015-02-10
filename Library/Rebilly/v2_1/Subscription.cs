using System;
using System.Net;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Rebilly.v2_1
{
    public class Subscription : RebillyRequest
    {
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

        const string CUSTOMER_END_POINT = "customers/";
        const string SUBSCRIPTION_END_POINT = "/subscriptions/";

        /// <summary>
        /// plan's ID
        /// </summary>
        public string plan = null;
        /// <summary>
        /// website's ID
        /// </summary>
        public string website = null;
        /// <summary>
        /// payment card's ID
        /// </summary>
        public string paymentCard = null;
        /// <summary>
        /// delivery address's ID
        /// </summary>
        public string deliveryAddress = null;
        /// <summary>
        /// quantity
        /// </summary>
        public string quantity = null;
        /// <summary>
        /// policy
        /// </summary>
        public string policy = null;

        /// <summary>
        /// customer's ID
        /// </summary>
        private string customerId = null;
        /// <summary>
        /// subscription's ID
        /// </summary>
        private string id = null;

        /// <summary>
        /// Set customerId and API version
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="id"></param>
        public Subscription(string customerId, string id = null)
        {
            this.customerId = customerId;
            if (String.IsNullOrEmpty(this.customerId))
            {
                throw new Exception("customerId cannot be empty.");
            }
            if (!String.IsNullOrEmpty(id))
            {
                this.id = id;
            }
            this.setApiVersion("v2.1");
        }

        /// <summary>
        /// Create subscription
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Subscription subscription = new Rebilly.v2_1.Subscription("customerId");
        ///     subscription.setApiKey("apiKey");
        ///     subscription.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     subscription.plan = "54be26d01294a";
        ///     subscription.website = "N4FTS";
        ///     subscription.paymentCard = "card123ABC";
        ///     subscription.deliveryAddress = "address123ABC";
        ///     subscription.quantity = 1;
        ///
        ///     RebillyResponse response = subscription.create();
        ///     if (response.statusCode == HttpStatusCode.Created) {
        ///         // Successfully created subscription
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse create()
        {
            this.setApiController(CUSTOMER_END_POINT + this.customerId + SUBSCRIPTION_END_POINT);
            string data = this.buildRequest(this);
            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Update subscription
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Subscription subscription = new Rebilly.v2_1.Subscription("customerId", "subscriptionId");
        ///     subscription.setApiKey("apiKey");
        ///     subscription.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     subscription.renewalTime = "2015-01-01 00:00:00";
        ///     subscription.deliveryAddress = "address123ABC";
        ///     subscription.quantity = 1;
        ///
        ///     RebillyResponse response = subscription.update();
        ///     if (response.statusCode == HttpStatusCode.OK) {
        ///         // Successfully updated subscription
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse update()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("subscription id cannot be empty.");
            }
            this.setApiController(CUSTOMER_END_POINT + this.customerId + SUBSCRIPTION_END_POINT + this.id);
            string data = this.buildRequest(this);
            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Cancel subscription
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Subscription subscription = new Rebilly.v2_1.Subscription("customerId", "subscriptionId");
        ///     subscription.setApiKey("apiKey");
        ///     subscription.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     subscription.policy = Rebilly.v2_1.Subscription.CANCEL_AT_NEXT_REBILL;
        ///
        ///     RebillyResponse response = subscription.cancel();
        ///     if (response.statusCode == HttpStatusCode.OK) {
        ///         // Successfully cancelled
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse cancel()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("subscription id cannot be empty.");
            }
            this.setApiController(CUSTOMER_END_POINT + this.customerId + SUBSCRIPTION_END_POINT + this.id + "/cancel");
            string data = this.buildRequest(this);
            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Switch subscription
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Subscription subscription = new Rebilly.v2_1.Subscription("customerId", "subscriptionId");
        ///     subscription.setApiKey("apiKey");
        ///     subscription.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     subscription.plan = "54be26d01294a";
        ///     subscription.website = "N4FTS";
        ///     subscription.policy = Rebilly.v2_1.Subscription.SWITCH_AT_NEXT_REBILL;
        ///
        ///     RebillyResponse response = subscription.switchPlan();
        ///     if (response.statusCode == HttpStatusCode.OK) {
        ///         // Successfully switched
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse switchPlan()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("subscription id cannot be empty.");
            }
            this.setApiController(CUSTOMER_END_POINT + this.customerId + SUBSCRIPTION_END_POINT + this.id + "/switch");
            string data = this.buildRequest(this);
            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Get a subscription
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Subscription subscription = new Rebilly.v2_1.Subscription("customerId", "subscriptionId");
        ///     subscription.setApiKey("apiKey");
        ///     subscription.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///
        ///     RebillyResponse response = subscription.retrieve();
        ///     if (response.statusCode == HttpStatusCode.OK) {
        ///         // Successfully
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse retrieve()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("subscription id cannot be empty.");
            }
            this.setApiController(CUSTOMER_END_POINT + this.customerId + SUBSCRIPTION_END_POINT + this.id);
            return this.sendGetRequest();
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="transaction">Transaction object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(Subscription subscription)
        {
            string data = JsonConvert.SerializeObject(subscription, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
