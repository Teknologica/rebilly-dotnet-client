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
    public class RebillyToken : RebillyRequest
    {

        public const string TOKEN_URL = "tokens/";

        /// <summary>
        /// string pan Primary Account Number
        /// </summary>
        public string pan = null;
        /// <summary>
        /// string expMonth expiry month
        /// </summary>
        public string expMonth;
        /// <summary>
        /// string expYear expiry year
        /// </summary>
        public string expYear;
        /// <summary>
        /// string cvv 3 or 4 digits security number
        /// </summary>
        public string cvv;
        /// <summary>
        /// string firstName first name
        /// </summary>
        public string firstName;
        /// <summary>
        /// string lastName last name
        /// </summary>
        public string lastName;
        /// <summary>
        /// string address address
        /// </summary>
        public string address;
        /// <summary>
        /// string address other address
        /// </summary>
        public string address2;
        /// <summary>
        /// string city city
        /// </summary>
        public string city;
        /// <summary>
        /// string region region
        /// </summary>
        public string region;
        /// <summary>
        /// string country county
        /// </summary>
        public string country;
        /// <summary>
        /// string phoneNumber phone number
        /// </summary>
        public string phoneNumber;
        /// <summary>
        /// string postalCode postal code
        /// </summary>
        public string postalCode;
        /// <summary>
        /// string token token
        /// </summary>
        public string token;

        public RebillyToken(string id = null)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.token = id;
            }
            this.setApiController(TOKEN_URL);
        }

        /// <summary>
        /// Get Token information
        /// </summary>
        /// <returns> RebillyResponse </returns>
        public RebillyResponse retrieve()
        {
            this.setApiController(TOKEN_URL + this.token);
            return this.sendGetRequest();
        }

        /// <summary>
        /// Create token
        /// </summary>
        /// <returns></returns>
        public RebillyResponse create()
        {
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Delete token
        /// </summary>
        /// <returns></returns>
        public RebillyResponse expire()
        {
            this.setApiController(TOKEN_URL + this.token);
            string data = "";

            return this.sendDeleteRequest(data);
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="customer">token object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(RebillyToken token)
        {
            string data = JsonConvert.SerializeObject(token, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
