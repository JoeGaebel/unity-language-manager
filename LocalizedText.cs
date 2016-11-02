using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

public class LocalizedText : MonoBehaviour {
	public Text textComponent;
	public string phrase;
	public LanguageManager languageManager;

	// Use this for initialization
	void Start () {
		string objectPath = EditorPrefs.GetString("ObjectPath");
		languageManager = AssetDatabase.LoadAssetAtPath (objectPath, typeof(LanguageManager)) as LanguageManager;
		textComponent = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		textComponent.text = languageManager.getActiveLanguagePhrase (phrase);
	}
}
