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
    public class Blacklist : RebillyRequest
    {
        /// <summary>
        /// blacklist's type
        /// </summary>
        public const string TYPE_EMAIL = "email";
        public const string TYPE_CUSTOMERID = "customerId";
        public const string TYPE_IPADDRESS = "ipAddress";
        public const string TYPE_COUNTRY = "country";
        public const string TYPE_BIN = "bin";
        public const string TYPE_PAYMENTCARD = "paymentCard";
        /// <summary>
        /// API end point
        /// </summary>
        const string END_POINT = "blacklists/";
        /// <summary>
        /// Blacklist's type
        /// </summary>
        public string type = null;
        /// <summary>
        /// Blacklist's item
        /// </summary>
        public Dictionary<string, string> items = null;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="type"></param>
        public Blacklist()
        {
            this.setApiController(END_POINT);
            this.setApiVersion("v2.1");
        }

        /// <summary>
        /// Create a blacklist
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Blacklist blacklist = new Rebilly.v2_1.Blacklist();
        ///     blacklist.setApiKey("your api key");
        ///     blacklist.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     blacklist.type = Rebilly.v2_1.Blacklist.TYPE_EMAIL;
        ///     Dictionary<string, string> items = new Dictionary<string, string>()
        ///     {
        ///         {"value", "example@example.com"},
        ///         {"ttl", "36000"}, // optional
        ///     };
        /// 
        ///     blacklist.items = items;
        ///     RebillyResponse response = blacklist.create();
        ///     if (response.statusCode == HttpStatusCode.Created)
        ///     {
        ///         // Successfully created
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse create()
        {
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Delete a blacklist
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Blacklist blacklist = new Rebilly.v2_1.Blacklist();
        ///     blacklist.setApiKey("your api key");
        ///     blacklist.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     blacklist.type = Rebilly.v2_1.Blacklist.TYPE_EMAIL;
        ///     Dictionary<string, string> items = new Dictionary<string, string>()
        ///     {
        ///         {"value", "example@example.com"},
        ///     };
        /// 
        ///     blacklist.items = items;
        ///     RebillyResponse response = blacklist.delete();
        ///     if (response.statusCode == HttpStatusCode.NoContent)
        ///     {
        ///         // Successfully deleted
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse delete()
        {
            string data = this.buildRequest(this);

            return this.sendDeleteRequest(data);
        }

        /// <summary>
        /// List all blacklists
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Blacklist blacklist = new Rebilly.v2_1.Blacklist();
        ///     blacklist.setApiKey("your api key");
        ///     blacklist.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     blacklist.type = Rebilly.v2_1.Blacklist.TYPE_EMAIL;
        ///    
        ///     RebillyResponse response = blacklist.delete();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // see response
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse listAll()
        {
            this.setApiController(END_POINT + this.type);

            return this.sendGetRequest();
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="blacklist">Blacklist object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(Blacklist blacklist)
        {
            string data = JsonConvert.SerializeObject(blacklist, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
