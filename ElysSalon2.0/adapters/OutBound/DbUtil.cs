﻿using System.Collections.ObjectModel;
using System.Windows;
using ElysSalon2._0.aplication.Management;
using Microsoft.Data.SqlClient;

namespace ElysSalon2._0.adapters.OutBound;

public class DbUtil {
    private static DbUtil _instance;
    private static readonly object _lock = new();
    private static string _connectionString;


    private DbUtil(){
        _connectionString = SecretManager.GetValue("userCon");
    }


    public static DbUtil getInstance(){
        lock (_lock)
        {
            if (_instance == null) _instance = new DbUtil();
        }

        return _instance;
    }

    public ObservableCollection<T> GetFromDB<T>(string table, string column, Func<SqlDataReader, T> mapFunction){
        ObservableCollection<T> results = new ObservableCollection<T>();

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var cmd = new SqlCommand($"SELECT {column} FROM {table}", connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) results.Add(mapFunction(reader));
                    connection.Close();
                }
            }
        }

        return results;
    }


    public object GetFromDB(int id, string table, string column, Func<SqlDataReader, object> mapFunction){
        object result = null;

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var cmd = new SqlCommand($"SELECT {column} FROM {table} WHERE {column} = @id",
                       connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) result = mapFunction(reader);
                    connection.Close();
                }
            }
        }

        MessageBox.Show(result?.ToString() ?? "No se encontró el registro.");
        return result;
    }

    public void AddToDb<T>(string table, Dictionary<string, object> parameters){
        var columns = string.Join(", ", parameters.Keys);
        var parameterNames = string.Join(", ", parameters.Keys.Select(k => "@" + k));

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var query = $"INSERT INTO {table} ({columns}) VALUES ({parameterNames})";

            using (var cmd = new SqlCommand(query, connection))
            {
                // Add all parameters dynamically
                foreach (var param in parameters)
                    cmd.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }
    }

    public int GetIdFrom(string table, string columnId, string columnName, string value){
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var cmd = new SqlCommand($"SELECT {columnId} FROM {table} WHERE {columnName} = @value",
                       connection))
            {
                cmd.Parameters.AddWithValue("@value", value);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return reader.GetInt32(0);
                    connection.Close();
                }
            }
        }

        throw new RankException("No se encontró el registro.");
    }
}