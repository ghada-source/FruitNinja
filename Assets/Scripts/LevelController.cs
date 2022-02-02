using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public GameObject apple;
    public GameObject banana;
    public GameObject bomb;
    public GameObject kiwi;
    public GameObject orange;
    public GameObject watermelon;
    public int fails=0;
    public List<float> fruitListre= new List<float>();
    public List<float> fruitListco= new List<float>();
    public List<GameObject> fruits= new List<GameObject>();
    public GameObject boum;
    public AudioClip boumsound;
    public AudioSource source;
    public AudioClip cutsound;
    public int score;
    public TextMeshProUGUI scoret;
    public TextMeshProUGUI starttext;
    public TextMeshProUGUI gameo;
    public TextMeshProUGUI retry;
    public TextMeshProUGUI suc;
    public TextMeshProUGUI failu;

    public float elapsed;
    public bool gameOver=false;
    int typeJoueur;
    public int mode;
    int freq;
    float gameSpeed;
    float bombRate;
    private IEnumerator coroutine;
    public bool finished;

    // Start is called before the first frame update
    void Start()
    {
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
        score = 0;
        scoret.text = "Score : " + score;
        failu.text = "Fails : " + fails;
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        scoret.text = "Score : " + score;
        failu.text = "Fails : " + fails;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (finished == false)
            {
                starttext.gameObject.SetActive(false);
                levelAdaptator();
                StartCoroutine(coroutine);
            }
            if (finished == true)
            {
                SceneManager.LoadScene("Prototype 5");
            }
        }
        if (Input.GetKeyDown(KeyCode.Backspace) && finished == true)
        {
            SceneManager.LoadScene("Scene3");
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
        while (fails < 3 && elapsed < DataHolder.timer && gameOver==false)
        {
            //yield return new WaitForSeconds(gameSpeed);
            yield return 0;
            elapsed += Time.deltaTime;
            gametime += Time.deltaTime;
            if (gametime > gameSpeed)
            {
                gametime = 0;
                if (nfreq == freq)
                {
                    nfreq = 0;
                    if (mode == 0)
                    {
                        area = argmin(loginSystem.tableauOccurences);
                    }
                    else
                    {

                        area = argmin(loginSystem.occurencesTransition[DataHolder.newdata]);
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
        DataHolder.lastScore = score;
        finished = true;
        if (fails >= 3 || gameOver==true)
        {
            //game over code here
            gameOver = true;
            gameo.gameObject.SetActive(true);
        }
        if (elapsed >= DataHolder.timer)
        {
            suc.gameObject.SetActive(true);
            //end of level code here
        }
        retry.gameObject.SetActive(true);
    }

    public void levelAdaptator()
    {
        freq = DataHolder.freq;
        bombRate = DataHolder.bombRate;
        gameSpeed = DataHolder.gameSpeed;
        if (loginSystem.typeJ == "debutant")
        {
            freq= (int)DataHolder.freq /2;
            bombRate = DataHolder.bombRate /2;
            gameSpeed = DataHolder.gameSpeed *2;
        }
        if (loginSystem.typeJ == "expert")
        {
            freq = (int)Math.Ceiling(DataHolder.freq * 1.5);
            bombRate = (float)(DataHolder.bombRate * 1.5);
            if (DataHolder.bombRate > 0.9)
            {
                bombRate = (float)0.9;
            }
            gameSpeed = (float)(DataHolder.gameSpeed / 1.5);
        }
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
                if (Double.IsNaN(datatab[g]) == true)
                {
                    datatab[g] = (float)0.0;
                }
            }
            chof = argmin(datatab);
        }
        fruitListco[chof] += 1;
        GameObject app = Instantiate(fruits[chof], fruits[chof].transform);
        app.gameObject.AddComponent<FruitController>();
        Rigidbody b = app.GetComponent<Rigidbody>();
        app.GetComponent<FruitController>().cut = cutsound;
        app.GetComponent<FruitController>().source = source;
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
        app.gameObject.AddComponent<BombController>();
        app.GetComponent<BombController>().explosion =boum;
        app.GetComponent<BombController>().boum = boumsound;
        app.GetComponent<BombController>().source = source;
        app.transform.position = (new Vector3((float)x, (float)y, 0));
        b.AddForce(Vector3.up * 14, ForceMode.Impulse);
        b.AddTorque(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), ForceMode.Impulse);
    }

}
