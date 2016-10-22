using UnityEngine;
using System.Collections;

public class WindowContent {
   
    string _title;
    string _subtitle;
    string _information;
    string[] windowInformation;

    public WindowContent(string title, string subtitle, string info) {

        _title = title;
        _subtitle = subtitle;
        _information = info;

        windowInformation = new string[] {  title,
                                            subtitle,
                                            info};
    }

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
}
