using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    private GameObject manager;
    bool trown;
    public AudioClip cut;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        trown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Rigidbody>().position.y > 0)
        {
            trown = true;
        }
        if (gameObject.GetComponent<Rigidbody>().velocity.y < 0 && gameObject.GetComponent<Rigidbody>().position.y < 0 && trown==true)
        {
            //Add here code for 1 fruit missed
            GameObject.Find("Trower").GetComponent<LevelController>().fails += 1;
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        source.PlayOneShot(cut);
        Debug.Log(gameObject.name);
        if (gameObject.name == "apple(Clone)")
        {
            GameObject.Find("Trower").GetComponent<LevelController>().fruitListre[0] += 1;
        }
        if (gameObject.name == "banana(Clone)")
        {
            GameObject.Find("Trower").GetComponent<LevelController>().fruitListre[1] += 1;
        }
        if (gameObject.name == "kiwi(Clone)")
        {
            GameObject.Find("Trower").GetComponent<LevelController>().fruitListre[2] += 1;
        }
        if (gameObject.name == "orange(Clone)")
        {
            GameObject.Find("Trower").GetComponent<LevelController>().fruitListre[3] += 1;
        }
        if (gameObject.name == "watermelon(Clone)")
        {
            GameObject.Find("Trower").GetComponent<LevelController>().fruitListre[4] += 1;
        }
        GameObject.Find("Trower").GetComponent<LevelController>().score += 1;
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
