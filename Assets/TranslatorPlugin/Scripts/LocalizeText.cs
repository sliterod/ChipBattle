using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System.Collections;
using System;

public class LocalizeText : MonoBehaviour {

    /// <summary>
    /// String used as a key to localize current text
    /// </summary>
    public string LocalizationKey;

    void Start() {
        Localize(LocalizationKey);
    }
        
    /// <summary>
    /// Localizes the text of this UI component using the public label value on it
    /// </summary>
    /// <param name="key">String containing localization key to search on files</param>
    void Localize(string key) {
        if (key != "")
        {
            Debug.Log("Searching for key: " + key);
            SearchKey(key);
        }
        else {
            Debug.Log("Using text on component");
        }
    }

    /// <summary>
    /// Sets a key and forces a new localization upon an already localized text
    /// </summary>
    /// <param name="newKey"></param>
    public void SetKeyAndLocalize(string newKey) {
        if (newKey != "")
        {
            Debug.Log("Forcing localization");
            Localize(newKey);
        }
    }

    /// <summary>
    /// Search on language file the Localization Key
    /// </summary>
    /// <param name="key">Localization Key name</param>
    void SearchKey(string key) {

        string[] localization;
        string localeContent = "";

        try {
            localization = GameObject.Find("LocaleManager")
                            .GetComponent<SetLanguage>()
                            .LanguageFileContent;

            Debug.Log("Splitting key and content");
            for (int i = 0; i < localization.Length; i++) {

                string result = Regex.Replace(localization[i], @"\r\n?|\n", "");
                Debug.Log(i + ", " + result);
                if (result.StartsWith(key)) {
                    string[] keyAndContent = Regex.Split(result, "_");

                    localeContent = keyAndContent[1];
                    break;
                }
            }

            localeContent = Regex.Replace(localeContent, @"\\n", "\n");
            SetLocalizedText(localeContent);
        }
        catch (NullReferenceException nrex) {
            Debug.Log("Localization is not possible: " + nrex.Message);
        }
    }

    /// <summary>
    /// Sets localized text to current UI element
    /// </summary>
    /// <param name="content">Localized content</param>
    void SetLocalizedText(string content) {
        Text labelText;

        Debug.Log("Localizing text");

        labelText = this.GetComponent<Text>();
        labelText.text = content;
    }
}
