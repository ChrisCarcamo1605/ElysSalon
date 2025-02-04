﻿using System.Collections.ObjectModel;
using System.Windows;
using Windows.Media.Streaming.Adaptive;
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

        public void AddToDb<T>(string table, Dictionary<string, object> parameters)
        {
            string columns = string.Join(", ", parameters.Keys);
            string parameterNames = string.Join(", ", parameters.Keys.Select(k => "@" + k));

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"INSERT INTO {table} ({columns}) VALUES ({parameterNames})";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Add all parameters dynamically
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int getIdFrom(string table, string columnId, string columnName, string value)
        {
            object result = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand($"SELECT {columnId} FROM {table} WHERE {columnName} = @value",
                           connection))
                {
                    cmd.Parameters.AddWithValue("@value", value);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                    
                        if (reader.Read())
                        {
                           
                            return reader.GetInt32(0);
                        }
                        connection.Close();
                    }
                }
            }
           
            throw new RankException("No se encontró el registro.") ;
        }
    }
}