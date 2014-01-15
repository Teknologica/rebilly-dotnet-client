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
        /// <summary>
        /// string planId
        /// </summary>
        public string planId = null;
        /// <summary>
        /// string lookupPlanId
        /// </summary>
        public string lookupPlanId = null;
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
        /// string expireTime
        /// </summary>
        public string expireTime = null;
        /// <summary>
        /// string setupAmount
        /// </summary>
        public string setupAmount = null;
        /// <summary>
        /// string recurringAmount
        /// </summary>
        public string recurringAmount = null;
        /// <summary>
        /// string recurringIntervalUnit
        /// </summary>
        public string recurringIntervalUnit = null;
        /// <summary>
        /// string recurringIntervalLength
        /// </summary>
        public string recurringIntervalLength = null;
        /// <summary>
        /// string trialAmount
        /// </summary>
        public string trialAmount = null;
        /// <summary>
        /// string trialIntervalUnit
        /// </summary>
        public string trialIntervalUnit = null;
        /// <summary>
        /// string trialIntervalLength
        /// </summary>
        public string trialIntervalLength = null;
        /// <summary>
        /// string contractLength
        /// </summary>
        public string contractTerm = null;
        /// <summary>
        /// string contractRebill
        /// </summary>
        public string contractRebill = null;

        public const string PLAN_URL = "plans/";

        /// <summary>
        /// Contructor set the right API URL
        /// </summary>
        /// <param name="id"></param>
        public RebillyPlan(string id = null)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.lookupPlanId = id;
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

        /// <summary>
        /// Delete a plan
        /// </summary>
        /// <returns>RebillyResponse</returns>
        public RebillyResponse delete()
        {
            string data = this.buildRequest(this);

            return this.sendDeleteRequest(data);
        }

        /// <summary>
        /// Retrieve a plans
        /// </summary>
        /// <returns>RebillyResponse</returns>
        public RebillyResponse retrieve()
        {
            this.setApiController(PLAN_URL + this.lookupPlanId);

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
