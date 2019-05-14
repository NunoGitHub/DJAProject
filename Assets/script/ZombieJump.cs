﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class ZombieJump : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Rigidbody rbZombie, rbPlayer;
    public  bool zombieJump;
    private  float timeJump = 0, timeGround =0;
    public int jumpForce, directionForce ;
    Transform parent;
    float yAngle = 0;
    //NavMeshAgent meshAg;
    void Start()
    {
        directionForce = 35;
        jumpForce = 7;
        zombieJump = false;
        Physics.IgnoreCollision(rbZombie.GetComponent<Collider>(), GetComponent<Collider>());
        yAngle = rbZombie.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        // rbZombie.transform.rotation = Quaternion.Euler(0, rbZombie.transform.rotation.y, 0);
       
       
    }
    void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.tag == "ground")
        {
            zombieJump = true;
           // meshAg.radius = 0;
        }

    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "ground")
        {
           
        }
    }
    void UpdateJump()
    {
        
        Transform parent = transform.parent;
       //  meshAg = parent.GetComponent<NavMeshAgent>();
        timeGround += Time.deltaTime;
        Vector3 dir = rbPlayer.transform.position - rbZombie.transform.position;
<<<<<<< HEAD
<<<<<<< HEAD
        //Quaternion target = Quaternion.LookRotation(dir);
        //rbZombie.transform.rotation = Quaternion.Lerp(rbZombie.transform.rotation, target, 0);
        rbZombie.transform.rotation = Quaternion.LookRotation(dir);//->erro
        if (this.zombieJump == true && timeJump >= 1f)// quando o colider colide contra um muro salta para apanhar o player
        {
            parent.GetComponent<NavMeshAgent>().enabled = false;
            rbZombie.transform.rotation = Quaternion.LookRotation(dir);
=======
        Quaternion target = Quaternion.LookRotation(dir);
        rbZombie.transform.rotation = Quaternion.Lerp(rbZombie.transform.rotation, target, 0);
        if (zombieJump == true && timeJump >= 1f)// quando o colider colide contra um muro salta para apanhar o player
        {
>>>>>>> parent of 046c642... some fixes
=======
        Quaternion target = Quaternion.LookRotation(dir);
        rbZombie.transform.rotation = Quaternion.Lerp(rbZombie.transform.rotation, target, 0);
        if (zombieJump == true && timeJump >= 1f)// quando o colider colide contra um muro salta para apanhar o player
        {
>>>>>>> parent of 046c642... some fixes
            //rbZombie.freezeRotation = true;
            
            meshAg.enabled = false;
           // rbZombie.transform.rotation = Quaternion.Euler(0, 0, Input.GetAxis("Horizontal"));
              rbZombie.AddForce(rbZombie.transform.up * jumpForce, ForceMode.Impulse);
             rbZombie.AddForce(dir.normalized * directionForce, ForceMode.Impulse);
            timeJump = 0;
            zombieJump = false;
            timeGround = 0;
        }
        timeJump += Time.deltaTime;
      
        if (timeGround >= 1 && transform.GetChild(0).GetComponent<SartNavMesh>().startNavMesh==true )
        {
        //    parent.GetComponent<NavMeshAgent>().enabled = true;
            timeGround = 0;
        }
    }
    private void FixedUpdate()
    {
        UpdateJump();

    }


}
