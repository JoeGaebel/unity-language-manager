using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : ScriptableObject {
	public List<Language> languages;
	public List<string> phrases;

	public void addLanguage(string name)
	{
		languages.Add(new Language(name, phrases));
	}

	public void addPhrase(string phrase){
		phrases.Add(phrase);
		foreach(Language language in languages){
			language.addPhrase (phrase, null);
		}
	}

	public void removePhrase(string phrase){
		phrases.Remove(phrase);
		foreach(Language language in languages){
			language.removePhrase (phrase);
		}
	}

	public bool removeLanguage(string name)
	{
		foreach(Language language in languages)
		{
			if (language.name == name)
			{
				languages.Remove(language);
				return true;
			}
		}
		return false;
	}

	public List<Language> getLanguages()
	{
		return languages;
	}
}
