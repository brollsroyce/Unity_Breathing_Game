using System.IO;
using UnityEngine;

// CSV data acquisition from other scripts
public static class CSVManager
{
    private static string reportDirectoryName = "csvFilesExport";
    private static string reportFileName = "Test.csv";
    private static string reportSeparator = ",";
    private static string[] reportHeaders = new string[3]
    {
        "Time",
        "Abdomen",
        "Chest"
    };
    private static string timeStampHeader = "time stamp";


#region Interactions

    // Adding the data to the file
    public static void AppendToReport(string[] strings)
    {
        VerifyDirectory();
        VerifyFile();
        using(StreamWriter sw = File.AppendText(GetFilePath()))
        {
            string finalString = "";
            for(int i = 0; i < strings.Length; i++)
            {
                if(finalString != "")
                {
                    finalString += reportSeparator;
                }
                finalString += strings[i];
            }
            finalString += reportSeparator + GetTimeStamp();
            sw.WriteLine(finalString);

        }
    }

    // Handling the data such that a csv file with proper headers and separators are added and creating the file.
    public static void CreateReport()
    {
        using(StreamWriter sw = File.CreateText(GetFilePath()))
        {
            VerifyDirectory();
            string finalString = "";
            for(int i = 0; i < reportHeaders.Length; i++)
            {
                if(finalString != ""){
                    finalString += reportSeparator;
                }
                finalString += reportHeaders[i];
            }
            finalString += reportSeparator + timeStampHeader;
            sw.WriteLine(finalString);
        }
    }
#endregion

#region Operations
    //Checking if file and folder specified in the initializations exists. If doesn't exist, create file / folder.
    static void VerifyDirectory()
    {
        string dir = GetDirectoryPath();
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    static void VerifyFile()
    {
        string file = GetFilePath();
        if (!File.Exists(file))
        {
            CreateReport();
        }
    }
#endregion

#region Queries
    // Create and return directory and file paths. Also print timestamps if required
    static string GetDirectoryPath()
    {
        return Application.dataPath + "/" + reportDirectoryName;
    }

    static string GetFilePath()
    {
        return GetDirectoryPath() + "/" + reportFileName;
    }

    static string GetTimeStamp()
    {
        return System.DateTime.UtcNow.ToString();
    }
#endregion
}
