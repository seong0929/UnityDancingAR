using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // ����� ���� �� �ε�
        GameManager gameManager = GameManager.instance;
        volumeSlider.value = gameManager.volume;

        // �����̴��� ���� ����� ������ OnVolumeChanged �޼��带 ȣ���ϵ��� ��
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float value)
    {
        // ��ü ����� �ҽ��� ���� ���� ����
        GameManager gameManager = GameManager.instance;
        gameManager.volume = value;
    }
}
