using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

public class AddEntryToFile {

    WindowContent windowContent;
    string[] windowInformation;

    public string[] WindowInformation
    {
        get
        {
            return windowInformation;
        }

        set
        {
            windowInformation = value;
        }
    }

    /// <summary>
    /// Adds a new entry to localization file
    /// </summary>
    /// <param name="files">Arrays of files</param>
    /// <param name="language">Language where the entry will be added</param>
    /// <param name="newKey">Localization Key</param>
    /// <param name="newContent">Localization Content</param>
    public void AddContentsToFile(string[] files, string language, string newKey, string newContent) {

        string placeholder = "Lorem ipsum dolor sit amet, consectetur";
        string fullContent = newKey + "_" + newContent + ";";
        string partialContent = newKey + "_" + placeholder + ";";

        partialContent = Regex.Replace(partialContent, @"\n", "\\n");
        fullContent = Regex.Replace(fullContent, @"\n", "\\n");

        string availableLanguages = "";

        foreach (string fileName in files) {

            string path = Application.dataPath
                        + "/TranslatorPlugin/Locale/"
                        + fileName;

            if (fileName != language)//Adds key and placeholder info
            {
                Debug.Log("Adding partial entry to file: " + fileName);
                if (File.Exists(path))
                {
                    TextWriter partial = new StreamWriter(path, true);
                    partial.WriteLine(partialContent);
                    partial.Close();

                    availableLanguages = availableLanguages + fileName + "\n";
                }
            }
            else if (fileName == language)//Adds complete information
            {
                Debug.Log("Adding full entry to file: " + fileName);
                if (File.Exists(path))
                {
                    TextWriter full = new StreamWriter(path, true);
                    full.WriteLine(fullContent);
                    full.Close();
                }
            }
        }

        SetWindowContent(   "Add entry to File",
                            "INFORMATION",
                            "Added Key: "+ newKey + "\nto file: " + language + "."+
                            "\n\n"+
                            "Key and placeholders added to:\n" + availableLanguages);
    }

    /// <summary>
    /// Creates the content to display on information window
    /// </summary>
    /// <param name="title">Information window title</param>
    /// <param name="subtitle">Information window subtitle</param>
    /// <param name="info">Information window description</param>
    void SetWindowContent(string title, string subtitle, string info)
    {
        windowContent = new WindowContent(title, subtitle, info);

        WindowInformation = windowContent.WindowInformation;
    }
}
