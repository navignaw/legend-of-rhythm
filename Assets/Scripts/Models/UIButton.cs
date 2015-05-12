using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum ButtonAction
{
    STORYSELECT,
    SONGSELECT,
    GAME,
    STORY1,
    STORY2,
    STORY3,
    MENU,
    QUIT
}

public class UIButton : MonoBehaviour
{
    public ButtonAction action;
    public int lockedLevel;

    void Start() {
        // disable tutorial buttons if not yet unlocked
        GetComponent<Button>().interactable = PlayerPrefs.GetInt("levelUnlocked", 0) >= lockedLevel;
    }

    public void OnMouseDown()
    {
        switch (action)
        {
            case ButtonAction.STORYSELECT:
                Application.LoadLevel("levelSelect"); // level select
                break;
            case ButtonAction.SONGSELECT:
                Application.LoadLevel("songSelect"); // song select
                break;
            case ButtonAction.GAME:
                Application.LoadLevel("game");        // game
                break;
            case ButtonAction.STORY1:
                Application.LoadLevel("story1");      // tutorial 1 (basic notes)
                break;
            case ButtonAction.STORY2:
                Application.LoadLevel("story2");      // tutorial 2 (basic rests)
                break;
            case ButtonAction.STORY3:
                Application.LoadLevel("story3");      // tutorial 3 (eighth notes, syncopation)
                break;
            case ButtonAction.MENU:
                Application.LoadLevel("titleScreen"); // load menu
                break;
            case ButtonAction.QUIT:
                Application.Quit();
                break;
        }
    }
}