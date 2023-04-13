using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResource
{
    abstract void Load();
}

public class ResourceManager : MonoBehaviour
{
    public void LoadResource()
    {
        IResource resource = new RemoteResourceProxy();
        resource.Load();
    }
}
