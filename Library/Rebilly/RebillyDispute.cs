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
    public class RebillyDispute : RebillyRequest
    {
        public const string DISPUTE_URL = "disputes/";
        /// <summary>
        /// processor account id on Rebilly
        /// </summary>
        public string processorAccountId;
        /// <summary>
        /// dispute type can be ('1CB', '2CB', 'RET')
        /// </summary>
        public string type;
        /// <summary>
        /// dispute time
        /// </summary>
        public string postedTime;
        /// <summary>
        /// transaction id on Rebilly
        /// </summary>
        public string transactionId;
        /// <summary>
        /// transaction id outside Rebilly
        /// </summary>
        public string legacyTransactionId;
        /// <summary>
        /// acquirerReferenceId
        /// </summary>
        public string acquirerReferenceId;
        /// <summary>
        /// dispute amount
        /// </summary>
        public string amount;
        /// <summary>
        /// 3 letter currency code
        /// </summary>
        public string currency;
        /// <summary>
        /// dispute reason code
        /// </summary>
        public string disputeReasonCodeId;
        /// <summary>
        /// dispute deadline
        /// </summary>
        public string deadlineTime;
        /// <summary>
        /// processor reason code
        /// </summary>
        public string processorReasonCode;
        /// <summary>
        /// processor reference id
        /// </summary>
        public string processorReferenceId;
        /// <summary>
        /// processor comments
        /// </summary>
        public string processorComments;
        /// <summary>
        /// raw response from processor
        /// </summary>
        public string rawResponse;
        /// <summary>
        /// payment method
        /// </summary>
        public string paymentMethod;

        /// <summary>
        /// constructor
        /// </summary>
        public RebillyDispute()
        {
            this.setApiController(DISPUTE_URL);
        }

        /// <summary>
        /// create dispute entry
        /// </summary>
        /// <returns>Rebilly response</returns>
        public RebillyResponse create()
        {
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="dispute">dispute object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(RebillyDispute dispute)
        {
            string data = JsonConvert.SerializeObject(dispute, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
