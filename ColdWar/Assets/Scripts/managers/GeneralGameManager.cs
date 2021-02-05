using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameManager : MonoBehaviour
{
	public mainMenuManager mainMenuManager;
	public BattleManager battleManager;

	public resourceManager resourceManager;

    // Start is called before the first frame update
    void Start()
    {
		DontDestroyOnLoad(this.gameObject);
    }

	public void giveBattleManager(BattleManager battleManagerTemp)
	{
		battleManager = battleManagerTemp;
		battleManager.initializeBattle(resourceManager, 0, 0);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
