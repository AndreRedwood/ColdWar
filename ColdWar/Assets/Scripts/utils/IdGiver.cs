using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdGiver
{
	private int[] usedIdList = new int[1000];
	
	public int giveId()
	{
		int value = Random.Range(1, 9);
		for (int i = 0; i < usedIdList.Length; i++)
		{
			int safeSwith = 0;
			if (i == 0) { value = Random.Range(1, 9); }
			if (value == usedIdList[i])
			{
				i = -1;
			}
			safeSwith++;
			if(safeSwith > 1000) { value = -1; break; }
		}
		if (value == -1) { Debug.LogError("not enought ID"); return -1; }
		for(int i=0;i<usedIdList.Length;i++)
		{
			if(usedIdList[i] == 0)
			{
				usedIdList[i] = value; break;
			}
		}
		return value;
	}
}
