using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    bool isJump = false;
    float time=0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        if (Input.GetKey(KeyCode.W))
        {
            rb.transform.position += Vector3.forward * 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.transform.position -= Vector3.forward * 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.transform.position -= Vector3.right * 10 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.position += Vector3.right * 10 * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && isJump == false)
        {
            time = 0;
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            isJump = true;
        }
        if (isJump == true)
        {
            time += Time.deltaTime;
            
        }
       
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground" )
        {
            if (time > 1) isJump = false;
        }
    }
}