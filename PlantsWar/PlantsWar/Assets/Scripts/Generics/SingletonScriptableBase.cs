using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SingletonScriptableBase<T> : ScriptableObject where T : ScriptableObject
{
    static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance ==  null)
            {
                instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
            }

            return instance;
        }
    }
}

