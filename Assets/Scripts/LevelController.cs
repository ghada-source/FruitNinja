using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject apple;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("yo");
            GameObject app=Instantiate(apple, apple.transform);
            Rigidbody b = app.GetComponent<Rigidbody>();
            b.AddForce(Vector3.up * 18, ForceMode.Impulse);
            b.AddTorque(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10), ForceMode.Impulse);

            app.transform.position = new Vector3(Random.Range(-4, 4), -6);
        }
    }

    public void TrowFruit()
    {
        Rigidbody b = apple.GetComponent<Rigidbody>();
        b.AddForce(Vector3.up * Random.Range(12, 16), ForceMode.Impulse);
        b.AddTorque(Random.Range(-10, 10),Random.Range(-10, 10), Random.Range(-10, 10),ForceMode.Impulse);

        apple.transform.position = new Vector3(Random.Range(-4, 4), -6);
    }

}
