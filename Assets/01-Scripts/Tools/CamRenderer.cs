using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Camera))]
public class CamRenderer : MonoBehaviour {
#if UNITY_EDITOR
	[SerializeField] private int width = 10;
	[SerializeField] private int height = 10;
	[SerializeField] private string path = Application.dataPath;
	
	private void RenderView(string path) {
		RenderTexture rnd = new RenderTexture(width, height, 16);
		Camera cam = GetComponent<Camera>();
		cam.targetTexture = rnd;
		
		cam.Render();
		RenderTexture.active = rnd;
		
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB48, false);
		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();
		
		RenderTexture.active = null;

		string dir = Path.GetDirectoryName(path);
		if (!Directory.Exists(dir)) {
			Directory.CreateDirectory(dir);
		}

		if (File.Exists(path)) {
			File.Delete(path);
		}
		
		File.WriteAllBytes(path, tex.EncodeToJPG());
		cam.targetTexture = null;
		AssetDatabase.Refresh();
	}
	
	public void OnValidate() {
		width = Mathf.Max(10, width);
		height = Mathf.Max(10, height);
	}


	[CustomEditor(typeof(CamRenderer))]
	private class CamRendererEditor : Editor {
		private CamRenderer _instance;

		private void OnEnable() {
			_instance = (CamRenderer)target;
		}

		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			if (GUILayout.Button("RenderView")) {
				string path =
					EditorUtility.SaveFilePanel("Save camera render",  _instance.path, "CameraRender", "jpg");
				if (!string.IsNullOrEmpty(path)) {
					_instance.path = path;
					_instance.RenderView(path);
				}
			}
		}
	}
#endif
}
