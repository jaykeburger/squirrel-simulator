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
        if (shouldMove == true)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    public void OnTriggerEnter (Collider other)
    {
        Debug.Log("Trigger detected from: " + other.gameObject.name);

        if (other.gameObject.name == "Player")
        {
            Debug.Log("Hit player");
            PlayerState.Instance.DecreaseHealth();
        }

        if (other.CompareTag("Barrier"))
        {
            transform.position = originalPos;
            shouldMove = false;

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; //Stop movement;
                rb.angularVelocity = Vector3.zero; //Stop rotation;
                rb.Sleep();
                Debug.Log("Found rb");
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
