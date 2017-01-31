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

        // public void Add_Submission(Submission item){
        //         using (IDbConnection dbConnection = Connection) {
        //         string query =  "INSERT INTO Submissions (follower_id, followee_id, created_at, updated_at) VALUES (@follower_id, @followee_id, NOW(), NOW())";
        //         dbConnection.Open();
        //         dbConnection.Execute(query, item);
        //     }

        // }
        // public List<Submission> FindFollowersByUser(User item){
        //     using (IDbConnection dbConnection = Connection)
        //     {
        //         dbConnection.Open();
        //         string query = "SELECT * FROM Submissions WHERE followee_id = {item.id} LEFT JOIN users on Submissions.follower_id = id";
        //         return dbConnection.Query<Submission, User, Submission>(query, (Submission, user) =>{
        //             Submission.follower = user;
        //             Submission.followee = item;
        //             return Submission;
        //         }).ToList();
        //     }  
        // }


    }
}