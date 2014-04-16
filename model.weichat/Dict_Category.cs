using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.weichat
{
    public class Dict_Category : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int CategoryID { get; set; }
        public String CategoryName { get; set; }
        public int PID { get; set; }
        public int OrderID { get; set; }
        public int Exclude { get; set; }
    }
}
