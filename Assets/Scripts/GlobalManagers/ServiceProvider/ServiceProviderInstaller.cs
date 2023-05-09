using UnityEngine;

public class ServiceProviderInstaller : MonoBehaviour
{

    public MonoBehaviour[] services;

    private void Awake()
    {
        foreach (object service in services)
        {
            ServiceProvider.AddService(service);
        }
    }

}
