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
    public class InvoiceItem : RebillyRequest
    {
        public const string TYPE_DEBIT = "debit";
        public const string TYPE_CREDIT = "credit";
        const string ITEM_END_POINT = "/items/";
        const string INVOICE_END_POINT = "invoices/";
        /// <summary>
        /// InvoiceItem's type
        /// </summary>
        public string type = null;
        /// <summary>
        /// InvoiceItem's unit price
        /// </summary>
        public string unitPrice = null;
        /// <summary>
        /// InvoiceItem's quantity
        /// </summary>
        public string quantity = null;
        /// <summary>
        /// InvoiceItem's description
        /// </summary>
        public string description = null;
        /// <summary>
        /// InvoiceItem's start time
        /// </summary>
        public string periodStartTime = null;
        /// <summary>
        /// InvoiceItem's end time
        /// </summary>
        public string periodEndTime = null;

        /// <summary>
        /// Invoice's ID
        /// </summary>
        private string invoiceId = null;
        /// <summary>
        /// InvoiceItem's ID
        /// </summary>
        private string id = null;

        /// <summary>
        /// Set Invoice's ID and endpoint
        /// </summary>
        /// <param name="invoiceId">Invoice's ID</param>
        /// <param name="id">InvoiceItem's ID</param>
        public InvoiceItem(string invoiceId, string id = null)
        {
            this.invoiceId = invoiceId;
            if (String.IsNullOrEmpty(this.invoiceId)) 
            {
                throw new Exception("invoiceId cannot be empty.");
            }
            if (!String.IsNullOrEmpty(id)) 
            {
                this.id = id;
            }
            this.setApiVersion("v2.1");
        }

        /// <summary>
        /// Create InvoiceItem
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.InvoiceItem item = new Rebilly.v2_1.InvoiceItem("invoiceId");
        ///     item.setApiKey("you api key");
        ///     item.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     item.type = Rebilly.v2_1.InvoiceItem.TYPE_DEBIT;
        ///     item.unitPrice = "5.99";
        ///     item.periodStartTime = "2015-01-01 00:00:00";
        ///     item.periodEndTime = "2015-02-01 00:00:00";
        ///     
        ///     RebillyResponse response = item.create();
        ///     if (response.statusCode == HttpStatusCode.Created) 
        ///     {
        ///         // Successfully created
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse create()
        {
            this.setApiController(INVOICE_END_POINT + this.invoiceId + ITEM_END_POINT);
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Get Items belong to an invoice
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.InvoiceItem item = new Rebilly.v2_1.InvoiceItem("invoiceId");
        ///     item.setApiKey("you api key");
        ///     item.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = item.retrieve();
        ///     if (response.statusCode == HttpStatusCode.OK) 
        ///     {
        ///         // see response
        ///         Console.WriteLine(response.getRawResponse());
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse retrieve()
        {
            this.setApiController(INVOICE_END_POINT + this.invoiceId + ITEM_END_POINT);

            return this.sendGetRequest();
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="item">InvoiceItem object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(InvoiceItem item)
        {
            string data = JsonConvert.SerializeObject(item, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
