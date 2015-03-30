using UnityEngine;
using System.Collections;

public class Bush : MonoBehaviour {
    public bool Fox;
    bool FadeIn,FadeOut,BushUsed;
    float AlphaFade,StartTime;
    SpriteRenderer Image;
    soundManager SoundMan;
    BushManager BushMan;
    Player Playa;
	// Use this for initialization
	void Start () {
        AlphaFade = 0;
        BushUsed = false;
        FadeIn = false;
        Image = this.transform.FindChild("Animal").GetComponent<SpriteRenderer>();
        SoundMan = GameObject.FindGameObjectWithTag("Sound").GetComponent<soundManager>();
        BushMan = GameObject.Find("BushManager").GetComponent<BushManager>();
        Playa = GameObject.Find("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        if (FadeIn)
        {
            SoundMan.playSound(1);
            AlphaFade += 0.08f;
            Color tempColor= new Color (Image.color.r,Image.color.g,Image.color.b,AlphaFade);
            Image.color = tempColor;
            if (AlphaFade > 1)
                FadeIn = false;
        }

        if (FadeOut)
        {
            AlphaFade -= 0.08f;
            Color tempColor = new Color(Image.color.r, Image.color.g, Image.color.b, AlphaFade);
            Image.color = tempColor;
            if (AlphaFade < 0)
                FadeOut = false;
        }
	}

    void OnTriggerEnter(Collider hit)
    {

        //Debug.Log(hit.name);
        if (hit.gameObject.tag == "Player")
        {
            FadeIn = true;
            StartTime = Time.time;
            AlphaFade = 0;

            SetAnimal();
            //Image.enabled = true;
        }
    }

    void OnTriggerExit(Collider hit)
    {

        //Debug.Log(hit.name);
        if (hit.gameObject.tag == "Player")
        {
            FadeOut = true;
            StartTime = Time.time;
            AlphaFade = 1.0f;
            CheckMissedBush();
            //Image.enabled = true;
        }
    }

    void CheckMissedBush()
    {
        if (!Fox&&!BushUsed)
            Player.MissedCats++;
    }

    public void f_AnimalFound()
    {
        //Debug.Log("AnimalFound");
        Image.enabled = false;
        BushUsed = true;
    }

    /// <summary>
    /// reseting function for the bush 
    /// </summary>
    /// <param name="Animal"></param>
    public void f_resetBush()
    {

        //reset the sprite 
        Color tempColor = new Color(Image.color.r, Image.color.g, Image.color.b, 0.0f);
        Image.color = tempColor;
        AlphaFade = 0.0f;
        Image.enabled = true;
        BushUsed = false;
    }

    public void f_setAnimal(Sprite Animal)
    {
        if (Image == null)
            Image = this.transform.FindChild("Animal").GetComponent<SpriteRenderer>();
        Image.sprite = Animal;
    }

    public float f_returnTime()
    {
        return StartTime;
    }

    void SetAnimal() {
        Playa.f_UpdateScore();
        Playa.f_CheckPercent();

        int RandomNumber= Random.Range(0, 100);
        Debug.Log(RandomNumber + "Random Number");
        if (RandomNumber <= BushManager.KittenPercent )
        {// cat= right 
            f_setAnimal(BushMan.Cat);
            Fox = false;
            Player.CurrentCats++;
            //Debug.Log(RandomNumber+ "set cat");
        }
        else 
        {
            f_setAnimal(BushMan.Fox);
            Fox = true;
            Player.CurrentFox++;
        }

    }
}
