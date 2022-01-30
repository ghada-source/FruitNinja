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


    public static string level;
    public Button yourButton;
    public loginSystem logi;


    void Start()
    {
        
     
        yourButton.onClick.AddListener(() => TaskOnClick(yourButton));
    }

 

    void TaskOnClick(Button mybutton)
    {
        if (mybutton.gameObject.tag == "Facile")
        {
            level = "Facile";
           
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
        // UPDATE `Users` SET `level`= '[value-5]' WHERE `pseudo`= 'ghada'
        /*
        logi.ConnectBDD();

        string commandsql = "UPDATE `Users` SET `level` = '"+ level +"' WHERE 'pseudo' ='" + loginSystem.pseudo + "'";

        MySqlCommand cmd = new MySqlCommand(commandsql, loginSystem.connec);
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
        loginSystem.con.Close();

        */
        SceneManager.LoadScene("Prototype 5");
    }
}