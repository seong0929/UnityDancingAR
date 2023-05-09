using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // �̱��� �ν��Ͻ�
    public AudioClip soundClip; // �ν���Ʈȭ �� �� ����� ����� Ŭ��
    public float volume = 1.0f; // �⺻ ���� ��

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

    // ������Ʈ�� �ν���Ʈȭ �� ������ ȣ��Ǵ� �Լ�
    public void PlaySoundOnInstantiate(GameObject obj)
    {
        // AudioSource ������Ʈ �߰�
        AudioSource audioSource = obj.AddComponent<AudioSource>();

        // GameManager���� soundClip�� �����ͼ� AudioSource�� �Ҵ�
        audioSource.clip = soundClip;

        // ���� ����
        audioSource.volume = volume;

        // ���
        audioSource.Play();
    }
}
