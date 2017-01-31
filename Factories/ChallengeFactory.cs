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
                string query =  "INSERT INTO challenges (name, created_at, updated_at) VALUES (@name, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public IEnumerable<Challenge> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Challenge>("SELECT * FROM challenges");
            }
        }
        public Challenge FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Challenge>("SELECT * FROM challenges WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }
    }
}