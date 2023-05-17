using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider; // 슬라이더 UI 컴포넌트
    private GameManager gameManager; // GameManager 참조

    private void Start()
    {
        // GameManager 인스턴스 가져오기
        gameManager = GameManager.instance;

        // 슬라이더의 값 설정
        volumeSlider.value = gameManager.volume;

        // 슬라이더 값 변경 이벤트에 함수 연결
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    // 볼륨 변경 이벤트 핸들러
    private void OnVolumeChanged(float value)
    {
        // GameManager의 볼륨 값 업데이트
        gameManager.volume = value;
    }
}

