using UnityEngine;

public class AudioController : MonoBehaviour
{
    void Start()
    {
        // GameManager에서 저장된 볼륨 값 로드
        GameManager gameManager = GameManager.instance;
        float savedVolume = gameManager.volume;

        // 모든 instant된 객체에 있는 AudioSource 컴포넌트의 볼륨 값을 변경
        AudioSource[] audioSources = Resources.FindObjectsOfTypeAll<AudioSource>();
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = savedVolume;
        }
    }

    // 오브젝트를 인스턴트화 할 때마다 호출되는 함수
    public void PlaySoundOnInstantiate(GameObject obj, AudioClip clip)
    {
        // AudioSource 컴포넌트 추가
        AudioSource audioSource = obj.AddComponent<AudioSource>();

        // clip을 AudioSource에 할당
        audioSource.clip = clip;

        // GameManager에서 저장된 볼륨 값 로드
        GameManager gameManager = GameManager.instance;
        audioSource.volume = gameManager.volume;

        // 재생
        audioSource.Play();
    }
}
