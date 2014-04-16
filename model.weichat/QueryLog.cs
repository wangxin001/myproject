using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model.weichat
{
    public class QueryLog : OR.Model.Entity
    {
        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int LogID { get; set; }
        public String SessionID { get; set; }
        public String QueryMethod { get; set; }
        public String QueryParameter { get; set; }
        public DateTime QueryTime { get; set; }
        public long TimeLength { get; set; }

        public static void save(String sessionID, String method, String parameter, DateTime date, long length)
        {
            try
            {
                OR.Control.DAL.Add<model.weichat.QueryLog>(new model.weichat.QueryLog()
                    {
                        QueryMethod = method,
                        QueryParameter = parameter,
                        QueryTime = date,
                        SessionID = sessionID,
                        TimeLength = length
                    }, false);
            }
            catch (Exception) { }
        }
    }
}
