using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using notcreepy.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using notcreepy.Models;
namespace notcreepy.Factory
{
    public class SubmissionFactory : IFactory<Submission>
    {
        private string connectionString;
        private readonly IOptions<MySqlOptions> mysqlConfig;

        public SubmissionFactory(IOptions<MySqlOptions> conf)
        {
            mysqlConfig = conf;
        }


        internal IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(mysqlConfig.Value.ConnectionString);
            }
        }

    }
}