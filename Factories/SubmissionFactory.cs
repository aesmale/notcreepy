using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using notcreepy.Models;
namespace notcreepy.Factory
{
    public class SubmissionFactory : IFactory<Submission>
    {
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

        public void Add(Submission item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO submissions (user_id, challenge_id, image, upvotes, downvotes, created_at, updated_at) VALUES (@user_id, @challenge_id, 0, 0, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }


        }

        public void Delete(Submission item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "DELETE FROM submissions WHERE id = {item.id}";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }


        }

        public IEnumerable<Submission> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Submission>("SELECT * FROM submissions");
            }
        }


        public IEnumerable<Submission> FindByUser(User item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Submission>("SELECT * FROM submissions WHERE user_id = {item.id}");
            }
        }


        public IEnumerable<Submission> FindByChallenge(Challenge item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Submission>("SELECT * FROM submissions WHERE challenege_id = {item.id}");
            }
        }



        public void Upvote(Submission item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "UPDATE submissions SET upvotes = {item.upvotes + 1} WHERE id = {item.id};";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }


        }

        public void Downvote(Submission item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "UPDATE submissions SET upvotes = {item.upvotes - 1} WHERE id = {item.id};";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }


        }
    }
}