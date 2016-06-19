using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System;

public class SetLanguage : MonoBehaviour {

    public TextAsset language;

    string languagePath;
    string[] languageFileContent;

    public string[] LanguageFileContent
    {
        get
        {
            return languageFileContent;
        }

        set
        {
            languageFileContent = value;
        }
    }

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this);
        LoadFileContents();
	}

    /// <summary>
    /// Loads all contents of file
    /// </summary>
    void LoadFileContents() {

        try {
            languagePath = Application.dataPath
                            + "/TranslatorPlugin/Locale/"
                            + language.name
                            + ".txt";

            TextReader reader = new StreamReader(languagePath);

            languageFileContent = Regex.Split(reader.ReadToEnd(), ";");

            //Sorts array from 0-9_a-z
            Array.Sort(languageFileContent);
        }
        catch (MissingReferenceException mrex) {
            Debug.Log("Language asset is missing: " + mrex.Message);
        }
        catch (UnassignedReferenceException urex) {
            Debug.Log("Language asset is empty: " + urex.Message);
        }
    }

}