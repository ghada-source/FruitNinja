using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    public GameObject apple;
    public GameObject banana;
    public GameObject bomb;
    public GameObject kiwi;
    public GameObject orange;
    public GameObject watermelon;
    public int fails=0;
    public List<float> tableauOccurences = new List<float>();
    public List<List<float>> occurencesTransition= new List<List<float>>();
    public List<float> fruitListre= new List<float>();
    public List<float> fruitListco= new List<float>();
    public List<GameObject> fruits= new List<GameObject>();

    public int newdata;
    public float elapsed;
    int typeJoueur=0;
    public int mode=0;
    int freq=5;
    int timer=20;
    float gameSpeed=1;
    float bombRate = (float)0.5;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i< 9; i++) {
            tableauOccurences.Add(0);
            List<float> u = new List<float>();
            occurencesTransition.Add(u);
            for (int k = 0; k < 9; k++)
            {
                occurencesTransition[i].Add(0);
            }
        }
        for (int i = 0; i < 5; i++)
        {
            fruitListco.Add(0);
            fruitListre.Add(0);
        }
        fruits.Add(apple);
        fruits.Add(banana);
        fruits.Add(kiwi);
        fruits.Add(orange);
        fruits.Add(watermelon);
        coroutine = LevelGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            levelAdaptator();
            StartCoroutine(coroutine);
        }
    }

    public int argmin(List<float> tab)
    {
        int idmax = -1;
        float value = Int32.MaxValue;
        for (int i = 0; i < tab.Count; i++)
        {
            if (tab[i] < value)
            {
                idmax = i;
                value = tab[i];
            }
        }
        return idmax;
    }

        IEnumerator LevelGenerator()
    {
        fails = 0;
        elapsed = 0;
        int nfreq = 0;
        float gametime = 0;
        int area = 0;
        while (fails < 3 && elapsed < timer)
        {
            yield return new WaitForSeconds(gameSpeed);
            elapsed += Time.deltaTime;
            gametime += Time.deltaTime;
            Debug.Log(Time.deltaTime);
            gametime = 0;
            if (nfreq == freq)
            {
                nfreq = 0;
                if (mode == 0)
                {
                    area = argmin(tableauOccurences);
                }
                else
                {
                    area = argmin(occurencesTransition[newdata]);
                }
            }
            else
            {
                area = UnityEngine.Random.Range(0, 8);
                nfreq += 1;
            }
            float p = UnityEngine.Random.Range((float)0, (float)1);
            if (p <= bombRate)
            {
                TrowBomb(area);
            }
            else
            {
                TrowFruit(area);
            }
        }
    }

    public void levelAdaptator()
    {
        if (typeJoueur == 0)
        {
            freq= (int) freq/2;
            bombRate = bombRate/2;
            gameSpeed = gameSpeed*2;
        }
        if (typeJoueur == 1)
        {
            freq = (int)Math.Ceiling(freq * 1.5);
            bombRate = (float)(bombRate * 1.5);
            if (bombRate > 0.9)
            {
                bombRate = (float)0.9;
            }
            gameSpeed = (float)(gameSpeed / 1.5);
        }
    }



    private void TrowBomb(object area)
    {
        throw new NotImplementedException();
    }

    public void TrowFruit(int area)
    {
        int chof;
        float x = 1;
        float y = 1;
        if (area == 0)
        {
             x = Random.Range((float)-4.7, (float)-1.5);
             y = Random.Range((float)-9.4, (float)-6.2);
        }
        if (area == 1)
        {
             x = Random.Range((float)-4.7, (float)-1.5);
             y = Random.Range((float)-6.2, (float)-3.1);
        }
        if (area == 2)
        {
             x = Random.Range((float)-4.7, (float)-1.5);
             y = Random.Range((float)-3.1, (float)0);
        }
        if (area == 3)
        {
             x = Random.Range((float)-1.5, (float)1.6);
             y = Random.Range((float)-9.4, (float)-6.2);
        }
        if (area == 4)
        {
             x = Random.Range((float)-1.5, (float)1.6);
             y = Random.Range((float)-6.2, (float)-3.1);
        }
        if (area == 5)
        {
             x = Random.Range((float)-1.5, (float)1.6);
             y = Random.Range((float)-3.1, (float)0);
        }
        if (area == 6)
        {
             x = Random.Range((float)1.6, (float)4.7);
             y = Random.Range((float)-9.4, (float)-6.2);
        }
        if (area == 7)
        {
             x = Random.Range((float)1.6, (float)4.7);
             y = Random.Range((float)-6.2, (float)-3.1);
        }
        if (area == 8)
        {
             x = Random.Range((float)1.6, (float)4.7);
             y = Random.Range((float)-3.1, (float)0);
        }
        float p = UnityEngine.Random.Range((float)0, (float)1); 
        if (p > 0.3)
        {
            chof = Random.Range(0, fruits.Count-1);
        }
        else 
        {
            List<float> datatab =new List<float>();
            for (int g = 0; g < fruits.Count; g++)
            {
                datatab.Add(fruitListre[g] / fruitListco[g]);
            }
            chof = argmin(datatab);
        }
        fruitListco[chof] += 1;
        GameObject app = Instantiate(fruits[chof], fruits[chof].transform);
        app.gameObject.AddComponent<FruitController>();
        Rigidbody b = app.GetComponent<Rigidbody>();
        app.transform.position = (new Vector3((float)x, (float)y, 0));
        b.AddForce(Vector3.up * 14, ForceMode.Impulse);
        b.AddTorque(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), ForceMode.Impulse);
    }

    public void TrowBomb(int area)
    {
        float x = 1;
        float y = 1;
        if (area == 0)
        {
            x = Random.Range((float)-4.7, (float)-1.5);
            y = Random.Range((float)-9.4, (float)-6.2);
        }
        if (area == 1)
        {
            x = Random.Range((float)-4.7, (float)-1.5);
            y = Random.Range((float)-6.2, (float)-3.1);
        }
        if (area == 2)
        {
            x = Random.Range((float)-4.7, (float)-1.5);
            y = Random.Range((float)-3.1, (float)0);
        }
        if (area == 3)
        {
            x = Random.Range((float)-1.5, (float)1.6);
            y = Random.Range((float)-9.4, (float)-6.2);
        }
        if (area == 4)
        {
            x = Random.Range((float)-1.5, (float)1.6);
            y = Random.Range((float)-6.2, (float)-3.1);
        }
        if (area == 5)
        {
            x = Random.Range((float)-1.5, (float)1.6);
            y = Random.Range((float)-3.1, (float)0);
        }
        if (area == 6)
        {
            x = Random.Range((float)1.6, (float)4.7);
            y = Random.Range((float)-9.4, (float)-6.2);
        }
        if (area == 7)
        {
            x = Random.Range((float)1.6, (float)4.7);
            y = Random.Range((float)-6.2, (float)-3.1);
        }
        if (area == 8)
        {
            x = Random.Range((float)1.6, (float)4.7);
            y = Random.Range((float)-3.1, (float)0);
        }
        GameObject app = Instantiate(bomb, bomb.transform);
        Rigidbody b = app.GetComponent<Rigidbody>();
        app.transform.position = (new Vector3((float)x, (float)y, 0));
        b.AddForce(Vector3.up * 14, ForceMode.Impulse);
        b.AddTorque(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), ForceMode.Impulse);
    }

}
