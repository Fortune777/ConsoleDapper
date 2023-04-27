using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleDapper.Model
{
    public class UserRepository: GenericRepository<User>
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["production"].ConnectionString;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                users = db.Query<User>("SELECT * FROM Users").ToList();
            }

            return users;
        }

        public User Get(int id)
        {
            User user = null;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                user = db.Query<User>("SELECT * FROM Users WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return user;
        }

        public User Create(User user)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                user.Id = userId.Value;
            }
            return user;
        }

        public void Update(User user)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE Users SET Name = @Name, Age = @Age WHERE Id = @Id";
                db.Execute(sqlQuery, user);
            }
        }


        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Users WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public void CreateTable()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"   
                    CREATE TABLE [Orders]
                    (
                    	[OrderID] [INT] IDENTITY(1,1) NOT NULL,
                    	[Number] [VARCHAR](20) NULL,
                    	[TotalPrice] [Money] NULL,
                    	[TotalQuantity] [int] NULL,
                    	CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
                    	(
                    		[OrderID] ASC
                    	)
                    ) ";
                    command.ExecuteNonQuery();
                }
            }
        }

        

        public Task DeleteRowAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveRangeAsync(IEnumerable<User> list)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User t)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(User t)
        {
            throw new NotImplementedException();
        }
    }
}
