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
    public class PaymentCard : RebillyRequest
    {
        const string CARD_END_POINT = "/payment-cards/";
        const string CUSTOMER_END_POINT = "customers/";

        /// <summary>
        /// Primary Account Number
        /// </summary>
        public string pan = null;
        /// <summary>
        /// Expired Month
        /// </summary>
        public string expMonth = null;
        /// <summary>
        /// Expriex Year
        /// </summary>
        public string expYear = null;
        /// <summary>
        /// Contact's ID
        /// </summary>
        public string billingContact = null;
        /// <summary>
        /// CVV
        /// </summary>
        public string cvv = null;
        /// <summary>
        /// Card token
        /// </summary>
        public string token = null;

        /// <summary>
        /// Customer's ID
        /// </summary>
        private string customerId = null;
        /// <summary>
        /// PaymentCard's ID
        /// </summary>
        private string id = null;

        /// <summary>
        /// Set customer's ID and api version
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="id"></param>
        public PaymentCard(string customerId, string id = null)
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
        /// Get a payment card
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.PaymentCard card = new Rebilly.v2_1.PaymentCard("customerId", "paymentCardId");
        ///     card.setApiKey("your api key");
        ///     card.setEnvironment(RebillyRequest.ENV_SANDBOX);
        /// 
        ///     RebillyResponse response = card.retrieve();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // check response from processor
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse retrieve()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("Card id cannot be empty.");
            }
            this.setApiController(CUSTOMER_END_POINT + this.customerId + CARD_END_POINT + this.id);

            return this.sendGetRequest();
        }

        /// <summary>
        /// List all payment cards
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.PaymentCard card = new Rebilly.v2_1.PaymentCard("customerId");
        ///     card.setApiKey("your api key");
        ///     card.setEnvironment(RebillyRequest.ENV_SANDBOX);
        /// 
        ///     RebillyResponse response = card.listAll();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // check response from processor
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse listAll()
        {
            this.setApiController(CUSTOMER_END_POINT + this.customerId + CARD_END_POINT);

            return this.sendGetRequest();
        }

        /// <summary>
        /// Create a payment cards
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.PaymentCard card = new Rebilly.v2_1.PaymentCard("customerId");
        ///     card.setApiKey("your api key");
        ///     card.setEnvironment(RebillyRequest.ENV_SANDBOX);
        /// 
        ///     card.pan = "4111111111111111";
        ///     card.expMonth = "02";
        ///     card.expYear = "2018";
        ///     card.contact = "contact123ABC";
        ///     
        ///     RebillyResponse response = card.create();
        ///     if (response.statusCode == HttpStatusCode.Created)
        ///     {
        ///         // check response from processor
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse create()
        {
            this.setApiController(CUSTOMER_END_POINT + this.customerId + CARD_END_POINT);

            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Create a payment cards with an ID
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.PaymentCard card = new Rebilly.v2_1.PaymentCard("customerId", "paymentCardId");
        ///     card.setApiKey("your api key");
        ///     card.setEnvironment(RebillyRequest.ENV_SANDBOX);
        /// 
        ///     card.pan = "4111111111111111";
        ///     card.expMonth = "02";
        ///     card.expYear = "2018";
        ///     card.contact = "contact123ABC";
        ///     
        ///     RebillyResponse response = card.update();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // check response from processor
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse update()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("Card ID cannot be empty.");
            }
            this.setApiController(CUSTOMER_END_POINT + this.customerId + CARD_END_POINT + this.id);

            string data = this.buildRequest(this);

            return this.sendPutRequest(data);
        }

        /// <summary>
        /// Do authorize a payment cards
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.PaymentCard card = new Rebilly.v2_1.PaymentCard("customerId", "paymentCardId");
        ///     card.setApiKey("your api key");
        ///     card.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = card.authorize();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // check response from processor
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse authorize()
        {
            this.setApiController(CUSTOMER_END_POINT + this.customerId + CARD_END_POINT + "authorization/");

            return this.sendPostRequest(null);
        }

        /// <summary>
        /// Deactivate a payment cards
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.PaymentCard card = new Rebilly.v2_1.PaymentCard("customerId", "paymentCardId");
        ///     card.setApiKey("your api key");
        ///     card.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = card.deactivate();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // check response from processor
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse deactivate()
        {
            this.setApiController(CUSTOMER_END_POINT + this.customerId + CARD_END_POINT + "deactivation/");

            return this.sendPostRequest(null);
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="subscription">Subscription object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(PaymentCard paymentCard)
        {
            string data = JsonConvert.SerializeObject(paymentCard, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
