using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using notcreepy.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
namespace notcreepy.Factory
{
    public class ChallengeFactory : IFactory<Challenge>
    {
        private string connectionString;
        private readonly IOptions<MySqlOptions> mysqlConfig;

        public ChallengeFactory(IOptions<MySqlOptions> conf)
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

              public void Add(Challenge item)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  "INSERT INTO challenges (name, approved, created_at, updated_at) VALUES (@name, 0, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }

            public void Approve(Challenge item)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  "UPDATE challenges SET approved = 1 WHERE id = @id}";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }

        public IEnumerable<Challenge> FindAllApproved()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Challenge>("SELECT * FROM challenges WHERE approved = 1");
            }
        }


        public IEnumerable<Challenge> FindAllPending()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Challenge>("SELECT * FROM challenges WHERE approved = 0");
            }
        }



        public Challenge FindApprovedByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Challenge>("SELECT * FROM challenges WHERE id = @id AND approved = 1", new { Id = id }).FirstOrDefault();
            }
        }



        public Challenge FindPendingByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Challenge>("SELECT * FROM challenges WHERE id = @id AND approved = 0", new { Id = id }).FirstOrDefault();
            }
        }
    }
}