using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginWallAvoidance : SensorPlayer {

    // Use this for initialization
     public  bool beginWallAvoidance;
	void Start () {
        beginWallAvoidance = false;
        Physics.IgnoreCollision(rbZombie.GetComponent<Collider>(), GetComponent<Collider>());
    }
	
	// Update is called once per frame
	void Update () {

        IsWallAvoidance(ref beginWallAvoidance);
	}
    void OnTriggerEnter(Collider collision)
    {   
        if (collision.gameObject.tag == "ground")
        {
            beginWallAvoidance = true;
        }

    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            beginWallAvoidance = false;
        }
    }
}
