using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public GameObject[] characters; // 캐릭터 오브젝트 배열
    public AudioClip[] musics; // 음악 클립 배열

    public Dropdown characterDropdown; // 캐릭터 선택 드롭다운 UI
    public Slider soundSlider; // 사운드 슬라이더 UI

    private AudioSource audioSource; // 오디오 소스 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        soundSlider.value = audioSource.volume;
    }

    // 캐릭터 선택 드롭다운 값 변경 이벤트 처리 함수
    public void OnCharacterDropdownValueChanged()
    {
        int index = characterDropdown.value;
        // 선택한 캐릭터 오브젝트 활성화
        for (int i = 0; i < characters.Length; i++)
        {
            if (i == index)
                characters[i].SetActive(true);
            else
                characters[i].SetActive(false);
        }
        // 선택한 캐릭터에 맞는 음악 클립 설정
        audioSource.clip = musics[index];
    }

    // 사운드 슬라이더 값 변경 이벤트 처리 함수
    public void OnSoundSliderValueChanged()
    {
        audioSource.volume = soundSlider.value;
    }
}
