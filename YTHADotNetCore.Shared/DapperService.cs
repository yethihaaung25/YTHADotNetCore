using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTHADotNetCore.Shared
{
    public class DapperService
    {
        private readonly string _connectionStrings;

        public DapperService(string connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public List<T>Query<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionStrings);
            var item = db.Query<T>(query, param).ToList();
            return item;
        }

        public T QueryFirstOrDefault<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionStrings);
            var item = db.Query<T>(query, param).FirstOrDefault();
            return item;
        }

        public int Execute(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionStrings);
            var item = db.Execute(query, param);
            return item;
        }
    }
}
