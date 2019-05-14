using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class ZombieJump : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Rigidbody rbZombie, rbPlayer;
    public  bool zombieJump;
    private static  float timeJump = 0, timeGround =0;
     int jumpForce, directionForce ;
    Transform parent;
    float yAngle = 0;
    NavMeshAgent meshAg;
    public  static string nameZomb;
    void Start()
    {
        /* directionForce = 80;
         jumpForce = 5;*/
        directionForce = 2950;
        jumpForce = 300;
        zombieJump = false;
       // Physics.IgnoreCollision(transform.parent.GetChild(21).GetComponent<Collider>(), GetComponent<Collider>());
        yAngle = rbZombie.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        // rbZombie.transform.rotation = Quaternion.Euler(0, rbZombie.transform.rotation.y, 0);
       if(nameZomb== transform.parent.name)
        UpdateJump();   
        
    }
    void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.tag == "ground")
        {
            zombieJump = true;
            nameZomb = transform.parent.name;
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
        timeJump += Time.deltaTime;
        Transform parent = transform.parent;
         meshAg = parent.GetComponent<NavMeshAgent>();
        timeGround += Time.deltaTime;
        Vector3 dir = rbPlayer.transform.position - rbZombie.transform.position;
        //Quaternion target = Quaternion.LookRotation(dir);
        //rbZombie.transform.rotation = Quaternion.Lerp(rbZombie.transform.rotation, target, 0);
       
           // rbZombie.transform.rotation = Quaternion.LookRotation(dir);
        
        if (zombieJump == true && transform.GetChild(0).GetComponent<SartNavMesh>().startNavMesh == true)// quando o colider colide contra um muro salta para apanhar o player
        {

            
            meshAg.enabled = false;
            rbZombie.transform.rotation = Quaternion.LookRotation(dir.normalized);
            rbZombie.AddForce(rbZombie.transform.up * jumpForce, ForceMode.Impulse);
            rbZombie.AddForce(dir.normalized * directionForce, ForceMode.Impulse);
            //rbZombie.freezeRotation = true;
            timeJump = 0;
            zombieJump = false;
            timeGround = 0;
        }
        timeJump += Time.deltaTime;
        if (timeGround >= 1 && transform.GetChild(0).GetComponent<SartNavMesh>().startNavMesh==true )
        {
            meshAg.enabled = true;
            meshAg.updateRotation = true;
            timeGround = 0;
            transform.parent.GetChild(18).GetComponent<SensorPlayer>().startSeek = true;
        }
    }
    private void FixedUpdate()
    {
        

    }


}
