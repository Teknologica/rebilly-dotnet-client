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
    public class Dispute : RebillyRequest
    {
        const string END_POINT = "disputes/";
        /// <summary> 
        /// string processorAccountId 
        /// </summary>
        public string processorAccountId = null;
        /// <summary> 
        /// string type 
        /// </summary>
        public string type = null;
        /// <summary> 
        /// string postedTime 
        /// </summary>
        public string postedTime = null;
        /// <summary> 
        /// string transactionId 
        /// </summary>
        public string transactionId = null;
        /// <summary> 
        /// string legacyTransactionId 
        /// </summary>
        public string legacyTransactionId = null;
        /// <summary> 
        /// string acquirerReferenceId 
        /// </summary>
        public string acquirerReferenceId = null;
        /// <summary> 
        /// string amount 
        /// </summary>
        public string amount = null;
        /// <summary> 
        /// string currency 
        /// </summary>
        public string currency = null;
        /// <summary> 
        /// string disputeReasonCodeId 
        /// </summary>
        public string disputeReasonCodeId = null;
        /// <summary> 
        /// string paymentMethod 
        /// </summary>
        public string paymentMethod = null;
        /// <summary> 
        /// string deadlineTime 
        /// </summary>
        public string deadlineTime = null;
        /// <summary> 
        /// string rawResponse 
        /// </summary>
        public string rawResponse = null;
        /// <summary> 
        /// string id 
        /// </summary>
        private string id = null;

        /// <summary>
        /// Set api version and endpoint
        /// </summary>
        /// <param name="id"></param>
        public Dispute(string id = null)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.id = id;
            }
            this.setApiVersion("v2.1");
            this.setApiController(END_POINT);
        }

        /// <summary>
        /// Create a dispute
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Dispute dispute = new Rebilly.v2_1.Dispute();
        ///     dispute.setApiKey("your api key");
        ///     dispute.setEnvironment(RebillyRequest::ENV_SANDBOX);
        ///     dispute.processorAccountId = "RebillyProcessor";
        ///     dispute.type = "1CB";
        ///     dispute.postedTime = "2013-02-19 13:10:30";
        ///     dispute.transactionId = "CPT8U35KGCSV";
        ///     dispute.amount = "5.99";
        ///     dispute.disputeReasonCodeId = "1000";
        ///     dispute.currency = "USD";
        ///     dispute.rawResponse = "Raw Response";
        ///     
        ///     RebillyResponse response = dispute.create();
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
        /// Update a dispute
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Dispute dispute = new Rebilly.v2_1.Dispute("disputeID");
        ///     dispute.setApiKey("your api key");
        ///     dispute.setEnvironment(RebillyRequest::ENV_SANDBOX);
        ///     dispute.processorAccountId = "RebillyProcessor";
        ///     dispute.type = "1CB";
        ///     dispute.postedTime = "2013-02-19 13:10:30";
        ///     dispute.transactionId = "CPT8U35KGCSV";
        ///     dispute.amount = "5.99";
        ///     dispute.disputeReasonCodeId = "1000";
        ///     dispute.currency = "USD";
        ///     dispute.rawResponse = "Raw Response";
        ///     
        ///     RebillyResponse response = dispute.update();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // successfully updated
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse update()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("Dispute id cannot be empty.");
            }
            this.setApiController(END_POINT + this.id);
            string data = this.buildRequest(this);

            return this.sendPutRequest(data);
        }

        /// <summary>
        /// List all disputes
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Dispute dispute = new Rebilly.v2_1.Dispute();
        ///     dispute.setApiKey("your api key");
        ///     dispute.setEnvironment(RebillyRequest::ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = dispute.listAll();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // success
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse listAll()
        {
            return this.sendGetRequest();
        }

        /// <summary>
        /// Get a dispute
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Dispute dispute = new Rebilly.v2_1.Dispute("disputeID");
        ///     dispute.setApiKey("your api key");
        ///     dispute.setEnvironment(RebillyRequest::ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = dispute.retrieve();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // success
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse retrieve()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("Dispute id cannot be empty.");
            }
            this.setApiController(END_POINT + this.id);

            return this.sendGetRequest();
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="dispute">Dispute object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(Dispute dispute)
        {
            string data = JsonConvert.SerializeObject(dispute, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
