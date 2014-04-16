using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class SysLog : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int LogID { get; set; }
        public String UserGUID { get; set; }
        public String UserName { get; set; }
        public DateTime Created { get; set; }
        public int LogLevel { get; set; }
        public String LogContent { get; set; }
        public String RemoteIP { get; set; }
    }
}
