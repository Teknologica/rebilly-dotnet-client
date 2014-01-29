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
    public class RebillyPaymentCard
    {
        /// <summary>
        /// customer's card number
        /// </summary>
        public string pan = null;
        /// <summary>
        /// last 4 card number
        /// </summary>
        public string last4 = null;
        /// <summary>
        /// card's expiration month
        /// </summary>
        public string expMonth = null;
        /// <summary>
        /// card's expiration year
        /// </summary>
        public string expYear = null;
        /// <summary>
        /// CVV 3 digit number at the back of the card.
        /// </summary>
        public string cvv = null;
        /// <summary>
        /// subscription
        /// </summary>
        public RebillySubscription subscription = null;
        /// <summary>
        /// use for one-time charge
        /// </summary>
        public RebillyCharge charge = null;
        /// <summary>
        /// use for creating card
        /// </summary>
        public RebillyCharge authorization = null;
        /// <summary>
        /// billing address related to card
        /// </summary>
        public RebillyAddressInfo billingAddress = null;

        public RebillyPaymentCard(string last4 = null)
        {
            if (!String.IsNullOrEmpty(last4) && last4.Length == 4)
            {
                this.last4 = last4;
            }
        }
    }
}
