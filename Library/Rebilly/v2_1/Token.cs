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
    public class Token : RebillyRequest
    {
        const string TOKEN_END_POINT = "tokens/";
        /// <summary>
        /// pan Primary Account Number
        /// </summary>
        public string pan = null;
        /// <summary>
        /// expMonth
        /// </summary>
        public string expMonth = null;
        /// <summary>
        /// expYear
        /// </summary>
        public string expYear = null;
        /// <summary>
        /// cvv
        /// </summary>
        public string cvv = null;
        /// <summary>
        /// firstName
        /// </summary>
        public string firstName = null;
        /// <summary>
        /// lastName
        /// </summary>
        public string lastName = null;
        /// <summary>
        /// address
        /// </summary>
        public string address = null;
        /// <summary>
        /// address2
        /// </summary>
        public string address2 = null;
        /// <summary>
        /// city
        /// </summary>
        public string city = null;
        /// <summary>
        /// region
        /// </summary>
        public string region = null;
        /// <summary>
        /// country - two letter code
        /// </summary>
        public string country = null;
        /// <summary>
        /// phoneNumber
        /// </summary>
        public string phoneNumber = null;
        /// <summary>
        /// postalCode
        /// </summary>
        public string postalCode = null;
        /// <summary>
        /// fingerprint
        /// </summary>
        public string fingerprint = null;
        /// <summary>
        /// Token's id
        /// </summary>
        private string id = null;

        /// <summary>
        /// Set api version and endpoint
        /// </summary>
        /// <param name="id"></param>        
        public Token(string id = null)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.id = id;
            }
            this.setApiVersion("v2.1");
            this.setApiController(TOKEN_END_POINT);
        }

        /// <summary>
        /// Create new token
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Token token = new Rebilly.v2_1.Token();
        ///     token.setApiKey("your api key");
        ///     token.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     token.pan = "4111111111111111";
        ///     token.expMonth = "12";
        ///     token.expYear = "2020";
        ///     token.cvv = "123";
        ///     token.firstName = "John";
        ///     token.lastName = "User";
        ///     token.address = "2020 main street";
        ///     token.city = "LA";
        ///     token.fingerprint = "123456789";
        ///     token.country = "US";
        ///     
        ///     RebillyResponse response = token.create();
        ///     if (response.statusCode == HttpStatusCode.Created)
        ///     {
        ///         // Success
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse create()
        {
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Get a token
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Token token = new Rebilly.v2_1.Token("tokenId");
        ///     token.setApiKey("your api key");
        ///     token.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = token.retrieve();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // Success
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse retrieve()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("Token cannot be empty.");
            }
            this.setApiController(TOKEN_END_POINT + this.id);

            return this.sendGetRequest();
        }

        /// <summary>
        /// Expire a token
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Token token = new Rebilly.v2_1.Token("tokenId");
        ///     token.setApiKey("your api key");
        ///     token.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = token.expire();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // Success
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse expire()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("Token cannot be empty.");
            }
            this.setApiController(TOKEN_END_POINT + this.id + "/expiration/");

            return this.sendPostRequest(null);
        }

        /// <summary>
        /// Helper RebillyResponse to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="token">Token object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(Token token)
        {
            string data = JsonConvert.SerializeObject(token, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
