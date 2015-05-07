using UnityEngine;
using System.Collections;

public enum ButtonAction
{
    STORYSELECT,
    STORY1,
    STORY2,
    STORY3,
    MENU,
    QUIT
}

public class UIButton : MonoBehaviour
{
    public ButtonAction action;

    public void OnMouseDown()
    {
        switch (action)
        {
            case ButtonAction.STORYSELECT:
                Application.LoadLevel("levelSelect");	// game scene
                break;
            case ButtonAction.STORY1:
                Application.LoadLevel("story1");	// thread cutting tutorial
                break;
            case ButtonAction.STORY2:
                Application.LoadLevel("story1");	// boat tutorial
                break;
            case ButtonAction.STORY3:
                Application.LoadLevel("story1");	// menu tutorial
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