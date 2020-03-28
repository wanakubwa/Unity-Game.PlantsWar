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

	#endregion
	#region Methods

	protected virtual void Awake()
	{
		instance = GetComponent<T>();
	}

	protected virtual void OnEnable()
	{
		AttachEvents();
	}

	protected virtual void OnDisable() 
	{
		DettachEvents();
	}

	protected virtual void AttachEvents()
	{

	}

	protected virtual void DettachEvents()
	{

	}

	#endregion
	#region Handlers

	#endregion
}
