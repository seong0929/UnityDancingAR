using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider; // �����̴� UI ������Ʈ
    private GameManager gameManager; // GameManager ����

    private void Start()
    {
        // GameManager �ν��Ͻ� ��������
        gameManager = GameManager.instance;

        // �����̴��� �� ����
        volumeSlider.value = gameManager.volume;

        // �����̴� �� ���� �̺�Ʈ�� �Լ� ����
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    // ���� ���� �̺�Ʈ �ڵ鷯
    private void OnVolumeChanged(float value)
    {
        // GameManager�� ���� �� ������Ʈ
        gameManager.volume = value;
    }
}

