using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
    public float movementSpeed;

    public static float MissedCats,CurrentCats;// calculating ommision error 
    public static float HitFox,CurrentFox;// calculating commision error 
    float HitFoxPer, MissCatPer;
    private int overallCorrect,currentCorrect,currentRound;
    private Text RoundNum, Score;
    private Vector3 Velocity;
    private bool hitBush, moving;

    private Bush CurrentBush;
    private RawImage LevelComplete;
    private BushManager BushMan;
    private GameObject ResetButton;
    private MeanReactionTime AvgReactTime;
    private PlayerRecorder TextRecorder;
    private CSVExporter ExcelExporter;

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
        GameObject RecordObj = GameObject.FindGameObjectWithTag("Recorder");
        TextRecorder = RecordObj.GetComponent<PlayerRecorder>();
        ExcelExporter = RecordObj.GetComponent<CSVExporter>();
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
            if (hitBush)
            {
                if (!CurrentBush.Tutorial)
                    BushCheck();
                else
                    CurrentBush.f_AnimalFound();

                hitBush = false;
            }

        }
    }

    void BushCheck()
    {
        float BushTime=Time.time-CurrentBush.f_returnTime();
        int cat = 0;
        string BrushMessage = "";
        //Debug.Log(BushTime + "=Bush Timer");
        AvgReactTime.AddReactionTime(BushTime);
        CurrentBush.f_AnimalFound();
        if (CurrentBush.Fox)
        {
            BrushMessage = "found a fox";
            HitFox++;
            cat++;
        }
        else
            BrushMessage = "found a Cat";


        ExcelExporter.addOnDetail(BrushMessage, 6);
        ExcelExporter.addOnDetail(cat.ToString(), 4);
        ExcelExporter.addOnDetail(BushTime.ToString(), 2);
        ExcelExporter.addOnDetail((CurrentCats + CurrentFox).ToString(), 1);
        ExcelExporter.ExportAllLines(TextRecorder.returnTime());

        f_UpdateScore();
    }

    void OnTriggerEnter(Collider hit)
    {
        //Debug.Log(hit.name);
        if (hit.gameObject.tag == "Bush" || hit.gameObject.tag == "TutBush")
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
            Debug.Log("Fox hit"+ HitFox+ " vs "+ CurrentFox);
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


    public void f_UpdateScore()
    {
        RoundNum.text = "Round = " + currentRound;
        Score.text = "Current Bush:" + (CurrentCats + CurrentFox);
    }

    public void f_CheckPercent()
    {
        if (CurrentCats + CurrentFox>10) 
        {
            HitFoxPer=(float)(HitFox / CurrentFox);
            MissCatPer = (float) (MissedCats / CurrentCats);
            Debug.Log(HitFoxPer + "=FoxPer " + MissCatPer + "=MissCatPer ");
            if (HitFoxPer > 0.1f && MissCatPer > 0.1f)
            {
                ExcelExporter.addOnDetail("Increasing Go Kitten Percent", 6);
                ExcelExporter.addOnDetail((BushManager.KittenPercent).ToString(), 5);
                ExcelExporter.addOnDetail(HitFoxPer.ToString(),4);
                ExcelExporter.addOnDetail(MissCatPer.ToString(),3);
                ExcelExporter.addOnDetail((AvgReactTime.returnAverageReactionTime()).ToString(),2);
                ExcelExporter.addOnDetail((CurrentCats + CurrentFox).ToString(),1);
                ExcelExporter.ExportAllLines(TextRecorder.returnTime());
                
                BushManager.KittenPercent += 5;
                TextRecorder.RecordAction("current average Reaction Time" + AvgReactTime.returnAverageReactionTime());
                TextRecorder.RecordAction("Increasing Go Percent to" + BushManager.KittenPercent);

                //reseting 
                MissedCats=0;
                CurrentCats=0;
                HitFox=0;
                CurrentFox = 0;
            }
                
        }
    }
}
