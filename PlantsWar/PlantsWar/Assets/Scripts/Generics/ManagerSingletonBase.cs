using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ManagerSingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
	#region Fields

	private static T instance;

	[SerializeField]
	private string fileName;

	#endregion
	#region Propeties
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (T)FindObjectOfType(typeof(T));
			}
			return instance;
		}
		private set
		{
			instance = value;
		}
	}

	public string FileName { 
		get => fileName; 
		private set => fileName = value; 
	}

	#endregion
	#region Methods

	public bool IsEnableOnScene(int sceneIndex)
	{
		ManagersContentSetup managersContentSetup = ManagersContentSetup.Instance;
		if(managersContentSetup == null)
		{
			return true;
		}

		SceneLabel label = managersContentSetup.GetSceneLabelByType(this.GetType());
		if((int)label == -1 || (int)label == sceneIndex)
		{
			return true;
		}

		return false;
	}

	protected virtual void Awake()
	{
		instance = GetComponent<T>();
	}

	protected virtual void Start() 
	{
		BrodcastEvents();
	}

	protected virtual void OnEnable()
	{
		AttachEvents();
	}

	protected virtual void OnDisable() 
	{
		DetachEvents();
	}

	protected virtual void AttachEvents()
	{

	}

	protected virtual void DetachEvents()
	{

	}

	protected virtual void BrodcastEvents()
	{

	}

	#endregion
	#region Handlers

	#endregion
}
