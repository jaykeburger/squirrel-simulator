using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision other)
    {
        //We can add a bullet decal but right we don't have any.
        //
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(player.transform);
        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
        transform.position += transform.forward * 3f * Time.deltaTime;
    }
}
