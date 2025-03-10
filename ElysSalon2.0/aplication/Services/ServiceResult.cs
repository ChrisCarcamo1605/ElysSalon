using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElysSalon2._0.aplication.Services
{
    public class ServiceResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }


        private ServiceResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }


        public static ServiceResult successResult(string message = "operacion exitosa")
        {
            return new ServiceResult(true, message);
        }

        public static ServiceResult Failed(string message = "Error en la operacion")
        {
            return new ServiceResult(false, message);
        }
    }
}