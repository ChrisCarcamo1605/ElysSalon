using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ElysSalon2._0.aplication.Management {
    using Microsoft.Extensions.Configuration;

    public class SecretManager
    {
        private static IConfiguration _configuration;

        static SecretManager()
        {
            var builder = new ConfigurationBuilder();

            builder.AddUserSecrets<SecretManager>();

            _configuration = builder.Build();
        }

        public static string GetValue(string key)
        {
            return _configuration[key];
        }
    }



}