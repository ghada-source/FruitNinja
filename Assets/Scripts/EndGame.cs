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
    public loginSystem log;
    void endplay()
    {
        /*log.ConnectBDD();
        MySqlCommand commandsql = new MySqlCommand("SELECT `pseudo`, `score`, `ID` , `typeJoueur` FROM `Users` WHERE (pseudo ='" + IfLogin.text + "')", connec);

        MySqlDataReader MyReader = commandsql.ExecuteReader();*/
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
