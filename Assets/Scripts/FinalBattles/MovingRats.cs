using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRats : MonoBehaviour
{
public float speed = 5f;
    private bool moveLeft = true;
    public static bool shouldMove = false;
    private Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        originalPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    public void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Barrier"))
        {
            transform.position = originalPos;
            shouldMove = false;

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("Found rb");
                rb.velocity = Vector3.zero; //Stop movement;
                rb.angularVelocity = Vector3.zero; //Stop rotation;
            }
            // gameObject.GetComponent<MeshRenderer>().enabled = false;
            // if (moveLeft)
            // {
            //     transform.position += Vector3.left * 3f;
            //     moveLeft = false;
            // }
            // else
            // {
            //     moveLeft = true;
            // }
        }
    }
}
