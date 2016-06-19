using UnityEngine;
using UnityEditor;
using System.Collections;

public class WindowInfo : EditorWindow {

    static string windowInformation;
    static string windowTitle;
    static string windowSubtitle;

    /// <summary>
    /// Sets string information to display on window
    /// </summary>
    /// <param name="title">Window title</param>
    /// <param name="subtitle">Window subtitle</param>
    /// <param name="info">Window information</param>
    public static void SetWindowInfo(string title, string subtitle, string info) {
        windowInformation = info;
        windowTitle = title;
        windowSubtitle = subtitle;

        ShowWindow();
    }

    /// <summary>
    /// Shows information window on Editor
    /// </summary>
    static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(WindowInfo), true, windowTitle);
    }

    void OnGUI() {

        GUILayout.Label(windowSubtitle, EditorStyles.boldLabel);//Subtitle

        GUILayout.Label(windowInformation + "\n");//Information

        if (GUILayout.Button("Close window"))
        {
            this.Close();
        }
    }
}
