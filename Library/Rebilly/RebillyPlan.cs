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
    public class RebillyPlan : RebillyRequest
    {
        public const string PLAN_URL = "plans/";
        /// <summary>
        /// string id
        /// </summary>
        private string id = null;
        /// <summary>
        /// bool isActive
        /// </summary>
        public bool isActive = true;
        /// <summary>
        /// string name
        /// </summary>
        public string name = null;
        /// <summary>
        /// string currency
        /// </summary>
        public string currency = null;
        /// <summary>
        /// string description
        /// </summary>
        public string description = null;
		/// <summary>
        /// string richDescription
        /// </summary>
        public string richDescription = null;
        /// <summary>
        /// string recurringAmount
        /// </summary>
        public string recurringAmount = null;
        /// <summary>
        /// string recurringIntervalUnit
        /// </summary>
        public string recurringPeriodUnit = null;
        /// <summary>
        /// string recurringIntervalLength
        /// </summary>
        public string recurringPeriodLength = null;
        /// <summary>
        /// string trialAmount
        /// </summary>
        public string trialAmount = null;
        /// <summary>
        /// string trialIntervalUnit
        /// </summary>
        public string trialPeriodUnit = null;
        /// <summary>
        /// string trialIntervalLength
        /// </summary>
        public string trialPeriodLength = null;
        /// <summary>
        /// string setupAmount
        /// </summary>
        public string setupAmount = null;
        /// <summary>
        /// string expireTime
        /// </summary>
        public string expireTime = null;
        /// <summary>
        /// string contractTermUnit
        /// </summary>
        public string contractTermUnit = null;
        /// <summary>
        /// string contractTermLength
        /// </summary>
        public string contractTermLength = null;
        /// <summary>
        /// string contractRebill
        /// </summary>
        public string recurringPeriodLimit = null;

        /// <summary>
        /// Contructor set the right API URL
        /// </summary>
        /// <param name="id"></param>
        public RebillyPlan(string id = null)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.id = id;
            }
            this.setApiController(PLAN_URL);
        }

        /// <summary>
        /// Create a new plan
        /// </summary>
        /// <returns>RebillyResponse</returns>
        public RebillyResponse create()
        {
            string data = this.buildRequest(this);

            return this.sendPostRequest(data);
        }

        public RebillyResponse update()
        {
            this.setApiController(PLAN_URL + this.id);
            string data = this.buildRequest(this);

            return this.sendPutRequest(data);
        }

        /// <summary>
        /// Delete a plan
        /// </summary>
        /// <returns>RebillyResponse</returns>
        public RebillyResponse delete()
        {
            this.setApiController(PLAN_URL + this.id);
            string data = this.buildRequest(this);

            return this.sendDeleteRequest(data);
        }

        /// <summary>
        /// Retrieve a plans
        /// </summary>
        /// <returns>RebillyResponse</returns>
        public RebillyResponse retrieve()
        {
            this.setApiController(PLAN_URL + this.id);

            return this.sendGetRequest();
        }

        /// <summary>
        /// List all plans
        /// </summary>
        /// <returns>RebillyResponse</returns>
        public RebillyResponse listAll()
        {
            return this.sendGetRequest();
        }

        /// <summary>
        /// Helper function to convert from object to JSON ready to send to Rebilly
        /// </summary>
        /// <param name="dispute">Subscription object</param>
        /// <returns>data in JSON format</returns>
        private string buildRequest(RebillyPlan plan)
        {
            string data = JsonConvert.SerializeObject(plan, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            return data;
        }
    }
}
