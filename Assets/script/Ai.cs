using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Ai : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rbZombie;
    public Rigidbody rbPlayer;
    // unsafe int* f;
    private  bool startSeek = false, isInicialDest=false;
    private bool isWallAvoidance;
    private Vector3 destination, startPos;
    NavMeshAgent agent;
    public float health = 100.0f;
    
    

    private void Start()
    {
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        
       //    Debug.Log("mes is stopped =" + agent.isStopped);
        Transform child = transform.GetChild(18);
        bool startSeek = child.GetComponent<SensorPlayer>().startSeek;
        
        if (startSeek == true)
        {
            isInicialDest = false;
            if (agent.enabled==true)
            agent.SetDestination(GameObject.Find("gajo").transform.position);

          /*  if (Vector3.Distance(agent.transform.position, agent.destination) < 5.0f)
                agent.isStopped = true; //VAI ATACAR!!!!
            else
            {
                if (agent.isStopped == true)
                    agent.isStopped = false;
            }*/

          
        }
        else
        { //VAI ANDAR AS VOLTAS DENTRO DO RETANGULO (600~700 X 2400~2450) - WANDERING
          /* agent.SetDestination(new Vector3(Random.Range(600.0f, 700.0f), 32.3f, Random.Range(2400.0f, 2450.0f)));

           if (Vector3.Distance(agent.transform.position, agent.destination) < 5.0f)
               agent.SetDestination(new Vector3(Random.Range(600.0f, 700.0f), 32.3f, Random.Range(2400.0f, 2450.0f)));*/
            
        }
        if (Vector3.Distance(agent.transform.position, agent.destination) > 60.0f)// depois de não ver mais o player vai para a pos inicial
        {
            child.GetComponent<SensorPlayer>().startSeek = false;
            agent.SetDestination(startPos);
            isInicialDest = true;

            //child.GetComponent<SensorPlayer>().startSeek = false;
        }
        if (agent.remainingDistance <= 3 && isInicialDest == true && child.GetComponent<SensorPlayer>().startSeek== false)
       {
            agent.SetDestination(RandomNavSphere());
                  
       }
        

    }
    Vector3 RandomNavSphere() {//o wandering
        int wanderRadious = 20;
       Vector3 randomDir= Random.insideUnitSphere* wanderRadious;//->wander radius
        randomDir += startPos;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDir,out navHit, wanderRadious, -1);

        return navHit.position;//vai ser o agent.setDestination
    }
  
}
