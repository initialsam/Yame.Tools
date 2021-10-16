using Polly;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollyNote
{
    class darkthread
    {
        public void Test()
        {
            var cnStr = "fake";
            Policy
            .Handle<SqlException>(se => se.Number == 1205)
            .WaitAndRetry(new TimeSpan[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(4)
            })
            .Execute(() =>
            {
                using (var cn = new SqlConnection(cnStr))
                {
                    //cn.Execute("INSERT INTO ....");
                }
            });
        }

        public void Test2()
        { 
        }
    }
}
