using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalisationSystem
{
	private static string path = "Assets/localisation/";

	public enum Language {English, Polish}
	public static Language language = Language.English;
	
	private static SerializableDictionary localisedEN = new SerializableDictionary();
	private static SerializableDictionary localisedPL = new SerializableDictionary();

	public static bool isInit;
	
	public static void Init()
	{
		string json;
		json = File.ReadAllText(path + "en.json");
		localisedEN = JsonUtility.FromJson<SerializableDictionary>(json);
		json = File.ReadAllText(path + "pl.json");
		localisedPL = JsonUtility.FromJson<SerializableDictionary>(json);

		isInit = true;
	}

	public static string GetLocalisedValue(string key)
	{
		if (!isInit) { Init(); }

		string value = key;

		switch(language)
		{
			case Language.English:
				value = localisedEN.getValue(key);
				break;
			case Language.Polish:
				value = localisedPL.getValue(key);
				break;
		}

		return value;
	}
}
