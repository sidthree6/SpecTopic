using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
    public float movementSpeed;

    public static int MissedCats,CurrentCats;// calculating ommision error 
    public static int HitFox,CurrentFox;// calculating commision error 
    private int overallCorrect,currentCorrect,currentRound;
    private Text RoundNum, Score;
    private Vector3 Velocity;
    private Bush CurrentBush;
    private bool hitBush, moving;
    private RawImage LevelComplete;
    private BushManager BushMan;
    private GameObject ResetButton;
    private MeanReactionTime AvgReactTime;

	// Use this for initialization
	void Start () {
        CurrentCats = 0;
        MissedCats = 0;
        CurrentFox = 0;
        HitFox = 0;

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

        AvgReactTime = new MeanReactionTime();
        AvgReactTime.Reset();
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
        float BushTime=Time.time-CurrentBush.f_returnTime();
        //Debug.Log(BushTime + "=Bush Timer");
        AvgReactTime.AddReactionTime(BushTime);
        CurrentBush.f_AnimalFound();
        if (CurrentBush.Fox)
            HitFox++;

        RoundNum.text = "Round = " + currentRound;
        Score.text = "Correct Kittens:" + (CurrentCats - MissedCats);
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
            Debug.Log("Avg RT=" + AvgReactTime.returnAverageReactionTime());
            Debug.Log("Fox "+ HitFox+ " vs "+ CurrentFox);
            Debug.Log("Missed cats "+MissedCats + " vs "+CurrentCats);
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
        CurrentCats = 0;
        MissedCats = 0;
        CurrentFox = 0;
        HitFox = 0;

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
