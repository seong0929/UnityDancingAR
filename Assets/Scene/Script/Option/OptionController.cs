using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // 저장된 볼륨 값 로드
        GameManager gameManager = GameManager.instance;
        volumeSlider.value = gameManager.volume;

        // 슬라이더의 값이 변경될 때마다 OnVolumeChanged 메서드를 호출하도록 함
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float value)
    {
        // 전체 오디오 소스의 볼륨 값을 변경
        GameManager gameManager = GameManager.instance;
        gameManager.volume = value;
    }
}
