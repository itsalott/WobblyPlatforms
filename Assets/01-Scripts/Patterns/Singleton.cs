using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
	public const string DOUBLE_SINGLETON = ": Singleton already exists!";
		
	public static T Instance { get; private set; }

	protected virtual void Awake(){
		if (Instance != null) {
#if UNITY_EDITOR
			EditorGUIUtility.PingObject(gameObject);
#endif
			
			throw new Exception( typeof(T) + DOUBLE_SINGLETON);
		}

		Instance = this as T;
	}
}
