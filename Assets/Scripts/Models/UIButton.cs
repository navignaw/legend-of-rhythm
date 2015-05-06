using UnityEngine;
using System.Collections;

public enum ButtonAction
{
    PLAY,
    STORY1,
    STORY2,
    STORY3,
    MENU,
    QUIT
}

public class UIButton : MonoBehaviour
{
    public ButtonAction action;

    public void OnClick()
    {
        switch (action)
        {
            case ButtonAction.PLAY:
                int lvl = PlayerPrefs.GetInt("levelUnlocked");
                if (lvl >= 3)
                {
                    Application.LoadLevel("game");	// game scene
                }
                else
                {
                    Application.LoadLevel("tutorial" + (lvl + 1));
                }
                break;
            case ButtonAction.STORY1:
                Application.LoadLevel("story1");	// thread cutting tutorial
                break;
            case ButtonAction.STORY2:
                Application.LoadLevel("story2");	// boat tutorial
                break;
            case ButtonAction.STORY3:
                Application.LoadLevel("story3");	// menu tutorial
                break;
            case ButtonAction.MENU:
                Application.LoadLevel("title");	// load menu
                break;
            case ButtonAction.QUIT:
                Application.Quit();
                break;
        }
    }
}