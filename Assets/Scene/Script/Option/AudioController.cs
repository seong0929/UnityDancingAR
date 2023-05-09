using UnityEngine;

public class AudioController : MonoBehaviour
{
    void Start()
    {
        // GameManager���� ����� ���� �� �ε�
        GameManager gameManager = GameManager.instance;
        float savedVolume = gameManager.volume;

        // ��� instant�� ��ü�� �ִ� AudioSource ������Ʈ�� ���� ���� ����
        AudioSource[] audioSources = Resources.FindObjectsOfTypeAll<AudioSource>();
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = savedVolume;
        }
    }

    // ������Ʈ�� �ν���Ʈȭ �� ������ ȣ��Ǵ� �Լ�
    public void PlaySoundOnInstantiate(GameObject obj, AudioClip clip)
    {
        // AudioSource ������Ʈ �߰�
        AudioSource audioSource = obj.AddComponent<AudioSource>();

        // clip�� AudioSource�� �Ҵ�
        audioSource.clip = clip;

        // GameManager���� ����� ���� �� �ε�
        GameManager gameManager = GameManager.instance;
        audioSource.volume = gameManager.volume;

        // ���
        audioSource.Play();
    }
}
