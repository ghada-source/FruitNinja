using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMatrice : MonoBehaviour
{
    // Start is called before the first frame update
 
    
   

    public int findIndex(float newdataX, float newdataY)

    {
        
        if (newdataX < 3 & newdataY < 3)
        {
            return 0;
        }
        else if (newdataX > 3 & newdataX < 3 & newdataY < 3)
        {
            return 1;
        }
        else if (newdataX > 3 & newdataY < 3)
        {
            return 2;
        }
        else if (newdataX < 3 & newdataY > 3 & newdataY < 3)
        {
            return 3;
        }
        else if (newdataX > 3 & newdataX < 3 & newdataY > 3 & newdataY < 3)
        {
            return 4;
        }
        else if (newdataX > 3  & newdataY > 3 & newdataY < 3)
        {
            return 5;
        }
        else if (newdataX < 3 & newdataY > 3)
        {
            return 6;
        }
        else if (newdataX > 3 & newdataX < 3 & newdataY > 3 )
        {
            return 7;
        }

        return 8;

    }

    public void OccuTransition(float newdataX, float newdataY, float lastTraceX, float lastTraceY)
    {
        List<List<float>> occurencesTransition = loginSystem.occurencesTransition;
        int i = findIndex(newdataX, newdataY);
        int j = findIndex(lastTraceX, lastTraceY);
        occurencesTransition[i][j] =1 + occurencesTransition[i][j];
        loginSystem.occurencesTransition = occurencesTransition;
    }


    public void analysisData(float newdataX, float newdataY)
    {
        List<float> tableauOccurences = loginSystem.tableauOccurences;
        List<List<float>> traces = loginSystem.Traces;
        //List<List<float>> occurencesTransition = loginSystem.occurencesTransition;

        List<float> lastTrace = traces[traces.Count - 1]; //on recupere la derniere traces enregistré
        
        List<float> slist = new List<float> { newdataX , newdataY}; 
        traces.Add(slist); //on ajoute la nouvelle trace

        int i = findIndex(newdataX, newdataY);
        tableauOccurences[i] =1 + tableauOccurences[i]; // on met a jour les occurences
        OccuTransition(newdataX, newdataY, slist[0], slist[1]);

        //on met a jour les variables global
        loginSystem.Traces = traces;
        loginSystem.tableauOccurences = tableauOccurences;

    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
