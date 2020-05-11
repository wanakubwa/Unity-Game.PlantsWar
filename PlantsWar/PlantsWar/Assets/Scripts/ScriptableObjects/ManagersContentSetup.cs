using JetBrains.Annotations;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ManagersContentSetup.asset", menuName = "Settings/ManagersContentSetup")]
public class ManagersContentSetup : ScriptableObject
{
    #region Fields

    private static ManagersContentSetup instance;

    [SerializeField]
    private List<ManagerElement> managersCollection = new List<ManagerElement>();

    #endregion

    #region Propeties

    public static ManagersContentSetup Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<ManagersContentSetup>("Setups/ManagersContentSetup");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public List<ManagerElement> ManagersCollection { 
        get => managersCollection; 
        private set => managersCollection = value; 
    }

    #endregion

    #region Methods

#if UNITY_EDITOR

    public void RefreshManagersCollection(List<Type> managersTypes)
    {
        if(ManagersCollection == null)
        {
            foreach (Type managerType in managersTypes)
            {
                ManagersCollection.Add(new ManagerElement(managerType.ToString()));
            }
        }
        else
        {
            foreach (Type managerType in managersTypes)
            {
                if (ManagersCollection.Exists(x => x.ManagerType == managerType.ToString()) == false)
                {
                    ManagersCollection.Add(new ManagerElement(managerType.ToString()));
                }
            }

            foreach (ManagerElement element in ManagersCollection)
            {
                if(managersTypes.Exists(x => x.ToString() == element.ManagerType) == false)
                {
                    ManagersCollection.Remove(element);
                }
            }
        }
    }

#endif

    private void OnEnable()
    {

#if UNITY_EDITOR
        List<Type> managersTypes = GetManagerTypesInAssembly();
        Debug.Log(managersTypes.Count);
        RefreshManagersCollection(managersTypes);
#endif

    }

    private static List<Type> GetManagerTypesInAssembly()
    {
        Type managerBaseType = typeof(ManagerSingletonBase<>);

        var lookup = typeof(ManagerSingletonBase<>);
        List<Type> output = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsInheritedFrom(lookup))
            .ToList();

        return output;
    }

    #endregion

    #region Handlers



    #endregion

    [Serializable]
    public class ManagerElement
    {
        #region Fields

        [SerializeField, Sirenix.OdinInspector.ReadOnly]
        private string managerType;
        [SerializeField]
        private string localizedKey = string.Empty;

        #endregion

        #region Propeties

        public string ManagerType { 
            get => managerType; 
            private set => managerType = value; 
        }

        public string LocalizedKey { 
            get => localizedKey; 
            private set => localizedKey = value; 
        }

        #endregion

        #region Methods

        public ManagerElement(string type)
        {
            ManagerType = type;
        }

        #endregion

        #region Handlers



        #endregion
    }
}
