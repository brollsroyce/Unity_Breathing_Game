using UnityEditor;
using UnityEngine;


// Script to create Unity tools that can be accessed within Unity Editor
public static class MyTools
{
    [MenuItem("My Tools/1. Add Defaults to Report %F1")]
    static void DEV_AppendDefaultsToReport()
    {
        CSVManager.AppendToReport(new string[3]
            {
                Time.time.ToString(),
                Random.Range(0, 11).ToString(),
                Random.Range(0, 11).ToString()
            }
            );
        EditorApplication.Beep();
    }

    [MenuItem("My Tools/2. Reset Report %F12")]
    static void DEV_ResetReport()
    {
        CSVManager.CreateReport();
        EditorApplication.Beep();
    }

    [MenuItem("My Tools/3. Add Specifics to Report %F2")]
    public static void DEV_AppendSpecificsToReport(string[] strings)
    {
        CSVManager.AppendToReport(strings);
        EditorApplication.Beep();
    }
}
