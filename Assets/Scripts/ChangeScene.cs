
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;



public class ChangeScene : MonoBehaviour
{

    public void ChangScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();

    }


}