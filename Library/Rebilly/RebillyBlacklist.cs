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
    public class RebillyBlacklist : RebillyRequest
    {
        public const string BLACKLIST_URL    = "blacklists/";
        public const string TYPE_EMAIL       = "email";
        public const string TYPE_CUSTOMERID  = "customerId";
        public const string TYPE_IPADDRESS   = "ipAddress";
        public const string TYPE_COUNTRY     = "country";
        public const string TYPE_BIN         = "bin";
        public const string TYPE_PAYMENTCARD = "paymentCard";
        /// <summary>
        /// value for black or gray list
        /// </summary>
        public String value;
        /// <summary>
        /// time to set gray list
        /// </summary>
        public String ttl;
        /// <summary>
        /// primary account number
        /// </summary>
        public String pan;
        /// <summary>
        /// expiration's month
        /// </summary>
        public String expMonth;
        /// <summary>
        /// expiration's year
        /// </summary>
        public String expYear;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="type">type of black or gray list</param>
        public RebillyBlacklist(String type)
        {
            this.setApiController(BLACKLIST_URL + type);
        }

        /// <summary>
        /// create black or gray list
        /// </summary>
        /// <returns>Rebilly response</returns>
        public RebillyResponse create()
        {
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        /// <summary>
        /// delete black or gray list
        /// </summary>
        /// <returns>Rebilly response</returns>
        public RebillyResponse delete()
        {
            string data = this.buildRequest(this);

            return this.sendDeleteRequest(data);
        }

        /// <summary>
        /// Retrieve black or gray list
        /// </summary>
        /// <returns></returns>
        public RebillyResponse retrieve()
        {
            return this.sendGetRequest();
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="blacklist">blacklist object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(RebillyBlacklist blacklist)
        {
            string data = JsonConvert.SerializeObject(blacklist, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
