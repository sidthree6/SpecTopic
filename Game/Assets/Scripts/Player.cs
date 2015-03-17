using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float movementSpeed;

    private Bush CurrentBush;
    private bool hitBush;
	// Use this for initialization
	void Start () {
        hitBush = false;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 Velocity = new Vector3( 0.1f*movementSpeed*Time.deltaTime, 0, 0);
        this.transform.position = this.transform.position + Velocity;

        ButtonPress();
	}

    void ButtonPress() {
        if (Input.GetKeyDown(KeyCode.Space) && hitBush)
        {
            CurrentBush.AnimalFound();
            hitBush = false;
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        //Debug.Log(hit.name);
        if (hit.gameObject.tag == "Bush")
        {
            hitBush=true;
            CurrentBush = hit.gameObject.GetComponent<Bush>();
        }
    }
    void OnTriggerExit(Collider hit)
    {
        //Debug.Log(hit.name);
        if (hit.gameObject.tag == "Bush")
        {
            hitBush = false;
            //Destroy(hit.gameObject);
        }
    }
}
