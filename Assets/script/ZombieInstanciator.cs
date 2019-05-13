using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class ZombieInstanciator : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform zombiePrefab;
    public int number=5;
    public Rigidbody rbPlayer;
    static int i = 0;
    float time = 0;
    private bool isthrow = true;
    public bool startFlocking = false;
    
    private void Start()
    {  //Boid.boids= new Collider[5];
        for (int i = 0; i < number; i++)// cria os zombies para o flocking
        {
            Transform boid =  Instantiate(zombiePrefab, new Vector3(Random.Range(this.transform.position.x-4, this.transform.position.x + 4), this.transform.position.y, Random.Range(this.transform.position.z-4, this.transform.position.z+4)), Quaternion.identity) as Transform;
            ///  Transform boid = Instantiate(fishPrefab, Random.insideUnitSphere * 25, Quaternion.identity) as Transform;
          //  Boid.boids[i] = transform.GetComponent<Collider>() as Collider;
            boid.parent = transform;
            Transform child = boid.parent.GetChild(i);
            child.name = "zombie" + i;
            child.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
            child.GetComponent<NavMeshAgent>().enabled = false;
            //  Boid.boids[i] = child.transform.parent.GetChild(i).GetComponentInChildren<Collider>();

        }
       // Boid.allBoids = boid;
      


    }
    private void Update()
    {
        ThrowsForce();
    }
    private void FixedUpdate()
    {
        
        time += Time.deltaTime;
        Debug.Log("i=" + i);
        if (time <= 4)
        {
            if (transform.Find("zombie" + i) != null)// stop being flock
            {
               

                Transform child = transform.Find("zombie" + i);
              
                if (startFlocking == false && child.GetComponent<NavMeshAgent>().enabled != false)
                {
                    child.GetComponent<NavMeshAgent>().isStopped = true;
                    child.GetComponent<NavMeshAgent>().updateRotation = false;
                    child.GetComponent<NavMeshAgent>().enabled = false;
                    child.rotation = Quaternion.LookRotation(this.transform.position.normalized, Vector3.up);
                }
                Vector3 dir = child.transform.position - rbPlayer.transform.position;
                if (child.GetChild(18).GetComponent<SensorPlayer>().startSeek == true) child.transform.rotation = Quaternion.LookRotation(dir.normalized);
                    //Transform child = boid.parent.GetChild(i);
                    Vector3 distance = rbPlayer.transform.position - child.transform.position;
                if (child.GetChild(18).GetComponent<SensorPlayer>().startSeek == true)// sai do flocking
                {
                    //  child.transform.parent = null;//provavelment não vai fazer nada
                    int b = 0;

                }
                else {
                    
                    //nav.updateRotation = false;
                  //  child.rotation= Quaternion.LookRotation(this.transform.forward);
                }
                if (distance.magnitude <= 25 && child.parent.name == "Flocking")// começam a preseguir o player
                {
                    startFlocking = true;
                    child.GetComponent<NavMeshAgent>().updateRotation = true;
                    child.GetChild(18).GetComponent<SensorPlayer>().startSeek = true;
                    
                    if (distance.magnitude <= 10) child.transform.parent = null;//deixam de ter pai

                }
            }
            
        }
        i++;
        if (i == number)
        {
            i = 0;
            time = 0;
        }
    }

    void ThrowsForce()//força repulsiva que os zombies tem um contra o outro enquanto estão no flocking
    {

        for (int i = 0; i < number; i++) {

            for(int j=0; j < number; j++)
            {
                Transform Child = transform.Find("zombie" + i);
                Transform Child2 = transform.Find("zombie" + j);
                if (Child != null && Child2 != null)
                {
                    Vector3 dir = Child.position - Child2.position;
                    if (dir.magnitude <= 2 && isthrow == true && dir.magnitude != 0)
                    {
                        Vector3 force = dir.normalized;//repulsive force
                        Child2.GetComponent<Rigidbody>().AddForce(-force * 20);
                        Child.GetComponent<Rigidbody>().AddForce(force * 20);
                        isthrow = false;
                    }
                    if (dir.magnitude > 2) isthrow = true;
                }
            }


        }

    }

   
}
