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
    private BushManager BushMan;
    private GameObject ResetButton;

	// Use this for initialization
	void Start () {
        moving = true;
        hitBush = false;
        overallCorrect = 0;
        currentCorrect = 0;
        currentRound = 1;
        BushMan = GameObject.Find("BushManager").GetComponent<BushManager>();

        ResetButton= GameObject.Find("NextRound");
        RoundNum = GameObject.Find("Round").GetComponent<UnityEngine.UI.Text>();
        Score = GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>();
        LevelComplete = GameObject.Find("LevelComplete").GetComponent<UnityEngine.UI.RawImage>();
        ResetButton.SetActive(false);
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
            LevelComplete.enabled = true;
            ResetButton.SetActive(true);
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

    public void f_resetGame()
    {
        currentCorrect = 0;
        currentRound++;
        RoundNum.text = "Round = " + currentRound;
        this.transform.position = new Vector3(0, 0, 0);
        moving = true;
        LevelComplete.enabled = false;
        ResetButton.SetActive(false);
        BushMan.f_ResettingBushes();
    }
}
