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
    public class Invoice : RebillyRequest
    {
        const string END_POINT = "invoices/";
        /// <summary>
        /// Customer's ID
        /// </summary>
        public string customer = null;
        /// <summary>
        /// Website's ID
        /// </summary>
        public string website = null;
        /// <summary>
        /// Currency three letter code
        /// </summary>
        public string currency = null;
        /// <summary>
        /// Invoice's Due time
        /// </summary>
        public string dueTime = null;
        /// <summary>
        /// Invoice's Issue time
        /// </summary>
        public string issuedTime = null;
        /// <summary>
        /// Invoice's billing contact
        /// </summary>
        public string billingContact = null;
        /// <summary>
        /// Invoice delivery contact
        /// </summary>
        public string deliveryContact = null;
        /// <summary>
        /// Invoice's ID
        /// </summary>
        private string id = null;

        /// <summary>
        /// Set api version and endpoint
        /// </summary>
        /// <param name="id"></param>
        public Invoice(string id = null)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.id = id;
            }
            this.setApiVersion("v2.1");
            this.setApiController(END_POINT);
        }

        /// <summary>
        /// Create invoice
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Invoice invoice = new Rebilly.v2_1.Invoice();
        ///     invoice.setApiKey("you api key");
        ///     invoice.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     invoice.customer = "CustABC123";
        ///     invoice.website = "WebABC123";
        ///     invoice.currency = "USD";
        ///
        ///     RebillyResponse response = invoice.create();
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
        /// Update invoice
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     // if invoice existed do update, create otherwise;
        ///     Rebilly.v2_1.Invoice invoice = new Rebilly.v2_1.Invoice("invoiceId");
        ///     invoice.setApiKey("you api key");
        ///     invoice.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     invoice.customer = "CustABC123";
        ///     invoice.website = "WebABC123";
        ///     invoice.currency = "USD";
        ///     invoice.dueTime = "2015-01-01 00:00:00";
        ///     
        ///     RebillyResponse response = invoice.update();
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
            this.setApiController(END_POINT + this.id);
            string data = this.buildRequest(this);

            return this.sendPutRequest(data);
        }

        /// <summary>
        /// Get an invoice
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Invoice invoice = new Rebilly.v2_1.Invoice("invoiceId");
        ///     invoice.setApiKey("you api key");
        ///     invoice.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = invoice.retrieve();
        ///     if (response.statusCode == HttpStatusCode.OK) 
        ///     {
        ///         // see response
        ///         Console.WriteLine(response.getRawResponse());
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse retrieve()
        {
            this.setApiController(END_POINT + this.id);

            return this.sendGetRequest();
        }

        /// <summary>
        /// Void an invoice
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Invoice invoice = new Rebilly.v2_1.Invoice("invoiceId");
        ///     invoice.setApiKey("you api key");
        ///     invoice.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = invoice.voidInvoice();
        ///     if (response.statusCode == HttpStatusCode.OK) 
        ///     {
        ///         // see response
        ///         Console.WriteLine(response.getRawResponse());
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse voidInvoice()
        {
            this.setApiController(END_POINT + this.id + "/void/");

            return this.sendPostRequest(null);
        }

        /// <summary>
        /// Abandon an invoice
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Invoice invoice = new Rebilly.v2_1.Invoice("invoiceId");
        ///     invoice.setApiKey("you api key");
        ///     invoice.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = invoice.abandon();
        ///     if (response.statusCode == HttpStatusCode.OK) 
        ///     {
        ///         // see response
        ///         Console.WriteLine(response.getRawResponse());
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse abandon()
        {
            this.setApiController(END_POINT + this.id + "/abandon/");

            return this.sendPostRequest(null);
        }

        /// <summary>
        /// Issue an invoice
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Invoice invoice = new Rebilly.v2_1.Invoice("invoiceId");
        ///     invoice.setApiKey("you api key");
        ///     invoice.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     invoice.issuedTime = "2015-01-01 00:00:00"; // default to NOW() if not supply
        ///     
        ///     RebillyResponse response = invoice.issue();
        ///     if (response.statusCode == HttpStatusCode.OK) 
        ///     {
        ///         // see response
        ///         Console.WriteLine(response.getRawResponse());
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse issue()
        {
            this.setApiController(END_POINT + this.id + "/issue/");
            string data = this.buildRequest(this);

            return this.sendPostRequest(null);
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="invoice">Invoice object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(Invoice invoice)
        {
            string data = JsonConvert.SerializeObject(invoice, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
