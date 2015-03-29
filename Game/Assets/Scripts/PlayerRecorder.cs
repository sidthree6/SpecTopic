using UnityEngine;
using System.Collections;
// below for fileIO
using System.IO;
using System.Text;
using System.Reflection;//for local data

public class PlayerRecorder : MonoBehaviour {
	//public string FileName;
	private string path;
	private float BufferedTime=0;// used with pausing to make a difference between the timings

	// Use this for initialization
	void Start () {
		path = Application.dataPath;
		path+= "/Subject01.txt";
		path = nextFile (path, 1);
		writeToFile();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider hit) {

	}
	
	private static void AddText(FileStream fs, string value)
	{
		byte[] info = new UTF8Encoding(true).GetBytes(value);
		fs.Write(info, 0, info.Length);
	}

	private void writeToFile(){
		// Delete the file if it exists. 
//		if (File.Exists(path))
//			File.Delete(path);
		
		//Create the file. 
		using (FileStream fs = File.Create(path));

		// Write some text in it
		WriteLine( "All game data will be recorded here ");

	}

	private void WriteLine(string Content){
		string writingStream="\r\n";
		writingStream+=Content;
		
		using (StreamWriter fs = new StreamWriter (path,true))
			fs.Write(writingStream);

	}

	private string nextFile (string filePath, int num)
	{
		string tempFile;
		tempFile = Application.dataPath;
		if (num < 10) 
		{
			tempFile += "/Subject0" + num + ".txt";
		}
		else
		{
			tempFile += "/Subject" + num + ".txt";
		}

		if (File.Exists (tempFile)) 
		{
			return nextFile (filePath, num + 1);
		} 
		else
			return tempFile;
	}

	public void RecordAction(string Action){
		string OutputLine="At time, "+(Time.time-BufferedTime)+" seconds, "+Action;
		WriteLine(OutputLine);

	}

	public void ResetTimer(){
		BufferedTime=Time.time;
	}

	public float returnTime(){
		return (Time.time-BufferedTime);
	}
}
