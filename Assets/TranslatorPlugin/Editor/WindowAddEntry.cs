using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class WindowAddEntry : EditorWindow {

    string textFieldLocaleKey = "";
    string textFieldLocaleContent = "";
    string[] localeFiles;
    int selectedFile = 0;

    [MenuItem("Localization/Files/Add entry to file")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(WindowAddEntry), true, "Add Entry");
    }

    void OnGUI()
    {
        GUILayout.Label("Creating a new entry for locale file", EditorStyles.boldLabel);

        //Filling drop down
        FillDropDownInfo();
        selectedFile = EditorGUILayout.Popup("Chose Language File", selectedFile, localeFiles);
        EditorGUILayout.LabelField("\n");

        //Localization Key
        textFieldLocaleKey = EditorGUILayout.TextField("Localization Key:",
                                                        textFieldLocaleKey);
        EditorGUILayout.LabelField("Example: timebomb\n");
        EditorGUILayout.LabelField("\n");

        //Key content
        /*textFieldLocaleContent = EditorGUILayout.TextField("Key content:",
                                                     textFieldLocaleContent);*/

        EditorGUILayout.LabelField("Key content:");
        textFieldLocaleContent = EditorGUILayout.TextArea(textFieldLocaleContent,
                                                           GUILayout.Height(50));


        EditorGUILayout.LabelField("Example: A bomb with a 3 sec. timer.\n");

        if (GUILayout.Button("Add entry to file"))
        {
            Debug.Log("Adding entry to file");
            AddNewEntry(localeFiles,
                        localeFiles[selectedFile],
                        textFieldLocaleKey,
                        textFieldLocaleContent);
        }

        if (GUILayout.Button("Close window"))
        {
            this.Close();
        }
    }

    /// <summary>
    /// Fills drop down with all available files on Locale folder
    /// </summary>
    void FillDropDownInfo() {
        var info = new DirectoryInfo(Application.dataPath + "/TranslatorPlugin/Locale");
        var fileInfo = info.GetFiles();
        int localeFilesIndex = 0;

        localeFiles = new string[fileInfo.Length/2];

        foreach (FileInfo file in fileInfo)
        {
            if (!file.Name.Contains("meta")) {
                localeFiles[localeFilesIndex] = file.Name;
                localeFilesIndex += 1;
            }
        }
    }

    /// <summary>
    /// Takes all information from current window and tries to add
    /// it to available files
    /// </summary>
    /// <param name="files">File names</param>
    /// <param name="lan">Language file</param>
    /// <param name="key">Localization key</param>
    /// <param name="con">Localization content</param>
    void AddNewEntry(string[] files, string lan, string key, string con) {
        AddEntryToFile addEntry = new AddEntryToFile();
        addEntry.AddContentsToFile(files, lan, key, con);

        //Information window
        WindowInfo.SetWindowInfo(   addEntry.WindowInformation[0],
                                    addEntry.WindowInformation[1],
                                    addEntry.WindowInformation[2]);

        AssetDatabase.Refresh();
    }
}
