using UnityEngine;
using System.Collections;
using System.IO;

public class CreateFile {

    string filePath;
    string fileLanguage;
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

    // Use this for initialization
    public void CreateTxtFile (string language) {

        if (language != "")
        {
            filePath = Application.dataPath + "/TranslatorPlugin/Locale";
            fileLanguage = language.ToLower();

            filePath = filePath + "/" + fileLanguage + ".txt";

            Debug.Log("Creating Txt file at: " + filePath);
            //File creation
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
                Debug.Log("File created!");
                FillTxtFile();
            }
            else
            {
                Debug.Log("File already exists!");
                SetWindowContent("Create Locale File", "INFORMATION", "File already exists.");
            }
        }
        else {
            Debug.Log("Language is empty. Please, fill the space.");
            SetWindowContent("Create Locale File", "ERROR", "Language entry is empty.\nPlease enter a valid language");
        }
	}

    /// <summary>
    /// Creates the content to display on information window
    /// </summary>
    /// <param name="title">Information window title</param>
    /// <param name="subtitle">Information window subtitle</param>
    /// <param name="info">Information window description</param>
    void SetWindowContent(string title, string subtitle, string info) {

        WindowContent windowContent = new WindowContent(title, subtitle, info);
        WindowInformation = windowContent.WindowInformation;

    }

    /// <summary>
    /// Fills text file with a few example lines
    /// </summary>
    void FillTxtFile() {
        TextWriter tw = new StreamWriter(filePath);
        tw.WriteLine(FileContentText());
        tw.Close();
    }

    /// <summary>
    /// Returns file content matching correct language
    /// </summary>
    /// <returns></returns>
    string FileContentText() {

        string content = "";

        content = "key_content.;\nexample_Use tags followed by an underscore to identify a localization key and its content.;";
        SetWindowContent("Create Locale File", "SUCCESS", "File was created.");

        return content;
    }
}
