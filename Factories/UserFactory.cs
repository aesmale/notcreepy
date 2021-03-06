using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using notcreepy.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
namespace notcreepy.Factory
{
    public class UserFactory : IFactory<User>
    {
        private readonly IOptions<MySqlOptions> mysqlConfig;

        public UserFactory(IOptions<MySqlOptions> conf)
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

        public void Add(User item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO users (username, email, password, admin, created_at, updated_at) VALUES (@Name, @Email, @Password, 0, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public IEnumerable<User> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM users");
            }
        }

        public IEnumerable<User> FindAllAdmins(){
                        using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM users WHERE admin = 1");
            }
        }


        public User FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM users WHERE id = @id", new { Id = id }).FirstOrDefault();
            }
        }



                public User FindByEmail(string email)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM users WHERE email = @email", new { email = email }).FirstOrDefault();
            }
        }

        public byte[] ConvertToBytes(IFormFile image)
        {
            byte[] CoverImageBytes = null;
            BinaryReader reader = new BinaryReader(image.OpenReadStream());
            CoverImageBytes = reader.ReadBytes((int)image.Length);
            return CoverImageBytes;
        }

        
        // public byte[] ConverToImage(btye[] image)
        // {
        //     IFormFile CoverImageBytes = null;
        //     BinaryReader reader = new BinaryReader(image.OpenReadStream());
        //     CoverImageBytes = reader.ReadBytes((int)image.Length);
        //     return CoverImageBytes;
        // }
    }
}