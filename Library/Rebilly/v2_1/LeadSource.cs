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
    public class LeadSource : RebillyRequest
    {
        const string LEAD_END_POINT = "lead-sources/";
        const string CUSTOMER_END_POINT = "customers/";

        /// <summary>  
        /// string medium
        /// </summary>
        public string medium = null;
        /// <summary>  
        /// string source
        /// </summary>
        public string source = null;
        /// <summary>  
        /// string campaign
        /// </summary>
        public string campaign = null;
        /// <summary>  string term
        /// </summary>
        public string term = null;
        /// <summary>
        /// string content
        /// </summary>
        public string content = null;
        /// <summary>
        /// string affiliate
        /// </summary>
        public string affiliate = null;
        /// <summary>  
        /// string subAffiliate
        /// </summary>
        public string subAffiliate = null;
        /// <summary>  
        /// string salesAgent
        /// </summary>
        public string salesAgent = null;
        /// <summary>  
        /// string currency
        /// </summary>
        public string currency = null;
        /// <summary>  
        /// string amount
        /// </summary>
        public string amount = null;
        /// <summary>  
        /// string createdTime
        /// </summary>
        public string createdTime = null;
        /// <summary>  
        /// string updatedTime
        /// </summary>
        public string updatedTime = null;
        /// <summary>  
        /// string path
        /// </summary>
        public string path = null;
        /// <summary>  
        /// string clickId
        /// </summary>
        public string clickId = null;
        /// <summary>  
        /// string ipAddress
        /// </summary>
        public string ipAddress = null;
        /// <summary>  
        /// string attribution
        /// </summary>
        public string attribution = null;

        /// <summary>  
        /// string customerId
        /// </summary>
        private string customerId = null;
        /// <summary>  
        /// string id
        /// </summary>
        private string id = null;

        /**
         * Set version
         */
        public LeadSource(string customerId = null, string id = null)
        {
            if (!String.IsNullOrEmpty(customerId))
            {
                this.customerId = customerId;
            }
            if (!String.IsNullOrEmpty(id))
            {
                this.id = id;
            }
            this.setApiVersion("v2.1");
        }

        /// <summary>
        /// Create a leas source
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///    Rebilly.v2_1.LeadSource leadSource = new Rebilly.v2_1.LeadSource("customerId");
        ///    leadSource.setApiKey("your api key");
        ///    leadSource.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///
        ///    leadSource.medium = "search";
        ///    leadSource.source = "yahoo";
        ///    leadSource.campaign = "leg warmers";
        ///
        ///    RebillyResponse response = leadSource.create();
        ///    if (response.statusCode == HttpStatusCode.Created)
        ///    {
        ///        // Successfully created
        ///    }
        ///    else
        ///    {
        ///       // Something wrong -- check response for errors
        ///       Console.WriteLine(response.getRawResponse());
        ///    }
        /// </code>
        /// </example>
        public RebillyResponse create()
        {
            if (String.IsNullOrEmpty(this.customerId))
            {
                throw new Exception("customerId cannot be empty.");
            }
            this.setApiController(CUSTOMER_END_POINT + this.customerId + "/" + LEAD_END_POINT);
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Create a leas source with ID
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///    Rebilly.v2_1.LeadSource leadSource = new Rebilly.v2_1.LeadSource("customerId", "leadSourceId");
        ///    leadSource.setApiKey("your api key");
        ///    leadSource.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///
        ///    leadSource.medium = "search";
        ///    leadSource.source = "yahoo";
        ///    leadSource.campaign = "leg warmers";
        ///
        ///    RebillyResponse response = leadSource.update();
        ///    if (response.statusCode == HttpStatusCode.Created)
        ///    {
        ///        // Successfully created
        ///    }
        ///    else
        ///    {
        ///       // Something wrong -- check response for errors
        ///       Console.WriteLine(response.getRawResponse());
        ///    }
        /// </code>
        /// </example>
        public RebillyResponse update()
        {
            if (String.IsNullOrEmpty(this.customerId))
            {
                throw new Exception("customerId cannot be empty.");
            }
            this.setApiController(CUSTOMER_END_POINT + this.customerId + "/" + LEAD_END_POINT + this.id);
            string data = this.buildRequest(this);

            return this.sendPutRequest(data);
        }

        /// <summary>
        /// Get lead source
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///    Rebilly.v2_1.LeadSource leadSource = new Rebilly.v2_1.LeadSource("customerId", "leadSourceId");
        ///    leadSource.setApiKey("your api key");
        ///    leadSource.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///
        ///    Dictionary<string, string> params = new Dictionary<string, string>()
        ///    {
        ///        {"fields", "source,medium,customer"}, // only want to show "id", "source", "medium" and "customer"
        ///        {"expand", "customer"}, // expand customer object
        ///    };
        ///    leadSource.setQueryParam(params);
        ///    RebillyResponse response = leadSource.retrieve();
        ///    if (response.statusCode == HttpStatusCode.OK)
        ///    {
        ///        // Successfully retrieved
        ///    }
        /// </code>
        /// </example>
        public RebillyResponse retrieve()
        {
            if (String.IsNullOrEmpty(this.customerId))
            {
                throw new Exception("customerId cannot be empty.");
            }
            this.setApiController(CUSTOMER_END_POINT + this.customerId + "/" + LEAD_END_POINT + this.id);

            return this.sendGetRequest();
        }

        /// <summary>
        /// List lead sources per customer
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///    Rebilly.v2_1.LeadSource leadSource = new Rebilly.v2_1.LeadSource("customerId");
        ///    leadSource.setApiKey("your api key");
        ///    leadSource.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///
        ///    Dictionary<string, string> params = new Dictionary<string, string>()
        ///    {
        ///        {"fields", "source,medium,customer"}, // only want to show "id", "source", "medium" and "customer"
        ///        {"expand", "customer"}, // expand customer object
        ///    };
        ///    leadSource.setQueryParam(params);
        ///    RebillyResponse response = leadSource.listByCustomer();
        ///    if (response.statusCode == HttpStatusCode.OK)
        ///    {
        ///        // Successfully retrieved
        ///    }
        /// </code>
        /// </example>
        public RebillyResponse listByCustomer()
        {
            if (String.IsNullOrEmpty(this.customerId))
            {
                throw new Exception("customerId cannot be empty.");
            }
            this.setApiController(CUSTOMER_END_POINT + this.customerId + "/" + LEAD_END_POINT);

            return this.sendGetRequest();
        }

        /// <summary>
        /// List lead sources
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///    Rebilly.v2_1.LeadSource leadSource = new Rebilly.v2_1.LeadSource();
        ///    leadSource.setApiKey("your api key");
        ///    leadSource.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///
        ///    Dictionary<string, string> params = new Dictionary<string, string>()
        ///    {
        ///        {"fields", "source,medium,customer"}, // only want to show "id", "source", "medium" and "customer"
        ///        {"expand", "customer"}, // expand customer object
        ///    };
        ///    leadSource.setQueryParam(params);
        ///    RebillyResponse response = leadSource.listAll();
        ///    if (response.statusCode == HttpStatusCode.OK)
        ///    {
        ///        // Successfully retrieved
        ///    }
        /// </code>
        /// </example>
        public RebillyResponse listAll()
        {
            this.setApiController(LEAD_END_POINT);

            return this.sendGetRequest();
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="leadSource">LeadSource object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(LeadSource leadSource)
        {
            string data = JsonConvert.SerializeObject(leadSource, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
