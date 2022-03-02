using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public static MenuManager Instance;
    public MenuItem[] menuItems;

    private void Awake()
    {
        Instance = this;
        SwitchMenu("Main");
    }
    public void SwitchMenu(string _name)
    {
        foreach (MenuItem mi in menuItems)
        {
            if (mi.menuItem == _name)
                mi.gameObject.SetActive(true);
            else
                mi.gameObject.SetActive(false);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
