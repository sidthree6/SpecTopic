using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float movementSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 Velocity = new Vector3(movementSpeed*Time.deltaTime, 0, 0);
        this.transform.position = this.transform.position + Velocity;
	}
}
