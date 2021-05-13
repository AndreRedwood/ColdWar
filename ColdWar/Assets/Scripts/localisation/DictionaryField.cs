using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DictionaryField
{
	[SerializeField]
    string key;
	[SerializeField]
	string value;

	public string getKey()
	{
		return key;
	}

	public string getValue()
	{
		return value;
	}

	public DictionaryField(string keySet, string valueSet)
	{
		key = keySet;
		value = valueSet;
	}
}
