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
    public class RebillyTransaction : RebillyRequest
    {
        public string lookupTransactionId = null;
        public string amount = null;

        public RebillyTransaction(string id)
        {
            this.lookupTransactionId = id;
            this.setApiController("transactions");
        }

        public RebillyResponse refund()
        {
            string data = this.buildRequest(this);

            return this.sendDeleteRequest(data);
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="transaction">transaction object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(RebillyTransaction transaction)
        {
            string data = JsonConvert.SerializeObject(transaction, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
