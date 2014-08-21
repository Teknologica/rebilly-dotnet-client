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
    public class RebillyThreeDSecure
    {
        public string enrolled = null;

        public string enrollmentEci = null;

        public string eci = null;

        public string cavv = null;

        public string xid = null;

        public string payerAuthResponseStatus = null;

        public string signatureVerification = null;

        public string amount = null;
    }
}
