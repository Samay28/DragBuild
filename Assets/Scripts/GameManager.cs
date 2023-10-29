using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isInventoryOpen;
    public GameObject InvPanel;
    public static GameManager instance;
    void Start()
    {   
        if(instance==null)
        {
            instance = this;
        }
        InvPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInventory();
        }
    }
    public void OpneInventory()
    {
        InvPanel.SetActive(true);
        isInventoryOpen = true;
    }
    public void CloseInventory()
    {
        InvPanel.SetActive(false);
        isInventoryOpen = false;
    }
}
