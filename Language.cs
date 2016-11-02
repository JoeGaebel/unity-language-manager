using System.Collections.Generic;
using System;


[Serializable]
public class Language
{
	public List<string> phraseKeys;
	public List<string> phraseValues;

	public string name;

	public Language(string languageName, List<string> phrases)
	{
		name = languageName;
		phraseKeys = new List<string>();
		phraseValues = new List<string>();
		foreach(string phrase in phrases){
			addPhrase (phrase, null);
		}
	}

	public string getPhrase(string key)
	{
		if (phraseKeys.Contains(key))
		{
			int index = phraseKeys.FindIndex(a => a == key);
			return phraseValues[index];
		}
		else
		{
			return null;
		}
	}

	public void addPhrase(string key, string value)
	{
		if (phraseKeys.Contains(key))
		{
			int index = phraseKeys.FindIndex(a => a == key);
			phraseValues[index] = value;
		}
		else
		{
			phraseKeys.Add (key);
			phraseValues.Add (value);
		}
	}

	public void removePhrase(string key)
	{
		if (phraseKeys.Contains(key))
		{
			int index = phraseKeys.FindIndex(a => a == key);
			phraseKeys.Remove (key);
			string value = phraseValues [index];
			phraseValues.Remove(value);
		}
	}

	public Dictionary<string, string> getDictionary()
	{
		var dictionary = new Dictionary<string, string>();

		for (int index = 0; index < phraseKeys.Count; index++)
		{
			dictionary.Add(phraseKeys[index], phraseValues[index]);
		}

		return dictionary;
	}
}