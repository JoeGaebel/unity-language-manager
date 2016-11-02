using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

public class LanguageManagerWindow : EditorWindow {

	private LanguageManager languageManager;
	private string newLanguageName = "New language name";
	private string phraseName = "new phrase name";
	private DictionaryWithDefault<string, string> textFields;

	private GUIStyle headerStyle;

	public LanguageManagerWindow(){
		headerStyle = new GUIStyle();
	}

	[MenuItem("Window/Language Manager", false, 1)]
	static void Init(){
		EditorWindow.GetWindow (typeof(LanguageManagerWindow));
	}

	void onEnable(){
		
		if(EditorPrefs.HasKey("ObjectPath")) 
		{
			string objectPath = EditorPrefs.GetString("ObjectPath");
			languageManager = AssetDatabase.LoadAssetAtPath (objectPath, typeof(LanguageManager)) as LanguageManager;
		}
	}

	void OnGUI(){
		headerStyle.fontSize = 15;

		if (textFields == null){
			textFields = new DictionaryWithDefault<string, string> ("");
			initializeFieldDictionary();
		}

		if (languageManager != null) {
			//Phrases
			EditorGUILayout.LabelField ("Phrases", headerStyle);
			GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});

			//Add Phrase
			EditorGUILayout.LabelField ("Add a phrase");
			GUILayout.BeginHorizontal("box");
			phraseName = EditorGUILayout.TextField(phraseName);
			if(GUILayout.Button("Add")) 
			{
				languageManager.addPhrase(phraseName);
			}
			GUILayout.EndHorizontal();

			//Enumerate phrases
			EditorGUILayout.LabelField ("Your phrases");
			foreach(string phrase in languageManager.phrases){
				GUILayout.BeginHorizontal("box");
				EditorGUILayout.LabelField (phrase);
				if(GUILayout.Button("X"))
				{
					languageManager.removePhrase(phrase);
				}
				GUILayout.EndHorizontal ();
			}

			//Languages
			EditorGUILayout.LabelField ("Languages", headerStyle);
			GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});

			//Add Language
			EditorGUILayout.LabelField ("Add a Language");
			newLanguageName = EditorGUILayout.TextField(newLanguageName);
			if(GUILayout.Button("Create language"))
			{
				languageManager.addLanguage (newLanguageName);
				languageManager.setActiveLanguage (newLanguageName);
			}

			//List Languages
			EditorGUILayout.LabelField ("Your Languages");

			foreach(Language language in languageManager.languages){
				GUILayout.BeginHorizontal("box");
				EditorGUILayout.LabelField (language.name);
				if(GUILayout.Button("X"))
				{
					languageManager.removeLanguage(language.name);
				}
				if(GUILayout.Button("Make active"))
				{
					languageManager.setActiveLanguage(language.name);
				}
				GUILayout.EndHorizontal();

				Dictionary<string, string> dictionary = language.getDictionary();

				//Enumerate the phrases
				if (dictionary != null){
					GUILayout.BeginVertical("box");
					
					foreach(string phrase in languageManager.phrases ){
						GUILayout.BeginHorizontal("box");
						EditorGUILayout.LabelField (phrase);
						textFields[language.name+phrase] = EditorGUILayout.TextField(textFields[language.name+phrase]);
						if(GUILayout.Button("Save"))
						{
							language.addPhrase(phrase, textFields[language.name+phrase]);
						}
						GUILayout.EndHorizontal();
					}
					GUILayout.EndVertical ();
				}
			}

			// Active Language things
			EditorGUILayout.LabelField ("Active Language", headerStyle);
			GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});
			EditorGUILayout.LabelField ("Current active language is: " + languageManager.activeLanguage.name);

		}
		else {
			if (GUILayout.Button("Open Langauge Data")) 
			{
				OpenItemList();
			}
		}
	}

	void OpenItemList () 
	{
		string absPath = EditorUtility.OpenFilePanel ("Select Inventory Item List", "", "");
		if (absPath.StartsWith(Application.dataPath)) 
		{
			string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
			languageManager = AssetDatabase.LoadAssetAtPath (relPath, typeof(LanguageManager)) as LanguageManager;
			if (languageManager.phrases == null && languageManager.languages == null ) {
				languageManager.languages = new List<Language>();
				languageManager.phrases = new List<string>();
			}
			if (languageManager) {
				EditorPrefs.SetString("ObjectPath", relPath);
			}
		}
	}

	void initializeFieldDictionary(){
		if (textFields != null) {
			foreach(Language language in languageManager.languages){
				foreach(string phrase in languageManager.phrases){
					if (language.getPhrase(phrase) != null){
						textFields[language.name+phrase] = language.getPhrase(phrase);
					}
				}
			}
		}
	}
}

public class DictionaryWithDefault<TKey, TValue> : Dictionary<TKey, TValue>
{
	TValue _default;
	public TValue DefaultValue {
		get { return _default; }
		set { _default = value; }
	}
	public DictionaryWithDefault() : base() { }
	public DictionaryWithDefault(TValue defaultValue) : base() {
		_default = defaultValue;
	}
	public new TValue this[TKey key]
	{
		get { 
			TValue t;
			return base.TryGetValue(key, out t) ? t : _default;
		}
		set { base[key] = value; }
	}
}