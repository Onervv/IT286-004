using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MoveBullet : MonoBehaviour
{
    public Vector3 hitPoint;
    //public GameObject dirt;
    public GameObject blood;
    public int speed = 3000;

    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce((hitPoint - this.transform.position).normalized * speed);
        Destroy(this.gameObject,1f);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision.gameObject);
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(10);
            //GameObject newBlood = Instantiate(blood, this.transform.position, this.transform.rotation);
            //newBlood.transform.parent = collision.transform;
            Destroy(gameObject);
        }
        else
        {
            //Instantiate(dirt, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }

    
}