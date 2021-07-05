using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary
{
	[SerializeField]
	private List<DictionaryField> fields = new List<DictionaryField>();

	public void Add(string key, string value)
	{
		fields.Add(new DictionaryField(key, value));
	}

	public string getValue(string key)
	{
		foreach(DictionaryField field in fields)
		{
			if(field.getKey() == key)
			{
				return field.getValue();
			}
		}
		Debug.LogError("error 404, missing value");
		return "missing text";
	}
}
