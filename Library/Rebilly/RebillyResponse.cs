using System;
using System.Net;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Rebilly
{
    public class RebillyResponse
    {
        /**
        * API's response status
        */
        public const string STATUS_SUCCESS = "Success";
        public const string STATUS_FAIL = "Failure";
        public const string STATUS_VALID = "Valid";
        public const string STATUS_PRECONDITION = "Precondition failed";

        /**
        * API"s response action
        */
        public const string CUSTOMER_LOOKUP = "CUSTOMER_LOOKUP";
        public const string CUSTOMER_CREATE = "CUSTOMER_CREATE";
        public const string CUSTOMER_UPDATE = "CUSTOMER_UPDATE";
        public const string SUBSCRIPTION_LOOKUP = "SUBSCRIPTION_LOOKUP";
        public const string SUBSCRIPTION_CANCEL = "SUBSCRIPTION_CANCEL";
        public const string SUBSCRIPTION_CREATE = "SUBSCRIPTION_CREATE";
        public const string SUBSCRIPTION_UPDATE = "SUBSCRIPTION_UPDATE";
        public const string SUBSCRIPTION_SWITCH = "SUBSCRIPTION_SWITCH";
        public const string DISPUTE_ENTRY_CREATE = "DISPUTE_ENTRY_CREATE";
        public const string PAYMENT_CARD_CREATE = "PAYMENT_CARD_CREATE";
        public const string PAYMENT_CARD_UPDATE = "PAYMENT_CARD_UPDATE";
        public const string PAYMENT_CARD_LOOKUP = "PAYMENT_CARD_LOOKUP";
        public const string AUTHORIZE_PAYMENT_CARD = "AUTHORIZE_PAYMENT_CARD";
        public const string PAYMENT_CARD_CHARGE = "PAYMENT_CARD_CHARGE";
        public const string THREE_D_SECURE_CREATE = "THREE_D_SECURE_CREATE";
        public const string METERED_BILLING_CREATE = "METERED_BILLING_CREATE";
        public const string BLACKLIST_ENTRY_CREATE = "BLACKLIST_ENTRY_CREATE";
        public const string BLACKLIST_ENTRY_DELETE = "BLACKLIST_ENTRY_DELETE";
        public const string TOKEN_CREATE = "TOKEN_CREATE";
        public const string PLAN_CREATE = "PLAN_CREATE";
        public const string PLAN_DELETE = "PLAN_DELETE";
        public const string INVOICE_ITEM_CREATE = "INVOICE_ITEM_CREATE";

        /// <summary>
        /// raw response
        /// </summary>
        private string response = null;
        /// <summary>
        /// all errors
        /// </summary>
        private ArrayList errors = null;
        /// <summary>
        /// all warnings
        /// </summary>
        private ArrayList warnings = null;
        /// <summary>
        /// all transactions
        /// </summary>
        private ArrayList transactions = null;

        public HttpStatusCode statusCode;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        public RebillyResponse(string response, HttpStatusCode code = 0)
        {
            errors = new ArrayList();
            warnings = new ArrayList();
            this.response = response;
            this.statusCode = code;
            this.processResponse(response);
        }

        /// <summary>
        /// Take raw reponse and proccess it. If there are errors or warnings add to errors or warnings list
        /// </summary>
        /// <param name="response"> Raw response from Rebilly</param>
        private void processResponse(string response)
        {
            if (response.StartsWith("{") && response.EndsWith("}"))
            {
                JObject responseArray = JObject.Parse(response);
                foreach (KeyValuePair<string, JToken> property in responseArray)
                {
                    if (property.Key == "errors" && property.Value != null)
                    {
                        foreach (string error in property.Value)
                        {
                            errors.Add(error);
                        }
                    }
                    if (property.Key == "warnings" && property.Value != null)
                    {
                        foreach (string warning in property.Value)
                        {
                            warnings.Add(warning);
                        }
                    }

                    string type = property.Value.GetType().Name;
                    if (type == "JObject")
                    {
                        this.processResponse(property.Value.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Return errors list
        /// </summary>
        public ArrayList getErrors()
        {
            return this.errors;
        }

        /// <summary>
        /// Return warnings list
        /// </summary>
        public ArrayList getWarnings()
        {
            return this.warnings;
        }

        /// <summary>
        /// Return raw response from Rebilly
        /// </summary>
        public dynamic getRawResponse()
        {
            return JObject.Parse(response);
        }

        /// <summary>
        /// Check if response has errors
        /// </summary>
        /// <returns> boolean </returns>
        public bool hasErrors()
        {
            return (errors != null && errors.Count > 0);
        }

        /// <summary>
        /// Check if reponse has warnings
        /// </summary>
        /// <returns> boolean </returns>
        public bool hasWarnings()
        {
            return (warnings != null && warnings.Count > 0);
        }

        /// <summary>
        /// Check if response has transactions
        /// </summary>
        /// <returns> boolean </returns>
        public bool hasTransaction()
        {
            return (transactions != null && transactions.Count > 0);
        }
    }
}
