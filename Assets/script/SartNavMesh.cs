using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SartNavMesh : MonoBehaviour
{
    // Start is called before the first frame update
    public bool startNavMesh=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "ground")
            startNavMesh = true;// quando o player entra na area do zombie e começa a ser perseguido pelo mesmo
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "ground")
            startNavMesh = false;// quando o player entra na area do zombie e começa a ser perseguido pelo mesmo
    }
}
