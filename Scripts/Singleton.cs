using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuillAnim {
public abstract class SingletonObject<T> : MonoBehaviour where T : MonoBehaviour {
	private static object threadLock = new object();
	private static T _instance;

	protected static void CreateIfNotInstantiated() {
		if(!_instance) {
			var singleton_root = GameObject.Find("SingletonContainer");
			if(!singleton_root) {
				singleton_root = new GameObject("SingletonContainer");
			}

			var obj = new GameObject(typeof(T).ToString());
			obj.transform.parent = singleton_root.transform;
			_instance = obj.AddComponent<T>();
		}
	}

	public static void EnsureExists() {
		lock(threadLock) {
			CreateIfNotInstantiated();
		}
	}

	public static T instance {
		get {
			lock(threadLock) {
				CreateIfNotInstantiated();
				return _instance;
			}
		}
	}
}
}
