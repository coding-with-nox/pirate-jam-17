using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
	private static T self;
	public static T I
	{
		get
		{
#if UNITY_EDITOR
			if (self == null)
				if (Application.isPlaying == false)
					self = (T)FindAnyObjectByType(typeof(T));
#endif
			return self;
		}
		protected set { self = value; }
	}

	protected virtual void Awake()
	{
		// If there is an instance, and it's not me, delete myself.
		if (I != null && I != this)
		{
			Debug.LogWarning($"There is already an instance of type {typeof(T)}");
			Destroy(this);
		}
		else
		{
			I = this as T;
		}
	}
}

