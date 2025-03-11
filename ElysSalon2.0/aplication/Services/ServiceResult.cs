using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElysSalon2._0.aplication.Services
{
    public class ServiceResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public object Data { get; private set; }

        private ServiceResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        private ServiceResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }


        public static ServiceResult SuccessResult(string message = "operacion exitosa")
        {
            return new ServiceResult(true, message);
        }

        public static ServiceResult SuccessResult(object data, string message = "operacion exitosa")
        {
            return new ServiceResult(true, message, data);
        }

        public static ServiceResult Failed(string message = "Error en la operacion")
        {
            return new ServiceResult(false, message);
        }

        public static ServiceResult Failed(object data = null, string message = "Error en la operacion")
        {
            return new ServiceResult(false, message, data);
        }
    }
}