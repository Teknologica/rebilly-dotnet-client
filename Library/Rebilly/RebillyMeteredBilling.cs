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
    public class RebillyMeteredBilling : RebillyRequest
    {   
        public string itemId = null;

        public string type = null;

        public string amount = null;

        public string quantity = null;

        public string description = null;
    }
}
