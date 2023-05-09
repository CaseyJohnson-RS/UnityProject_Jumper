using System.Collections.Generic;
using UnityEngine;

public static class ServiceProvider
{

    private readonly static List<object> services = new();

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    public static void AddService(object service)
    {

        services.Add(service);

    }

    public static void RemoveService(object service)
    {

        services.Remove(service);

    }

    public static T GetService<T>()
    {
        foreach(var service in services)
        {
            if( service is T) return (T)service;
        }

        Debug.Log("������ " + typeof(T).FullName + " �� �������!");

        return default(T);
    }

}
