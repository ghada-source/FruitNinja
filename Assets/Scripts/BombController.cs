using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject explosion;
    public AudioClip boum;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        GameObject.Find("Trower").GetComponent<LevelController>().gameOver=true;
        GameObject o = Instantiate(explosion, transform.position, explosion.transform.rotation);
        o.transform.localScale = new Vector3(10, 10, 10);
        source.PlayOneShot(boum);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.GetComponent<Rigidbody>().velocity.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
