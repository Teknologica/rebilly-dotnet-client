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
    public class RebillyCustomer : RebillyRequest
    {
        /// <summary>
        /// Unique id for each customer
        /// </summary>
        public string customerId = null;
        /// <summary>
        /// Unique id for each customer (use to lookup for customer)
        /// </summary>
        public string lookupCustomerId = null;
        /// <summary>
        /// customer's email
        /// </summary>
        public string email = null;
        /// <summary>
        /// customer's first name
        /// </summary>
        public string firstName = null;
        /// <summary>
        /// customer's last name
        /// </summary>
        public string lastName = null;
        /// <summary>
        /// customer's IP address
        /// </summary>
        public string ipAddress = null;
        /// <summary>
        /// Payment Card related to a customer
        /// </summary>
        public RebillyPaymentCard paymentCard = null;
        /// <summary>
        /// Subscription related to a customer
        /// </summary>
        public RebillySubscription subscription = null;
        /// <summary>
        /// Subscription related to a customer (use when switching from one subscription to a new one)
        /// </summary>
        public RebillySubscription newSubscription = null;

        public const string CUSTOMER_URL = "customers/";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">customer id</param>
        public RebillyCustomer(string id = null)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.lookupCustomerId = id;
            }
            this.setApiController(CUSTOMER_URL);
        }

        /// <summary>
        /// Get customer information
        /// </summary>
        /// <returns> RebillyResponse </returns>
        public RebillyResponse retrieve()
        {
            this.setApiController(CUSTOMER_URL + this.lookupCustomerId);
            return this.sendGetRequest();
        }

        /// <summary>
        /// Get all subscriptions that belong to a customer
        /// </summary>
        /// <returns> RebillyResponse </returns>
        public RebillyResponse retrieveSubscriptions()
        {
            this.setApiController(CUSTOMER_URL + this.lookupCustomerId + "/subscriptions");
            return this.sendGetRequest();
        }

        /// <summary>
        /// Get all transactions that belong to a customer
        /// </summary>
        /// <returns> RebillyResponse </returns>
        public RebillyResponse retrieveTransactions()
        {
            this.setApiController(CUSTOMER_URL + this.lookupCustomerId + "/transactions");
            return this.sendGetRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RebillyResponse create()
        {
            string data = this.buildRequest(this);
            
            return this.sendPostRequest(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RebillyResponse update()
        {
            string data = this.buildRequest(this);

            return this.sendPutRequest(data);
        }

        /// <summary>
        /// Switch subscription
        /// </summary>
        /// <returns></returns>
        public RebillyResponse switchPlan()
        {
            string data = this.buildRequest(this);

            return this.sendPutRequest(data);
        }

        /// <summary>
        /// Stop/Cancel subscription
        /// </summary>
        /// <returns></returns>
        public RebillyResponse stopSubscription()
        {
            string data = this.buildRequest(this);

            return this.sendDeleteRequest(data);
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="customer">customer object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(RebillyCustomer customer)
        {
            string data = JsonConvert.SerializeObject(customer, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
