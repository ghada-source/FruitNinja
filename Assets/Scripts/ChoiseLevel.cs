using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class ChoiseLevel : MonoBehaviour
{
    public string host = "mysql-evhiprojet.alwaysdata.net";
    public string db = "evhiprojet_bdd";
    public string user = "253116";
    public string mdp = "Musique12";
    public MySqlConnection connec;
    public MySqlConnection con;
    

    public static string level;
    public Button yourButton;
    public loginSystem logi;

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

    void Start()
    {
        
        yourButton.onClick.AddListener(() => TaskOnClick(yourButton));
    }

 

    void TaskOnClick(Button mybutton)
    {
        if (mybutton.gameObject.tag == "Facile")
        {
            level = "Facile";
            DataHolder.bombRate = (float)0.0;
            DataHolder.freq = 5;
            DataHolder.timer = 60;
            DataHolder.mode = 0;
            DataHolder.gameSpeed = 2;
            SceneManager.LoadScene("Prototype 5");

           
        }
        else if (mybutton.gameObject.tag == "Difficile")
        {
            level = "Difficile";
        
        }
        else 
        {
            level = "Moyen";
           
        }
        
        Debug.Log("level =" + level);
        
        
        ConnectBDD();

        //on ajoute le level a l'utilisateur 
        //UPDATE `Users` SET `level`= 'facile' WHERE `pseudo`= 'ghada'
        string commandsql = "UPDATE `Users` SET `level` = '" + level.ToString() + "' WHERE  `pseudo` = '" + loginSystem.pseudo.ToString() + "'";

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



        
        SceneManager.LoadScene("Prototype 5");
    }
}