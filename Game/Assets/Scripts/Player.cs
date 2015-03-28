using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
    public float movementSpeed;

    private int overallCorrect,currentCorrect,currentRound;
    private Text RoundNum, Score;
    private Vector3 Velocity;
    private Bush CurrentBush;
    private bool hitBush, moving;
    private RawImage LevelComplete;

	// Use this for initialization
	void Start () {
        moving = true;
        hitBush = false;
        overallCorrect = 0;
        currentCorrect = 0;
        currentRound = 1;

        RoundNum = GameObject.Find("Round").GetComponent<UnityEngine.UI.Text>();
        Score = GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>();
        LevelComplete = GameObject.Find("LevelComplete").GetComponent<UnityEngine.UI.RawImage>();
        LevelComplete.enabled = false;
        RoundNum.text = "Round = " + currentRound;
        Score.text = "Correct Kittens:" + currentCorrect;

	}
	
	// Update is called once per frame
	void Update () {
        Velocity = new Vector3( movementSpeed*Time.deltaTime, 0, 0);
        if (moving)
            this.transform.position = this.transform.position + Velocity;

        ButtonPress();
	}

    void ButtonPress() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (hitBush){
                BushCheck();
                hitBush = false;
            }

        }
    }

    void BushCheck()
    {
        CurrentBush.AnimalFound();
        if (!CurrentBush.Fox)
            currentCorrect++;

        RoundNum.text = "Round = " + currentRound;
        Score.text = "Correct Kittens:" + currentCorrect;
    }

    void OnTriggerEnter(Collider hit)
    {
        //Debug.Log(hit.name);
        if (hit.gameObject.tag == "Bush")
        {
            hitBush=true;
            CurrentBush = hit.gameObject.GetComponent<Bush>();
        }
        if (hit.gameObject.tag == "Finish")
        {
            moving = false;
            overallCorrect += currentCorrect;
            currentRound++;
            LevelComplete.enabled = true;
        }
    }
    void OnTriggerExit(Collider hit)
    {
        //Debug.Log(hit.name);
        if (hit.gameObject.tag == "Bush")
        {
            //Debug.Log("exiting");
            //hitBush = false;
            //Destroy(hit.gameObject);
        }
    }
}
