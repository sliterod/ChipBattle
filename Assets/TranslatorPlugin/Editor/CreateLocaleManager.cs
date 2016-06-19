using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateLocaleManager : EditorWindow {

    [MenuItem("Localization/Create Locale Manager")]
    public static void Create()
    {
        CreateLocaleGameObject();
    }

    /// <summary>
    /// Creates Locale Manager GameObject
    /// </summary>
    static void CreateLocaleGameObject() {
        if (!GameObject.Find("LocaleManager"))
        {
            GameObject go = Instantiate(Resources.Load("LocaleManager", typeof(GameObject))) as GameObject;
            go.name = "LocaleManager";

            go.transform.position = Vector3.zero;

            SetWindowContent("Create Locale Manager",
                             "SUCCESS",
                             "LocaleManager object created on hierarchy.");
        }
        else {
            Debug.Log("GameObject already exists.");
            SetWindowContent("Create Locale Manager",
                             "INFORMATION",
                             "LocaleManager already exists on hierarchy.");
        }
    }

    /// <summary>
    /// Creates the content to display on information window
    /// </summary>
    /// <param name="title">Information window title</param>
    /// <param name="subtitle">Information window subtitle</param>
    /// <param name="info">Information window description</param>
    static void SetWindowContent(string title, string subtitle, string info)
    {
        WindowInfo.SetWindowInfo(title,
                                 subtitle,
                                 info);
    }
}
