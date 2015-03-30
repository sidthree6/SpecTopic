using UnityEngine;
using System.Collections;

public class BushManager : MonoBehaviour {
    public Sprite Cat, Fox;
    public float RigthPercent;
    public static int KittenPercent;
    private Bush[] AllBushes;
	// Use this for initialization
	void Start () {
        KittenPercent = 70;
        GameObject[] BushObjects = GameObject.FindGameObjectsWithTag("Bush");
        AllBushes = new Bush[BushObjects.Length];
        for (int i = 0; i < BushObjects.Length; i++)
        {
            AllBushes[i] = BushObjects[i].GetComponent<Bush>();
        }
        f_ResettingBushes();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void f_ResettingBushes(){
        for (int i = 0; i < AllBushes.Length; i++)
        {
            AllBushes[i].f_resetBush();
            AllBushes[i].Fox = false;
        }
    }

    /// <summary>
    /// old reseting bushes, for resetting when there was a set amount at the beginning 
    /// </summary>
    public void f_oldResettingBushes()
    {
        bool set=false;
        int RightSet=0,WrongSet=0,RandomNumber;
        int Rightmax = (int)(AllBushes.Length * RigthPercent), WrongMax = (int)(AllBushes.Length * (1.0f - RigthPercent));
        Debug.Log("Right Max=" + Rightmax + " Wrong Max=" + WrongMax);

        RandomNumber=Random.Range(0, 9);

        for (int i = 0; i < AllBushes.Length; i++)
        {
            while (!set)
            {
                if (RandomNumber >= 5 && Rightmax >= RightSet)
                {// cat= right 
                    //AllBushes[i].f_resetBush(Cat);
                    AllBushes[i].Fox = false;
                    RightSet++;
                    set = true;
                }
                else if (WrongMax >= WrongSet)
                {
                    //AllBushes[i].f_resetBush(Fox);
                    AllBushes[i].Fox = true;
                    WrongSet++;
                    set = true;
                }
                RandomNumber = Random.Range(0, 9);
            }
            RandomNumber = Random.Range(0, 9);
            set=false;
        }
        Debug.Log("Right =" + RightSet + " Wrong =" + WrongSet);
        
    }

}
