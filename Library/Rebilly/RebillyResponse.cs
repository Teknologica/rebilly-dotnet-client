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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        public RebillyResponse(string response)
        {
            errors = new ArrayList();
            warnings = new ArrayList();
            this.response = response;
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
        /// Return raw response from Rebilly in JSON
        /// </summary>
        public string getResponse()
        {
            return response;
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
