using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using static Dapper.SqlMapper;

namespace ConsoleDapper.Model
{
    public interface IGenericRepository<T> 
    {
        Task<IEnumerable<T>> FindAllAsync(string query);
        Task<T> FindAsync(string query);
        Task<IEnumerable<T>> GetAllAsync(string query);
        Task<T> GetAsync(T id);
        Task<int> UpdateRangeAsync(IEnumerable<T> list);
        Task UpdateAsync(T entity);
        Task InsertAsync(T entity);
        Task RemoveAsync(T entity);
    }

    public abstract class GenericRepository<T> : IGenericRepository<T> 
                                       where T : class
    {
        /// <summary>
        /// Generate new connection based on connection string 
        /// </summary>
        /// <returns></returns>
        private SqlConnection SqlConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["production"].ConnectionString);
        }


        /// <summary>
        /// Open new connection and return it for use
        /// </summary>
        /// <returns></returns>
        private IDbConnection CreateConnection()
        {
            IDbConnection connection = SqlConnection();
            connection.Open();
            return connection;
        }
        public async Task<IEnumerable<T>> GetAllAsync(string query)
        {
            string tName = typeof(T).Name;

            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<T>(query);
            }
        }

        //public async int DeleteRowAsync(T id)
        //{
        //    using (var connection = CreateConnection())
        //    {
        //        return await connection.QueryAsync($"DELETE FROM {nameof(T)} where id = {id} ");
        //    }
        //}

        public Task<T> GetAsync(T id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveRangeAsync(IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T t)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(T t)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRangeAsync(IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }
    }
} 