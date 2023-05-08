using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public GameObject[] characters; // ĳ���� ������Ʈ �迭
    public AudioClip[] musics; // ���� Ŭ�� �迭

    public Dropdown characterDropdown; // ĳ���� ���� ��Ӵٿ� UI
    public Slider soundSlider; // ���� �����̴� UI

    private AudioSource audioSource; // ����� �ҽ� ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        soundSlider.value = audioSource.volume;
    }

    // ĳ���� ���� ��Ӵٿ� �� ���� �̺�Ʈ ó�� �Լ�
    public void OnCharacterDropdownValueChanged()
    {
        int index = characterDropdown.value;
        // ������ ĳ���� ������Ʈ Ȱ��ȭ
        for (int i = 0; i < characters.Length; i++)
        {
            if (i == index)
                characters[i].SetActive(true);
            else
                characters[i].SetActive(false);
        }
        // ������ ĳ���Ϳ� �´� ���� Ŭ�� ����
        audioSource.clip = musics[index];
    }

    // ���� �����̴� �� ���� �̺�Ʈ ó�� �Լ�
    public void OnSoundSliderValueChanged()
    {
        audioSource.volume = soundSlider.value;
    }
}
