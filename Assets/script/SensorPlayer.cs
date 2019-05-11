using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SensorPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public bool startSeek = false;
    // unsafe int* f;
    private  bool isWallAvoidance;
    Vector3 destination;
    public Rigidbody rbZombie;
    public Rigidbody rbPlayer;

    private void Start()
    {
        isWallAvoidance = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "player")
                startSeek = true;// quando o player entra na area do zombie e começa a ser perseguido pelo mesmo
    }
    public void  IsWallAvoidance(ref bool isTrue) {
        isWallAvoidance = isTrue;
    }  
}
