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
    public class RebillyInvoiceItem : RebillyRequest
    {
        public const string INVOICE_ITEM_URL = "invoiceitems/";

        public string lookupCustomerId = null;

        public string lookupWebsiteId = null;

        public Dictionary<string, string>[] invoiceItem = null;

        private string itemId; 

        /// <summary>
        /// Cosntructor
        /// </summary>
        public RebillyInvoiceItem(string id = null)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.itemId = id;
            }
            this.setApiController(INVOICE_ITEM_URL);
        }

        /// <summary>
        /// Get invoice item information
        /// </summary>
        /// <returns> RebillyResponse </returns>
        public RebillyResponse retrieve()
        {
            this.setApiController(INVOICE_ITEM_URL + this.itemId);
            return this.sendGetRequest();
        }

        /// <summary>
        /// Create invoice item
        /// </summary>
        /// <returns> RebillyResponse </returns>
        public RebillyResponse create()
        {
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="customer">customer object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(RebillyInvoiceItem invoiceItem)
        {
            string data = JsonConvert.SerializeObject(invoiceItem, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
