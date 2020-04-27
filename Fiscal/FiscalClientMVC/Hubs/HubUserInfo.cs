using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiscalClientMVC.Hubs
{
    public class HubUserInfo
    {

        public HubUserInfo()
        {
            groups = new List<string>();
            connection_ids = new List<string>();
        }

        public string full_name { get; set; }
        public List<string> groups { get; set; }
        public List<string> connection_ids { get; set; }
        public string user_id { get; set; }
    }
}
