using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // ½Ì±ÛÅæ ÀÎ½ºÅÏ½º
    public float volume = 1.0f; // ±âº» º¼·ý °ª

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}