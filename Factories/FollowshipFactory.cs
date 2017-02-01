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
    public class FollowshipFactory : IFactory<Followship>
    {
        private readonly IOptions<MySqlOptions> mysqlConfig;

        public FollowshipFactory(IOptions<MySqlOptions> conf)
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

        public void Add_Followship(Followship item){
                using (IDbConnection dbConnection = Connection) {
                string query =  "INSERT INTO followships (follower_id, followee_id, created_at, updated_at) VALUES (@follower_id, @followee_id, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }

        }
        public List<Followship> FindFollowersByUser(User item){
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string query = "SELECT * FROM followships WHERE followee_id = {item.id} LEFT JOIN users on followships.follower_id = id";
                return dbConnection.Query<Followship, User, Followship>(query, (followship, user) =>{
                    followship.follower = user;
                    followship.followee = item;
                    return followship;
                }).ToList();
            }  
        }


    }
}