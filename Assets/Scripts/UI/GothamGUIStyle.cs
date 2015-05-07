using UnityEngine;
using System.Collections;

public class GothamGUIStyle {

    private static GUIStyle guiStyle;

    public static GUIStyle Style {
        get {
            if (guiStyle == null) {
                guiStyle = new GUIStyle();
                guiStyle.font = Resources.Load("Fonts/Gotham") as Font;
                guiStyle.fontSize = 20;
            }
            return guiStyle;
        }
    }
}
