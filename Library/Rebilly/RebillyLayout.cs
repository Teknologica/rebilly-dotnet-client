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
    public class RebillyLayout : RebillyRequest
    {
        public const string LAYOUT_URL = "layouts/";
        public const string ITEM_URL = "/items/";

        public string name = null;
        public Dictionary<string, string>[] items = null;

        private string id;
        /// <summary>
        /// Contructor set the right API URL
        /// </summary>
        /// <param name="id"></param>
        public RebillyLayout(string id = null)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.id = id;
            }
            this.setApiController(LAYOUT_URL);
        }

        /// <summary>
        /// Create a new layout
        /// </summary>
        /// <returns>RebillyResponse</returns>
        public RebillyResponse create()
        {
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        /// <summary>
        /// Update layout
        /// </summary>
        /// <returns>RebillyResponse</returns>
        public RebillyResponse update()
        {
            this.setApiController(LAYOUT_URL + this.id);
            string data = this.buildRequest(this);

            return this.sendPutRequest(data);
        }

        /// <summary>
        /// Update items in the layout
        /// </summary>
        /// <returns>RebillyResponse</returns>
        public RebillyResponse updateItems()
        {
            this.setApiController(LAYOUT_URL + this.id + ITEM_URL);
            string data = JsonConvert.SerializeObject(this.items);
            
            return this.sendPutRequest(data);
        }

        /// <summary>
        /// Delete all items belong to the layout
        /// </summary>
        /// <returns>RebillyResponse</returns>
        public RebillyResponse deleteItems()
        {
            this.setApiController(LAYOUT_URL + this.id + ITEM_URL);

            return this.sendDeleteRequest(null);
        }

        /// <summary>
        /// Retrieve a Layout
        /// </summary>
        /// <returns>RebillyResponse</returns>
        public RebillyResponse retrieve()
        {
            this.setApiController(LAYOUT_URL + this.id);
            return this.sendGetRequest();
        }

        /// <summary>
        /// Retrieve a an item by order
        /// </summary>
        /// <returns>RebillyResponse</returns>
        public RebillyResponse retrieveItem(int order)
        {
            this.setApiController(LAYOUT_URL + this.id + ITEM_URL + order);
            return this.sendGetRequest();
        }

        /// <summary>
        /// Retrieve all items belong to a layout
        /// </summary>
        /// <returns>RebillyResponse</returns>
        public RebillyResponse retrieveItems()
        {
            this.setApiController(LAYOUT_URL + this.id + ITEM_URL);
            return this.sendGetRequest();
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="dispute">Subscription object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(RebillyLayout layout)
        {
            string data = JsonConvert.SerializeObject(layout, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
