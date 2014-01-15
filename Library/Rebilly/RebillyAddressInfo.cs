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
    public class RebillyAddressInfo
    {
        /// <summary>
        /// customer's first name
        /// </summary>
        public string firstName = null;
        /// <summary>
        /// customer's last name
        /// </summary>
        public string lastName = null;
        /// <summary>
        /// customer's address
        /// </summary>
        public string address = null;
        /// <summary>
        /// customer's address
        /// </summary>
        public string address2 = null;
        /// <summary>
        /// customer's city
        /// </summary>
        public string city = null;
        /// <summary>
        /// customer's region
        /// </summary>
        public string region = null;
        /// <summary>
        /// customer's country
        /// </summary>
        public string country = null;
        /// <summary>
        /// customer's phone number
        /// </summary>
        public string phoneNumber = null;
        /// <summary>
        /// customer's postal code
        /// </summary>
        public string postalCode = null;
    }
}
