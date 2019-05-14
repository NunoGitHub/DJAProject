using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Boid : ZombieInstanciator
{
    
    public int maxSpeed = 2;
    //NavMeshAgent agent;
    NavMeshAgent agent;

    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public Transform tr;
    
   
    private float str;
    bool distance = false;
    public static Transform allBoids;

    void Awake()
    {
        tr = transform;
      velocity = Random.onUnitSphere * maxSpeed;
    }

    private void Start()
    {
        //agent = transform.GetComponent<NavMeshAgent>();
        //  InvokeRepeating("CalculateVelocity", Random.value * tick, tick);

        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        if (startFlocking == true)
        {//agent.SetDestination(new Vector3(Random.Range(600.0f, 700.0f), 32.3f, Random.Range(2400.0f, 2450.0f)));
            agent.SetDestination(GameObject.Find("gajo").transform.position);
        }
        //}
    }

    Vector3 Steering() {
        Vector3 desiredVel = rbPlayer.transform.position - tr.transform.position;
        if (desiredVel.magnitude < 2) distance = true; else distance= false;
        Quaternion target = Quaternion.LookRotation(desiredVel);
        float strength = 2.5f;
        str = Mathf.Min(strength * Time.deltaTime, 1);
        desiredVel = Vector3.Normalize(desiredVel) * 2;//->max vel;
        Vector3 steering = desiredVel - velocity*Time.deltaTime;
        tr.rotation = Quaternion.Lerp(tr.rotation, target, str);
        tr.rotation = Quaternion.Euler(0, tr.eulerAngles.y, 0);
        return steering.normalized * 2 * Time.deltaTime;

    }

  
}
