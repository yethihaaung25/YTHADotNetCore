using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace YTHADotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string connectionStrings;
        public AdoDotNetService(string connectionStrings)
        {
            this.connectionStrings = connectionStrings;
        }

        public List<T> Query<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection con = new SqlConnection(connectionStrings);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            if(parameters is not null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name,item.Value)).ToArray());
            }
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            con.Close();

            string json = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json);
            return lst;
        }

        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection con = new SqlConnection(connectionStrings);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            if (parameters is not null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
            }
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            con.Close();

            string json = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json);
            T item = lst.Count == 0 ? default(T) : lst[0];
            return item;
        }

        public int Execute(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection con = new SqlConnection(connectionStrings);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            if (parameters is not null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
            }
            
            var result = cmd.ExecuteNonQuery();
            con.Close();

            
            return result;
        }
    }

    public class AdoDotNetParameter
    {
        public AdoDotNetParameter() { }
        public AdoDotNetParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}
