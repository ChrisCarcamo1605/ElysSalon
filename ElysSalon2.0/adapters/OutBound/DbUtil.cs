using System.Collections.ObjectModel;
using System.Windows;
using ElysSalon2._0.aplication.DTOs;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.domain.Entities;
using Microsoft.Data.SqlClient;

namespace ElysSalon2._0.adapters.OutBound {
    public class DbUtil {
        private static DbUtil instance;
        private static readonly object _lock = new object();
        private static SqlConnection connection;


        private DbUtil(){
            connection = new SqlConnection(SecretManager.GetValue("userCon"));
            connection.Open();
        }


        public static DbUtil getInstance(){
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new DbUtil();
                }
            }

            return instance;
        }

        public ObservableCollection<T> getFromDB<T>(string table, string column, Func<SqlDataReader, T> mapFunction){


            
            SqlCommand cmd = new SqlCommand($"SELECT {column} FROM {table}", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            ObservableCollection<T> results = new ObservableCollection<T>();
            int i = 0;
            while (reader.Read())
            {
                results.Add(mapFunction(reader));
                Console.WriteLine(results[i]);
                i++;
            }


            return results;
        }


        public object getFromDB(int id, string table, string column, Func<SqlDataReader, object> mapFunction){
            SqlCommand cmd = new SqlCommand($"SELECT {column} FROM {table} where {column} = {id};", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            object result = null;
            if (reader.Read())
            {
                result = mapFunction(reader);
            }

        
            MessageBox.Show(result.ToString());
            return result;
        }


        public void CloseConnection(){
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}