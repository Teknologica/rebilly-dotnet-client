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
    public class Contact : RebillyRequest
    {
        const string END_POINT = "contacts/";
        /** @var  string $firstName */
        public string firstName = null;
        /** @var  string $lastName */
        public string lastName = null;
        /** @var  string $organization */
        public string organization = null;
        /** @var  string $address */
        public string address = null;
        /** @var  string $address2 */
        public string address2 = null;
        /** @var  string $city */
        public string city = null;
        /** @var  string $region */
        public string region = null;
        /** @var  string $country */
        public string country = null;
        /** @var  string $phoneNumber */
        public string phoneNumber = null;
        /** @var  string $postalCode */
        public string postalCode = null;

        private string id = null;
        
        /// <summary>
        /// Set api version and endpoint
        /// </summary>
        /// <param name="id"></param>
        public Contact(string id = null)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.id = id;
            }
            this.setApiVersion("v2.1");
            this.setApiController(END_POINT);
        }

        /// <summary>
        /// Create new contact
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Contact contact = new Rebilly.v2_1.Contact();
        ///     contact.setApiKey("your api key");
        ///     contact.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     contact.firstName = 'John';
        ///     contact.lastName = 'Doe';
        ///     contact.organization = 'Test Org';
        ///     contact.address = '2020 Rue test';
        ///     contact.city = 'City';
        ///     
        ///     RebillyResponse response = contact.create();
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
        /// Get a contact
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Contact contact = new Rebilly.v2_1.Contact("contactId");
        ///     contact.setApiKey("your api key");
        ///     contact.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     
        ///     RebillyResponse response = contact.retrieve();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // Successfully created
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse retrieve()
        {
            if (String.IsNullOrEmpty(this.id))
            {
                throw new Exception("contact id cannot be empty.");
            }
            this.setApiController(END_POINT + this.id);

            return this.sendGetRequest();
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="blacklist">Blacklist object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(Contact contact)
        {
            string data = JsonConvert.SerializeObject(contact, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
