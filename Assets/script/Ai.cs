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
     bool startSeek = false;
    private bool isWallAvoidance;
    Vector3 destination;
    NavMeshAgent agent;
    

    private void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("mes is stopped =" + agent.isStopped);
        Transform child = transform.GetChild(18);//-> indice do filho do isWallAvoidance;
        bool startSeek = child.GetComponent<SensorPlayer>().startSeek;
        if (startSeek == true)
        {
            agent.SetDestination(GameObject.Find("gajo").transform.position);

            if (Vector3.Distance(agent.transform.position, agent.destination) < 5.0f)
                agent.isStopped = true; //VAI ATACAR!!!!
            else
            {
                if (agent.isStopped == true)
                    agent.isStopped = false;
            }

            if (Vector3.Distance(agent.transform.position, agent.destination) > 60.0f)
            {
                Debug.Log("HE DISAPPEARED! NANI!??!?!");
              //  child.GetComponent<SensorPlayer>().startSeek = false;
            }
        }
        else
        { //VAI ANDAR AS VOLTAS DENTRO DO RETANGULO (600~700 X 2400~2450) - WANDERING
            agent.SetDestination(new Vector3(Random.Range(600.0f, 700.0f), 32.3f, Random.Range(2400.0f, 2450.0f)));

            if (Vector3.Distance(agent.transform.position, agent.destination) < 5.0f)
                agent.SetDestination(new Vector3(Random.Range(600.0f, 700.0f), 32.3f, Random.Range(2400.0f, 2450.0f)));
        }

    }
  
}
