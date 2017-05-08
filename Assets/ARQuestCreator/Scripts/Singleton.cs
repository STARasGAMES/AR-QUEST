using UnityEngine;
using System.Collections;

namespace ARQuestCreator{
    
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

		protected static T _instance;
		public static T Instance{
			get{ 
				if (_instance)
					return _instance;
				if (!_tryToFindInstance && Application.isPlaying) {
	//				Debug.Log ("[Singletone] Instance of '"+typeof(T)+" already destroyed on application quit. (Nothing wrong)");
					return null;
				}
				if (!(_instance = (T)FindObjectOfType (typeof(T)))) {
					Debug.LogError ("[Singletone] No Instance of '"+typeof(T)+"' already created!!!");
					return null;
				}
                if (Application.isPlaying)
				    _instance.SendMessage ("Awake", SendMessageOptions.DontRequireReceiver);
				return _instance;
			}
		}

		private static bool _tryToFindInstance = true;

		void Awake(){
//			Debug.Log ("[Singletone] Awake"+"  ("+typeof(T)+")");
			CheckInstance ();
			_tryToFindInstance = true;
		}

		void OnEnable(){
//			Debug.Log ("[Singletone] OnEnable"+"  ("+typeof(T)+")");
			CheckInstance ();
			_tryToFindInstance = true;
		}
			
//		void OnDisable(){
//			Debug.Log ("[Singletone] OnDisable"+"  ("+typeof(T)+")");
//			_tryToFindInstance = false;
//		}

		void OnApplicationQuit(){
//			Debug.Log ("[Singletone] OnApplicationQuit"+"  ("+typeof(T)+")");
			_tryToFindInstance = false;
		}

		void CheckInstance(){
            if (_instance && _instance != this)
            {
                Debug.Log("[Singletone] Already exist instance of '" + typeof(T) + "' ! Destroy this exemplar.");
                GameObject.Destroy(this.gameObject);
            }
		}
	}
}
