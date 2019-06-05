using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody rb;
    [SerializeField] float turnSpeed;
    [SerializeField] GameObject camera;
    [SerializeField] bool canJump = false;
    float fallMult = 2.5f;
    float rotationY = 0;
    float health, maxHealth = 100.0f;
    float timer = 0.0f;
    public TextMeshProUGUI healthCount;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        health = maxHealth;
        healthCount.text = "Health: " + health;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        float movX = Input.GetAxisRaw("Horizontal");                                                                                                    //Input esquerda/direita
        float movZ = Input.GetAxisRaw("Vertical");                                                                                                      //Input frente/tras
        Vector3 xMov = transform.right * movX;                                                                                                          //Movimento esquerda/direita
        Vector3 zMov = transform.forward * movZ;                                                                                                        //Movimento frente/tras
        Vector3 mov = (xMov + zMov).normalized * speed;                                                                                                 //Movimento
        rb.MovePosition(rb.position + mov);

        float turnX = Input.GetAxisRaw("Mouse X") * turnSpeed;                                                                                          //Rotação horizontal
        rotationY += Input.GetAxisRaw("Mouse Y") * turnSpeed;                                                                                           //Rotação vertical total
        rotationY = Mathf.Clamp(rotationY, -45, 60);                                                                                                    //Limitação do angulo vertical
        camera.transform.localEulerAngles = new Vector3(-rotationY, camera.transform.localEulerAngles.y, camera.transform.localEulerAngles.z);          //Atualização do angulo vertical
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, turnX, 0)));                                                                      //Rotação sob o eixo horizontal
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
            rb.velocity += new Vector3(0, 5, 0);
        if (rb.velocity.y < 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMult - 1) * Time.fixedDeltaTime;

        Debug.Log(health);

        if (health <= 0.0f)
            SceneManager.LoadScene("SampleScene");
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contact = new ContactPoint[1];
        collision.GetContacts(contact);
        int contactN = 0;
        foreach(ContactPoint cont in contact){
            if (cont.point.y < transform.position.y)
                contactN++;
        }
        if (contactN == contact.Length)
            canJump = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        ContactPoint[] contact = new ContactPoint[1];
        collision.GetContacts(contact);
        int contactN = 0;
        foreach (ContactPoint cont in contact)
        {
            if (cont.point.y < transform.position.y)
                contactN++;
        }
        if (contactN == contact.Length)
            canJump = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "zombie" && timer >= 2.0f)
        {
            timer = 0.0f;
            health -= 10.0f;
            healthCount.text = "Health: " + health;
        }
    }
}
