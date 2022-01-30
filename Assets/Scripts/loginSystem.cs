using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class loginSystem : MonoBehaviour

{
    string host = "mysql-evhiprojet.alwaysdata.net";
    string db = "evhiprojet_bdd";
    string user = "253116";
    string mdp = "Musique12";
    MySqlConnection connec;
    MySqlConnection con;
    public Text txtstate;
    public InputField IfLogin;
    public InputField IfConnect;

    public static string pseudo;
    public static string id;
    public static string scoreJeu;
    public static string typeJ;
    public static int nouveau;



    // Start is called before the first frame update
    void ConnectBDD()
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
            txtstate.text = Ex.ToString();
        }
    }


    public void Login()

    {
        //permet d'ajouter un nouvelle utilisateur
        ConnectBDD();
        bool Exist = false;

        //verifier que l'utilisateur n'existe pas deja

        MySqlCommand commandsql = new MySqlCommand("SELECT `pseudo`, `score`, `ID` , `typeJoueur` FROM `Users` WHERE (pseudo ='" + IfLogin.text + "')", connec);

        MySqlDataReader MyReader = commandsql.ExecuteReader();
        while (MyReader.Read())
        {
            if (MyReader["pseudo"].ToString() != "")
            {
                txtstate.text = "Pseudo Exist";
                Exist = true;
            }
        }
        MyReader.Close();


        if (!Exist)
        {

            // insert le nouveau joueur
            string command = "INSERT INTO `Users`(`pseudo`, `typeJoueur`) VALUES('" + IfLogin.text + "'," + "'debutant')";

            MySqlCommand cmd = new MySqlCommand(command, connec);

            try
            {
                cmd.ExecuteReader();


            }
            catch (IOException Ex)
            {

                txtstate.text = Ex.ToString();
            }
            string name = IfLogin.text;
            SceneManager.LoadScene("Scene3");

            cmd.Dispose();

            Debug.Log(name);



            // recupere les info du joueur

            MySqlCommand commandsql1 = new MySqlCommand("SELECT * FROM `Users` WHERE (pseudo ='" + name + "')", con);
            MySqlDataReader MyR = commandsql1.ExecuteReader();


            try
            {

                while (MyR.Read())
                {

                    if (MyR["pseudo"].ToString() != "")
                    {

                        id = MyR["ID"].ToString();
                        scoreJeu = MyR["score"].ToString();
                    }

                }
                MyR.Close();
            }
            catch (IOException Ex)
            {

                txtstate.text = Ex.ToString();
            }

            pseudo = IfLogin.text;
            typeJ = "debutant";
            nouveau = 1; //on a un nouveau joueur
            recupMatrice(nouveau, pseudo);
            commandsql1.Dispose();
            con.Close();



        }

    }



    public void Connect()
    { // permet de se connecter
        ConnectBDD();
        bool Exist = false;

        //Si l'utilisateur existe

        MySqlCommand commandsql = new MySqlCommand("SELECT `pseudo`, `score`, `ID`, `typeJoueur` FROM `Users` WHERE (pseudo ='" + IfConnect.text + "')", connec);
        MySqlDataReader MyReader = commandsql.ExecuteReader();

        while (MyReader.Read())
        {

            if (MyReader["pseudo"].ToString() != "")
            {
                id = MyReader["ID"].ToString();
                scoreJeu = MyReader["score"].ToString();
                pseudo = IfConnect.text;
                typeJ = MyReader["typeJoueur"].ToString();
                nouveau = 0;
                recupMatrice(nouveau, pseudo);

                SceneManager.LoadScene("Scene3");

            }
            Exist = true;
        }

        MyReader.Close();


        if (Exist == false)
        {
            txtstate.text = "Pseudo Not Exist";
        }



        connec.Close();
        Debug.Log(Exist);



    }

    /*
    void creerDos()
    {
        //cree le dossier des traces
        string folderPath = "Log";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Console.WriteLine(folderPath);

        }

    }
    */


    //on recupere les données du joueurs
    void recupMatrice(int nouv, string psd)
    {
        if (nouv == 1) // si le joueur est nouveau 
        {
            creeFichierVide(psd);
        }

        //on recupere les données
       
        string filePath = psd + "Transition.txt"; // or whatever the path is
        string readText = File.ReadAllText(filePath);
        for (int i =0; i<readText.Length; i++)
        {
            Debug.Log(readText.Length);
            Debug.Log(readText[i]);
        }
        Debug.Log(readText);
        


    }


    //si le joueur n'existe pas on 
    void creeFichierVide(string psd)
    {
        
        // on cree le fichier occurence vide 
        string path = psd + "Occu.txt";
        File.CreateText(path).Dispose();
        using (TextWriter writer = new StreamWriter(path, false))
        {
            writer.WriteLine("0 0 0");
            writer.WriteLine("0 0 0");
            writer.WriteLine("0 0 0");
            writer.Close();
        }

        //on cree le fichier avec les transition vide
        path = psd + "Transition.txt";
        File.CreateText(path).Dispose();
        using (TextWriter writer = new StreamWriter(path, false))
        {
            for (int i = 0; i < 9; i++)
            {
                writer.WriteLine("0 0 0 0 0 0 0 0 0");
            }
            writer.Close();
        }

        //on cree le fichier des traces vide
        path = psd + "Trace.txt";
        File.CreateText(path).Dispose();
      
    }


}