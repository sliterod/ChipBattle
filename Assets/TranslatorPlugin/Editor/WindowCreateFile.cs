using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Globalization;
using System;

public class WindowCreateFile : EditorWindow {

    CreateFile createFile;
    static string[] languages;
    static int dropdownIndex = 0;

    [MenuItem("Localization/Files/Create Locale File")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(WindowCreateFile),true,"Create Locale File");

        FillLanguageList();
        SetCurrentLanguageOnDropdown();
    }

    void OnGUI() {
        
        GUILayout.Label("Creating a new Language File", EditorStyles.boldLabel);        

        dropdownIndex = EditorGUILayout.Popup("Chose Language", dropdownIndex, languages);
        EditorGUILayout.LabelField("\n");

        if (GUILayout.Button("Create file"))
        {
            Debug.Log("Atempting to create file");          
            CreateFile();
        }

        if (GUILayout.Button("Close window"))
        {
            this.Close();
        }

        FillLanguageList();
    }

    /// <summary>
    /// Creates a new text file
    /// </summary>
    void CreateFile(){

        if (dropdownIndex == 0)
        {
            //Information window
            WindowInfo.SetWindowInfo(   "Create Locale File",
                                        "INFORMATION",
                                        "Please select a valid language.");
        }
        else {
            createFile = new CreateFile();
            createFile.CreateTxtFile(languages[dropdownIndex]);

            //Information window
            WindowInfo.SetWindowInfo(   createFile.WindowInformation[0],
                                        createFile.WindowInformation[1],
                                        createFile.WindowInformation[2]);
        }

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// Fill supported languages list
    /// </summary>
    static void FillLanguageList() {
        CultureInfo[] allCultures;
        int languageIndex = 0;

        //Get all neutral cultures
        allCultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures);

        //Initialize language array
        languages = new string[allCultures.Length];

        //Fill language array
        foreach (CultureInfo ci in allCultures)
        {
            if (languageIndex > 0)
            {
                languages[languageIndex] = ci.EnglishName.ToLower();
                languageIndex += 1;
            }
            else {
                languages[languageIndex] = " - - - ";
                languageIndex += 1;
            }
        }

        //Sorting array
        Array.Sort(languages);
    }

    /// <summary>
    /// Sets system language on dropdown
    /// </summary>
    static void SetCurrentLanguageOnDropdown() {

        string osLanguage = Application.systemLanguage.ToString().ToLower();
        int index = 0;

        //Searching language
        foreach (string language in languages) {
            if (language == osLanguage)
            {
                Debug.Log("Language found on position: " + index);
                break;
            }
            else {
                index += 1; 
            }
        }

        //Changing dropdown position
        if (index < languages.Length)
        {
            dropdownIndex = index;
        }
        else
        {
            dropdownIndex = 0;
        }
    }
}
