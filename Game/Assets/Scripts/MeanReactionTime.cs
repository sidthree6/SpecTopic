using UnityEngine;
using System.Collections;

public class MeanReactionTime : MonoBehaviour {
    private float[] avgReationTime;
    private int CurrentAmount;
	// Use this for initialization
	void Start () {
        Reset();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reset()
    {
        avgReationTime = new float[120];

        for (int i = 0; i < 120; i++)
        {
            avgReationTime[i] = -1;
        }
        CurrentAmount = 0;
        Debug.Log("reset");
    }

    public void AddReactionTime(float reactionTime)
    {
        avgReationTime[CurrentAmount] = reactionTime;
        CurrentAmount++;
        //Debug.Log(avgReationTime[CurrentAmount-1] + " + CA" + CurrentAmount);
    }

    public float returnAverageReactionTime()
    {
        float ReturningAverage=0;
        int Count = 0;

        for (int i = 0; i < 120; i++)
        {
            if (avgReationTime[i] == -1)
                return (float)(ReturningAverage / Count);
            ReturningAverage += avgReationTime[i];
            Count++;
            Debug.Log("At Count=" + Count + " ReturningAverage=" + ReturningAverage);
        }
        return (float)(ReturningAverage / Count);
    }
}
