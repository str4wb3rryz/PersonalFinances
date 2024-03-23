using DBLayer;
using PersonalFinances.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinances.Repositories
{
    public class UserRepository
    {
        public static User GetUser(string username)
        {
            string sql = $"SELECT * FROM Users WHERE Username = '{username}'";
            return FetchUser(sql);
        }
        public static User GetUser(int id)
        {
            string sql = $"SELECT * FROM Users WHERE Id = {id}";
            return FetchUser(sql);
        }
        private static User FetchUser(string sql)
        {
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);
            User user = null;
            if (reader.HasRows == true)
            {
                reader.Read();
                user = CreateObject(reader);
                reader.Close();
            }
            DB.CloseConnection();
            return user;
        }
        private static User CreateObject(SqlDataReader reader)
        {
            int id = int.Parse(reader["Id"].ToString());
            string firstName = reader["FirstName"].ToString();
            string lastName = reader["LastName"].ToString();
            string username = reader["Username"].ToString();
            string password = reader["Password"].ToString();
            var user = new User
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password
            };
            return user;
        }
    }
}
