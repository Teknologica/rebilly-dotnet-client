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
    public class Organization : RebillyRequest
    {

        const string END_POINT = "organizations/";

        /// <summary>
        /// Organization's name
        /// </summary>
        public string name = null;
        /// <summary>
        /// Organization's address
        /// </summary>
        public string address = null;
        /// <summary>
        /// Organization's address
        /// </summary>
        public string address2 = null;
        /// <summary>
        /// Organization's city
        /// </summary>
        public string city = null;
        /// <summary>
        /// Organization's region
        /// </summary>
        public string region = null;
        /// <summary>
        /// Organization's postalCode
        /// </summary>
        public string postalCode = null;
        /// <summary>
        /// Organization's country
        /// </summary>
        public string country = null;
        /// <summary>
        /// Organization's id
        /// </summary>
        private string id = null;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id"></param>
        public Organization(string id = null)
        {
            this.setApiVersion("v2.1");
            if (!String.IsNullOrEmpty(id))
            {
                this.id = id;
            }
        }

        /// <summary>
        /// Create an organization
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Organization organization = new Rebilly.v2_1.Organization();
        ///     organization.setApiKey("your api key");
        ///     organization.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     organization.name = "My organization";
        ///     organization.address = "123 main street";
        ///     organization.city =  "New York";
        ///     organization.country =  "US";
        ///     organization.postalCode =  "12345";
        /// 
        ///     RebillyResponse response = organization.create();
        ///     if (response.statusCode == HttpStatusCode.Created)
        ///     {
        ///         // Successfully created
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse create()
        {
            this.setApiController(END_POINT);
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);

        }

        /// <summary>
        /// Update an organization
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Organization organization = new Rebilly.v2_1.Organization("organizationId");
        ///     organization.setApiKey("your api key");
        ///     organization.setEnvironment(RebillyRequest.ENV_SANDBOX);
        ///     organization.name = "My organization";
        ///     organization.address = "123 main street";
        ///     organization.city =  "New York";
        ///     organization.country =  "US";
        ///     organization.postalCode =  "12345";
        /// 
        ///     RebillyResponse response = organization.update();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // Successfully updated
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse update()
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new Exception("Organization id cannot be empty.");
            }
            this.setApiController(END_POINT + this.id);
            string data = this.buildRequest(this);

            return this.sendPutRequest(data);
        }

        /// <summary>
        /// Delete an organization
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Organization organization = new Rebilly.v2_1.Organization("organizationId");
        ///     organization.setApiKey("your api key");
        ///     organization.setEnvironment(RebillyRequest.ENV_SANDBOX);
        /// 
        ///     RebillyResponse response = organization.delete();
        ///     if (response.statusCode == HttpStatusCode.NoContent)
        ///     {
        ///         // Successfully deleted
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse delete()
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new Exception("Organization id cannot be empty.");
            }
            this.setApiController(END_POINT + this.id);
            string data = this.buildRequest(this);

            return this.sendDeleteRequest(data);
        }

        /// <summary>
        /// Get an organization
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Organization organization = new Rebilly.v2_1.Organization("organizationId");
        ///     organization.setApiKey("your api key");
        ///     organization.setEnvironment(RebillyRequest.ENV_SANDBOX);
        /// 
        ///     RebillyResponse response = organization.retrieve();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // Success
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse retrieve()
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new Exception("Organization id cannot be empty.");
            }
            this.setApiController(END_POINT + this.id);

            return this.sendGetRequest();
        }

        /// <summary>
        /// List all organizations
        /// </summary>
        /// <returns>RebillyResponse</returns>
        /// <example>
        /// <code>
        ///     Rebilly.v2_1.Organization organization = new Rebilly.v2_1.Organization();
        ///     organization.setApiKey("your api key");
        ///     organization.setEnvironment(RebillyRequest.ENV_SANDBOX);
        /// 
        ///     RebillyResponse response = organization.listAll();
        ///     if (response.statusCode == HttpStatusCode.OK)
        ///     {
        ///         // Success
        ///     }
        /// </code>
        /// </example>
        public RebillyResponse listAll()
        {
            this.setApiController(END_POINT);

            return this.sendGetRequest();
        }


        /// <summary>
        /// Helper RebillyResponse to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="organization">Organization object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(Organization organization)
        {
            string data = JsonConvert.SerializeObject(organization, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
