using UnityEngine;
using System.Collections;

public class Bush : MonoBehaviour {
    public bool Fox;
    bool FadeIn;
    float AlphaFade;
    SpriteRenderer Image;
    soundManager SoundMan;
	// Use this for initialization
	void Start () {
        AlphaFade = 0;
        FadeIn = false;
        Image = this.transform.FindChild("Animal").GetComponent<SpriteRenderer>();
        SoundMan = GameObject.FindGameObjectWithTag("Sound").GetComponent<soundManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (FadeIn)
        {
            SoundMan.playSound(1);
            AlphaFade += 0.05f;
            Color tempColor= new Color (Image.color.r,Image.color.g,Image.color.b,AlphaFade);
            Image.color = tempColor;
            if (AlphaFade > 1)
                FadeIn = false;
        }
	}

    void OnTriggerEnter(Collider hit)
    {

        //Debug.Log(hit.name);
        if (hit.gameObject.tag == "Player")
        {
            FadeIn = true;
            //Image.enabled = true;
        }
    }

    public void AnimalFound()
    {
        Debug.Log("AnimalFound");
        Image.enabled = false;
    }

    public void f_SetAnimal(Sprite Animal)
    {
        if (Image==null)
            Image = this.transform.FindChild("Animal").GetComponent<SpriteRenderer>();
        Image.sprite = Animal;
        //reset the sprite 
        Color tempColor = new Color(Image.color.r, Image.color.g, Image.color.b, 0.0f);
        Image.color = tempColor;
        AlphaFade = 0.0f;
        Image.enabled = true;
    }
}
