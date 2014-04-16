using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.weichat
{
    public class Dict_SiteInfo : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int SiteID { get; set; }
        public String SiteName { get; set; }
    }
}
