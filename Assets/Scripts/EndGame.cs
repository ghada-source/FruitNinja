using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
public class EndGame : MonoBehaviour
{
   
    public string host = "mysql-evhiprojet.alwaysdata.net";
    public string db = "evhiprojet_bdd";
    public string user = "253116";
    public string mdp = "Musique12";
    public MySqlConnection connec;
    public MySqlConnection con;
    private bool done=false;

    public Button yourButton;

    void Update()
    {
        if (GameObject.Find("Trower").GetComponent<LevelController>().finished == true && done == false)
        {
            done = true;
            Debug.Log("It is alive");
            endGame();
            saveBDD();
            saveFichier();
        }
        
        //yourButton.onClick.AddListener(() => TaskOnClick(yourButton));
    }


    public void saveBDD()
    {
        //mettre a jour la bdd
        ConnectBDD();
        string commandsql = "UPDATE `Users` SET `score` = '" + DataHolder.lastScore + "', `typeJoueur` = '" + loginSystem.typeJ + "'WHERE  `pseudo` = '" + loginSystem.pseudo.ToString() + "'";

        MySqlCommand cmd = new MySqlCommand(commandsql, connec);
        try
        {
            cmd.ExecuteReader();


        }
        catch (IOException Ex)
        {

            Debug.Log("ajout echouer");
        }
        //on met le level dans le bdd
        cmd.Dispose();
        con.Close();

    }

    public void saveFichier()
    {
        //enregistre la occurence

        // on cree le fichier occurence vide 
        string path = loginSystem.pseudo + "Occu.txt";
        File.Delete(path);
        File.CreateText(path).Dispose();
        using (TextWriter writer = new StreamWriter(path, false))
        {
            
            foreach (float item in loginSystem.tableauOccurences)
            { 
               
                writer.WriteLine(item);
                
                
            }
           
            writer.Close();
        }


        //enegristre transition
        // on cree le fichier occurence vide 
        path = loginSystem.pseudo + "Transition.txt";

        File.CreateText(path).Dispose();
        using (TextWriter writer = new StreamWriter(path, false))
        {

            foreach (var sublist in loginSystem.occurencesTransition)
            {
                string line = "";
                foreach (var obj in sublist)
                {

                    line = line + " " + obj;
                }
                Debug.Log(line);

                writer.WriteLine(line);
            }
 
            writer.Close();
        }




    }
    public void endGame()

    {
        ConnectBDD();
        float moyenne =-1;
        float var=-1;

        //SELECT AVG(score) FROM Users WHERE `level`= 'Difficile';
        MySqlCommand commandsql = new MySqlCommand("SELECT AVG(score) FROM `Users` WHERE (`level` ='" + ChoiseLevel.level + "')", connec);

        MySqlDataReader MyReader = commandsql.ExecuteReader();
        while (MyReader.Read())
        {
            if (MyReader["AVG(score)"].ToString() != "")
            {

                moyenne = float.Parse(MyReader["AVG(score)"].ToString());
            }
        }
        MyReader.Close();

        //Debug.Log(moyenne + var);

        commandsql = new MySqlCommand("SELECT VARIANCE(score) FROM `Users` WHERE (`level` ='" + ChoiseLevel.level + "')", connec);
        MySqlDataReader MyR = commandsql.ExecuteReader();
        while (MyR.Read())
        {
            if (MyR["VARIANCE(score)"].ToString() != "")
            {
                var = float.Parse(MyR["VARIANCE(score)"].ToString());
            }
        }

        MyR.Close();
        //Debug.Log(moyenne + var);

        if (DataHolder.lastScore < (moyenne - 1.95 * var))
        {
            loginSystem.typeJ = "debutant";
        }

        else if (DataHolder.lastScore > (moyenne - 1.95 * var) & DataHolder.lastScore < (moyenne + 1.95 * var))
        {
            loginSystem.typeJ = "intermediaire";
        }
        else
        {
            loginSystem.typeJ = "expert";
        }


    }

    public void ConnectBDD()
    {


        string constr = "Server=" + host + ";DATABASE=" + db + ";User ID=" + user + ";Password=" + mdp + ";Pooling=true;Charset=utf8;";

        try
        {
            connec = new MySqlConnection(constr);

            connec.Open();
            con = new MySqlConnection(constr);

            con.Open();
            //txtstate.text = connec.State.ToString();

        }
        catch (IOException Ex)
        {
            Debug.Log("Pas de connexion");
        }
    }
}
