using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDungeonManager : MonoBehaviour
{
    public GameObject boss;
    public GameObject victoryPanel;

    // Update is called once per frame
    void Update()
    {
        if(boss == null)
        {
            OnFightWin();

        }
    }


    private void OnFightWin()
    {
        victoryPanel.SetActive(true);
    }
}
