using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject towerChoosingMenu;
    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (towerChoosingMenu == isActiveAndEnabled)
        {
            towerChoosingMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
