﻿namespace ElysSalon2._0.domain.Services;

public class ResultFromService
{
    private ResultFromService(bool success, string message, object data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    private ResultFromService(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; private set; }
    public string Message { get; private set; }

    public object Data { get; private set; }


    public static ResultFromService SuccessResult(string message = "operacion exitosa")
    {
        return new ResultFromService(true, message);
    }

    public static ResultFromService SuccessResult(object data, string message = "operacion exitosa")
    {
        return new ResultFromService(true, message, data);
    }

    public static ResultFromService Failed(string message = "Error en la operacion")
    {
        return new ResultFromService(false, message);
    }

    public static ResultFromService Failed(object data = null, string message = "Error en la operacion")
    {
        return new ResultFromService(false, message, data);
    }
}