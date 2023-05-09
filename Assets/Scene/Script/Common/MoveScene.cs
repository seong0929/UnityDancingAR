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
    /*
     // 다음 씬으로 이동하며 데이터를 넘겨주는 코드
    Scene nextScene = SceneManager.LoadScene("다음 씬 이름");
    nextScene.param = 데이터 객체;

    // 다음 씬에서 데이터를 받아오는 코드
    DataClass data = SceneManager.GetSceneParam<DataClass>();
    */
}