using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    private GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Rigidbody>().angularVelocity.y < 0 && gameObject.GetComponent<Rigidbody>().position.y < 0)
        {
            //Add here code for 1 fruit missed
            Debug.Log("hehe");
            GameObject.Find("Trower").GetComponent<LevelController>().fails += 1;
            Destroy(gameObject);
        }
    }
}
