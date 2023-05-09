using UnityEngine;
using UnityEngine.UI;

public class UIScreenManager : MonoBehaviour
{
    public Canvas canvas;

    private void Start()
    {
        // ȭ�� ũ�⿡ �°� Canvas ũ�� ����
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        float canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        float canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        float canvasScale = Mathf.Min(screenWidth / canvasWidth, screenHeight / canvasHeight);
        canvas.scaleFactor = canvasScale;

        // UI ��ҵ��� ũ�� ����
        foreach (RectTransform rectTransform in canvas.GetComponentsInChildren<RectTransform>())
        {
            // Canvas �ڽ� ��ü�� �����ϵ��� ���͸�
            if (rectTransform.gameObject.transform.parent != canvas.transform)
                continue;

            rectTransform.localScale *= canvasScale;
        }
    }
}
