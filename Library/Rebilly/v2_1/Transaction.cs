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
    public class Transaction : RebillyRequest
    {
        /// <summary>
        /// API customer's endpoint
        /// </summary>
        const string CUSTOMER_END_POINT = "customers/";
        /// <summary>
        /// API transaction's endpoint
        /// </summary>
        const string TRANSACTION_END_POINT = "transactions/";
        /// <summary>
        /// Transaction's amount
        /// </summary>
        public string amount = null;
        /// <summary>
        /// Transaction's ID
        /// </summary>
        private string id = null;
        /// <summary>
        /// Customer's ID
        /// </summary>
        private string customerId = null;

        /// <summary>
        /// Set api version and endpoint
        /// </summary>
        /// <param name="id"></param>
        public Transaction(string id = null, string customerId = null)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.id = id;
            }
            if (!String.IsNullOrEmpty(customerId))
            {
                this.customerId = customerId;
            }
            this.setApiController(TRANSACTION_END_POINT);
            this.setApiVersion("v2.1");
        }

        /// <summary>
        /// Do refund
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Transaction transaction = new Rebilly.v2_1.Transaction("transactionId");
        ///     transaction.setApiKey("your api key");
        ///     transaction.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     transaction.amount = "9.99"; // set amount for partial refund.
        /// 
        ///     RebillyResponse response = transaction.refund();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // check response from processor
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse refund()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("Transaction id cannot be empty.");
            }
            this.setApiController(TRANSACTION_END_POINT + this.id + "/refund/");
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Get a transaction
        /// </summary>
        /// <returns> RebillyResponse </returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Transaction transaction = new Rebilly.v2_1.Transaction("transactionId");
        ///     transaction.setApiKey("your api key");
        ///     transaction.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = transaction.retrieve();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // see response
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse retrieve()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("Transaction ID cannot be empty.");
            }
            this.setApiController(TRANSACTION_END_POINT + this.id);

            return this.sendGetRequest();
        }

        /// <summary>
        /// List all transactions
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Transaction transaction = new Rebilly.v2_1.Transaction("transactionId");
        ///     transaction.setApiKey("your api key");
        ///     transaction.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = transaction.listAll();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // see response
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse listAll()
        {
            return this.sendGetRequest();
        }

        /// <summary>
        /// List all transactions per customer
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Transaction transaction = new Rebilly.v2_1.Transaction(null, "customerId");
        ///     transaction.setApiKey("your api key");
        ///     transaction.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = transaction.retrieveByCustomer();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // see response
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse retrieveByCustomer()
        {
            if (String.IsNullOrEmpty(this.customerId))
            {
                throw new Exception("Customer ID cannot be empty.");
            }
            this.setApiController(CUSTOMER_END_POINT + this.customerId + '/' + TRANSACTION_END_POINT);

            return this.sendGetRequest();
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="transaction">Transaction object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(Transaction transaction)
        {
            string data = JsonConvert.SerializeObject(transaction, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
