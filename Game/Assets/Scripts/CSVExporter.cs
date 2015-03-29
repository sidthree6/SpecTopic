using UnityEngine;
using System.Collections;
// below for fileIO
using System.IO;
using System.Text;
using System.Reflection;//for local data

public class CSVExporter : MonoBehaviour
{
    public int maxLines;
    public int DetailsLine;
    private string path;
    private string[] Line;
    // Use this for initialization
    void Start()
    {
        intializeFile();
        // Write some text in it
        //WriteLine("All game, data will be,,, recorded here ");

        //addToLine("false", 1);
        //addToLine("false", 4);
        //addToLine("false", 2);
        //ExportLine();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// initalize function 
    /// </summary>
    private void intializeFile()
    {
        if (maxLines == 0)
            maxLines = 30;
        Line = new string[maxLines];

        path = Application.dataPath;
        path = nextFile(path, 1);

        //// Delete the file if it exists. 
        if (File.Exists(path))
            File.Delete(path);

        //Create the file. 
        using (FileStream fs = File.Create(path)) ;
        initializeFirstLine();
    }

    /// <summary>
    /// set the headers  
    /// </summary>
    private void initializeFirstLine()
    {// start the first line 
        addToLine("Time(s)", 0);
        addToLine("Expected", 1);
        addToLine("Actual", 2);
        addToLine("Other Time", 3);
        addToLine("Details", DetailsLine);
        ExportAllLines();
    }

    /// <summary>
    /// export all the lines currently held 
    /// </summary>
    public void ExportAllLines()
    {
        string writingStream = "\r\n" + Line[0];
        for (int i = 1; i < Line.Length; i++)
            writingStream += (","+Line[i]);

        using (StreamWriter fs = new StreamWriter(path, true))
            fs.Write(writingStream);
        ClearLines();
    }

    /// <summary>
    /// overrided export all lines, with the time parameter 
    /// </summary>
    /// <param name="time"></param>
    public void ExportAllLines(float time)
    {
        string writingStream = "\r\n" + time;
        for (int i = 1; i < Line.Length; i++)
            writingStream += ("," + Line[i]);

        using (StreamWriter fs = new StreamWriter(path, true))
            fs.Write(writingStream);
        ClearLines();
    }
    /// <summary>
    /// export 1 detail 
    /// </summary>
    /// <param name="time"> used to determine the time</param>
    /// <param name="detail">to determine the details being entered</param>
    public void ExportDetail(float time, string detail)
    {
        Line[DetailsLine] = detail;
        string writingStream = "\r\n" + time;
        for (int i = 1; i < Line.Length; i++)
            writingStream += ("," + Line[i]);

        using (StreamWriter fs = new StreamWriter(path, true))
            fs.Write(writingStream);
        ClearLines();
    }

    public void ClearLines()
    {
        for (int i = 0; i < Line.Length; i++)
            Line[i] = "";
    }

    public void addToLine(string input, int col)
    {
        if (maxLines >= col)
            Line[col] = input;
    }

    public void addOnDetail(string input, int col)
    {
        if (maxLines >= col)
            Line[col] =  Line[col]+input;
    }


    private string nextFile(string filePath, int num)
    {
        string tempFile;
        tempFile = Application.dataPath;

        if (num < 10)
            tempFile += "/Subject0" + num + ".csv";
        else
            tempFile += "/Subject" + num + ".csv";

        if (File.Exists(tempFile))
        {
            return nextFile(filePath, num + 1);
        }
        else
            return tempFile;
    }


}