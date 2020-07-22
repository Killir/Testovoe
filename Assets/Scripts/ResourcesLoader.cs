using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesLoader 
{

    public static GameObject LoadChip(string name)
    {
        return Resources.Load(name) as GameObject;
    }

}
