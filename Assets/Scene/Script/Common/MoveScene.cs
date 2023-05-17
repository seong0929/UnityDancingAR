using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void ToHome()
    {
        Debug.Log("Scene: Home");
        SceneManager.LoadScene("Home");
    }
    public void ToOption()
    {
        Debug.Log("Scene: Option");
        SceneManager.LoadScene("Option");
    }
    public void ToDancingAR()
    {
        Debug.Log("Scene: DancingAR");
        SceneManager.LoadScene("DancingAR");
    }
    public void QuitGame()
    {
        Debug.Log("Quit game called");
        Application.Quit();
    }
}