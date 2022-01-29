using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoiseLevel : MonoBehaviour
{
 
    public static string level;
    public Button yourButton;

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
        SceneManager.LoadScene("Prototype 5");
    }
}