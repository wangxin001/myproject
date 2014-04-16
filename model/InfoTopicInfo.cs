using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class InfoTopicInfo : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int TopicID { get; set; }
        public int BoardID { get; set; }
        public String TopicTitle { get; set; }
        public String Publisher { get; set; }
        public DateTime PublishDate { get; set; }
        public bool ShowDate { get; set; }
        public String UserGUID { get; set; }
        public String UserName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Versions { get; set; }
        public int Status { get; set; }
        public String TopicContent { get; set; }
        public String TopicLink { get; set; }
        public int TopicOrder { get; set; }
        public int TopicType { get; set; }
    }
}
