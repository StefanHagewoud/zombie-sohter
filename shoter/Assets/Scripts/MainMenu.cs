using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    public MenuItem[] menuItems;

    private void Awake()
    {
        Instance = this;
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
