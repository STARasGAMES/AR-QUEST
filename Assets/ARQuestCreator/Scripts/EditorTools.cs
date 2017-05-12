using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;



	public static class EditorTools {
		
		[MenuItem ("Tools/Revert to Prefab %r")]
		static void Revert() {
			var selection = Selection.gameObjects;

			if (selection.Length > 0) {
				for (var i = 0; i < selection.Length; i++) {
					PrefabUtility.RevertPrefabInstance(selection[i]);
				}
			} else {
				Debug.Log("Cannot revert to prefab - nothing selected");
			}
		}
	}

#endif
