using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateLanguageData {
	[MenuItem("Assets/Create/Language Data")]
	public static LanguageManager Create()
	{
		LanguageManager asset = ScriptableObject.CreateInstance<LanguageManager>();

		AssetDatabase.CreateAsset(asset, "Assets/LanguageData.asset");
		AssetDatabase.SaveAssets();
		return asset;
	}
}
