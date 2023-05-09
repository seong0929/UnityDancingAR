using UnityEngine;
using UnityEngine.UI;

public class UIScreenManager : MonoBehaviour
{
    public Canvas canvas;

    private void Start()
    {
        // 화면 크기에 맞게 Canvas 크기 조절
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        float canvasHeight = canvas.GetComponent<RectTransform>().rect.height;
        float canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        float canvasScale = Mathf.Min(screenWidth / canvasWidth, screenHeight / canvasHeight);
        canvas.scaleFactor = canvasScale;

        // UI 요소들의 크기 조절
        foreach (RectTransform rectTransform in canvas.GetComponentsInChildren<RectTransform>())
        {
            // Canvas 자식 객체만 조절하도록 필터링
            if (rectTransform.gameObject.transform.parent != canvas.transform)
                continue;

            rectTransform.localScale *= canvasScale;
        }
    }
}
