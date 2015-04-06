using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
    public static int TutorialNumber;
    RawImage[] Images;
	// Use this for initialization
	void Start () {
        TutorialNumber = 0;
        Images = new RawImage[4];
        Images[0] = GameObject.Find("Tutorial1").GetComponent<RawImage>();
        Images[1] = GameObject.Find("Tutorial2").GetComponent<RawImage>();
        Images[2] = GameObject.Find("Tutorial3").GetComponent<RawImage>();
        Images[3] = GameObject.Find("Tutorial4").GetComponent<RawImage>();
        f_ResetImages();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void F_BushUsed()
    {
        Images[TutorialNumber].enabled=false;
        TutorialNumber++;
        if (TutorialNumber < Images.Length)
            Images[TutorialNumber].enabled = true;
    }
    public void f_ResetImages()
    {
        for (int i = 0; i < Images.Length; i++)
        {
            Images[i].enabled = false;
        }
        Images[0].enabled = true;
    }
}
