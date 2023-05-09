using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 인스턴스
    public AudioClip soundClip; // 인스턴트화 될 때 재생될 오디오 클립
    public float volume = 1.0f; // 기본 볼륨 값

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

    // 오브젝트를 인스턴트화 할 때마다 호출되는 함수
    public void PlaySoundOnInstantiate(GameObject obj)
    {
        // AudioSource 컴포넌트 추가
        AudioSource audioSource = obj.AddComponent<AudioSource>();

        // GameManager에서 soundClip을 가져와서 AudioSource에 할당
        audioSource.clip = soundClip;

        // 볼륨 조절
        audioSource.volume = volume;

        // 재생
        audioSource.Play();
    }
}
