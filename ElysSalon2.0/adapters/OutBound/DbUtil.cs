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
        private static string connectionString;


        private DbUtil(){
            connectionString = SecretManager.GetValue("userCon");
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
            ObservableCollection<T> results = new ObservableCollection<T>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand($"SELECT {column} FROM {table}", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(mapFunction(reader));
                        }
                        connection.Close();
                    }
                }
            }

            return results;
        }


        public object getFromDB(int id, string table, string column, Func<SqlDataReader, object> mapFunction){
            object result = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand($"SELECT {column} FROM {table} WHERE {column} = @id",
                           connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = mapFunction(reader);
                        }
                        connection.Close();
                    }
                }
            }

            MessageBox.Show(result?.ToString() ?? "No se encontró el registro.");
            return result;
        }
    }
}