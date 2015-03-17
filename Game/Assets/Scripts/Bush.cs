using UnityEngine;
using System.Collections;

public class Bush : MonoBehaviour {
    public bool Fox;
    SpriteRenderer Image;
	// Use this for initialization
	void Start () {
        Image = this.transform.FindChild("Animal").GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider hit)
    {
        //Debug.Log(hit.name);
        if (hit.gameObject.tag == "Player")
        {
            Image.enabled = true;
        }
    }

    public void AnimalFound()
    {
        Image.enabled = false;
    }
}
