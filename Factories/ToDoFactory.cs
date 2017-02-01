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
    public class ToDoFactory : IFactory<ToDo>
    {
        private readonly IOptions<MySqlOptions> mysqlConfig;

        public ToDoFactory(IOptions<MySqlOptions> conf)
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

        public void Add(ToDo item)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  "INSERT INTO ToDos (name, approved, created_at, updated_at) VALUES (@name, 0, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }

        public void Delete(ToDo item){
                using (IDbConnection dbConnection = Connection) {
                string query =  "DELETE FROM todos WHERE id = @id";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }

        public IEnumerable<ToDo> AssignedToUser(User item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ToDo>("SELECT * FROM todos WHERE challengee = @id");
            }
        }


        public IEnumerable<ToDo> AssignedToUserbyUser(User item, User assigner)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ToDo>("SELECT * FROM todos WHERE challengee = {item.id} AND challenger = {assigner.id};");
            }
        }

    }
}