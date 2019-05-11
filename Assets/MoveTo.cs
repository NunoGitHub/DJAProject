using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    Vector3 destination;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = new Vector3(Random.Range(600.0f, 700.0f), 32.3f, Random.Range(2400.0f, 2450.0f));
        agent.destination = destination;
    }

    //z = 2400 - 2450
    //x = 600 - 700

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Pos: " + agent.transform.position + "\n Destino: " + destination);

        //if (Vector3.Distance(agent.transform.position, player.transform. position) > VALOR) {
            // Update destination if the target moves one unit
            if (Vector3.Distance(agent.transform.position, destination) < 8.0f)
            {
                Debug.Log(agent.transform.position + destination);
                destination = new Vector3(Random.Range(600.0f, 700.0f), 32.3f, Random.Range(2400.0f, 2450.0f));
                agent.destination = destination;
            }
        //} else ---> PERSEGUIÇÃO
            //agent.destination = player.transform.position;

    }
}
