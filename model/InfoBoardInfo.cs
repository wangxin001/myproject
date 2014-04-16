using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class InfoBoardInfo : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int BoardID { get; set; }
        public String BoardTitle { get; set; }
        public String DisplayName { get; set; }
        public int BoardType { get; set; }
        public DateTime Created { get; set; }
        public String UserGUID { get; set; }
        public String UserName { get; set; }
        public int Status { get; set; }
    }
}
