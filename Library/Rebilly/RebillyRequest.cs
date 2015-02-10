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
    public class RebillyRequest
    {
        private string apiUrl;
        
        /// <summary>
        /// ENV_STAGING and ENV_PRODUCTION are deprecated
        /// </summary>
        public const string ENV_STAGING = "staging";
        public const string ENV_PRODUCTION = "production";
        
        public const string ENV_LIVE = "live";
        public const string ENV_SANDBOX = "sandbox";

        Dictionary<string, string> urls = new Dictionary<string, string>()
        {
            {ENV_STAGING, "http://apix.rebilly.com/"},
            {ENV_PRODUCTION, "https://api.rebilly.com/"},
            {ENV_LIVE, "https://api.rebilly.com/"},
            {ENV_SANDBOX, "https://api-sandbox.rebilly.com/"},
        };

        /// <summary>
        /// Environment can be prod, staging, dev
        /// </summary>
        private string environment;
        /// <summary>
        /// controller
        /// </summary>
        private string controller;
        /// <summary>
        /// Unique API key for each user
        /// </summary>
        private string apiKey;
        /// <summary>
        /// Api version
        /// </summary>
        private string apiVersion = "v2";
        /// <summary>
        /// Method GET
        /// </summary>
        const string METHOD_GET = "GET";
        /// <summary>
        /// Method POST
        /// </summary>
        const string METHOD_POST = "POST";
        /// <summary>
        /// Method PUT
        /// </summary>
        const string METHOD_PUT = "PUT";
        /// <summary>
        /// Method DELETE
        /// </summary>
        const string METHOD_DELETE = "DELETE";

        /// <summary>
        /// Get api url
        /// </summary>
        /// <returns> API URL </returns>
        public String getApiUrl()
        {
            return this.apiUrl;
        }

        /// <summary>
        /// Set API key
        /// </summary>
        /// <param name="key"> key </param>
        public void setApiKey(string key)
        {
            this.apiKey = key;
        }

        /// <summary>
        /// Set Environment
        /// </summary>
        /// <param name="env"> env </param>
        public void setEnvironment(string env)
        {
            this.environment = env;
        }

        /// <summary>
        /// Set API URL
        /// </summary>
        /// <param name="url"> url </param>
        public void setApiUrl(string url)
        {
            this.apiUrl = url;
        }

        /// <summary>
        /// Set controller endpoint 
        /// </summary>
        /// <param name="controller"> controller </param>
        public void setApiController(string controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Set version 
        /// </summary>
        /// <param name="version"> version </param>
        public void setApiVersion(string version)
        {
            this.apiVersion = version;
        }

        /// <summary>
        /// Send GET request to Rebilly
        /// </summary>
        /// <returns> Response from Rebilly </returns>
        public RebillyResponse sendGetRequest()
        {
            return this.sendRequest(METHOD_GET);
        }

        /// <summary>
        /// Send POST request to Rebilly
        /// </summary>
        /// <param name="data"> Data that need to sent to Rebilly </param>
        /// <returns> Response from Rebilly </returns>
        public RebillyResponse sendPostRequest(string data)
        {
            return this.sendRequest(METHOD_POST, data);
        }

        /// <summary>
        /// Send PUT request to Rebilly
        /// </summary>
        /// <param name="data"> Data that need to sent to Rebilly </param>
        /// <returns> Response from Rebilly </returns>
        public RebillyResponse sendPutRequest(string data)
        {
            return this.sendRequest(METHOD_PUT, data);
        }

        /// <summary>
        /// Send DELETE request to Rebilly
        /// </summary>
        /// <param name="data"> Data that need to sent to Rebilly </param>
        /// <returns> Response from Rebilly </returns>
        public RebillyResponse sendDeleteRequest(string data)
        {
            return this.sendRequest(METHOD_DELETE, data);
        }

        /// <summary>
        /// Send request to Rebilly
        /// </summary>
        /// <param name="method"> Method or Verb </param>
        /// <param name="data"> Data that need to sent to Rebilly </param>
        /// <returns></returns>
        private RebillyResponse sendRequest(string method, string data = null)
        {
            if (String.IsNullOrEmpty(this.environment) || !urls.ContainsKey(this.environment))
            {
                throw new Exception("Please set the correct environment.");
            }

            this.apiUrl = urls[this.environment] + this.apiVersion + "/" + this.controller;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.apiUrl);
            request.Method = method;
            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";
            request.Headers["REB-APIKEY"] = this.apiKey;

            if (method != METHOD_GET) 
            {
                // turn our request string into a byte stream
                byte[] postBytes = Encoding.UTF8.GetBytes(data);
    
                request.ContentLength = postBytes.Length;
                
                Stream requestStream = request.GetRequestStream();

                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
            }
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return readWebResponse(response);
            }
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (StreamReader streamReader = new StreamReader(stream))
                {
                   return new RebillyResponse(streamReader.ReadToEnd());
                }
            }
        }

        /// <summary>
        /// Get Raw response from Rebilly and turn into and object easy to use.
        /// </summary>
        /// <param name="response"> Response from Rebilly</param>
        /// <returns> RebillyResponse Object </returns>
        private RebillyResponse readWebResponse(HttpWebResponse response)
        {
            string result;
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return new RebillyResponse(result, response.StatusCode);
        }
    }
}
