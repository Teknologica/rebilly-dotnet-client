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
    public class Customer : RebillyRequest
    {
        const string END_POINT = "customers/";
        /// <summary>
        /// string email
        /// </summary>
        public string email = null;
        /// <summary>
        /// string firstName
        /// </summary>
        public string firstName = null;
        /// <summary>
        /// string lastName
        /// </summary>
        public string lastName = null;
        /// <summary>
        /// string ipAddress
        /// </summary>
        public string ipAddress = null;
        /// <summary>
        /// default payment card
        /// </summary>
        public string defaultCard = null;
        /// <summary>
        /// string id
        /// </summary>
        private string id = null;

        /// <summary>
        /// Set api version and endpoint
        /// </summary>
        /// <param name="id"></param>
        public Customer(string id = null)
        {
            if (!String.IsNullOrEmpty(id)) 
            {
                this.id = id;
            }
            this.setApiVersion("v2.1");
            this.setApiController(END_POINT);
        }

        /// <summary>
        /// Create a customer
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Customer customer = new Rebilly.v2_1.Customer();
        ///     customer.setApiKey("your api key");
        ///     customer.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     customer.email = "example@example.com";
        ///     customer.firstName = "Test";
        ///     customer.lastName = "Customer";
        ///     customer.ipAddress = "127.0.0.1";
        /// 
        ///     RebillyResponse response = customer.create();
        ///     if (response.statusCode == HttpStatusCode.Created)
        ///     {
        ///         // successfully created
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse create()
        {
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }
        
        /// <summary>
        /// Update customer info
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     /** 
        ///      * Update if customer existed, create new record otherwise.
        ///      */
        ///     Rebilly.v2_1.Customer customer = new Rebilly.v2_1.Customer("customerId");
        ///     customer.setApiKey("your api key");
        ///     customer.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     customer.email = "example@example.com";
        ///     customer.firstName = "Test";
        ///     customer.lastName = "Customer";
        ///     customer.ipAddress = "127.0.0.1";
        /// 
        ///     RebillyResponse response = customer.update();
        ///     if (response.statusCode == HttpStatusCode.Created)
        ///     {
        ///         // successfully created
        ///     } 
        ///     elseif (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // successfully updated
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse update()
        {
            if (String.IsNullOrEmpty(this.id)) 
            {
                throw new Exception("customer id cannot be empty.");
            }
            this.setApiController(END_POINT + this.id);
            string data = this.buildRequest(this);
            return this.sendPutRequest(data);
        }

        /// <summary>
        /// List all customers
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Customer customer = new Rebilly.v2_1.Customer();
        ///     customer.setApiKey("your api key");
        ///     customer.setEnvironment(RebillyRequest.ENV_SANDBOX);
        /// 
        ///     RebillyResponse response = customer.listAll();
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
        /// Get a customer
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Customer customer = new Rebilly.v2_1.Customer("customerId");
        ///     customer.setApiKey("your api key");
        ///     customer.setEnvironment(RebillyRequest.ENV_SANDBOX);
        /// 
        ///     RebillyResponse response = customer.retrieve();
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
                throw new Exception("customer id cannot be empty.");
            }
            this.setApiController(END_POINT + this.id);
            return this.sendGetRequest();
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="customer">Customer object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(Customer customer)
        {
            string data = JsonConvert.SerializeObject(customer, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
