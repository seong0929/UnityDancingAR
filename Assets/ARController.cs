using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ARController : MonoBehaviour
{
    public GameObject characterPrefab; // ĳ���� ������
    public ARRaycastManager arRaycastManager; // AR ����ĳ��Ʈ �Ŵ���
    public GameObject alarm;
    public int maxCharacterCount = 5; // �ִ� ĳ���� ��
    private List<ARRaycastHit> hits = new List<ARRaycastHit>(); // ����ĳ��Ʈ ��Ʈ ����� ������ ����Ʈ
    private int currentCharacterCount = 0; // ���� ĳ���� ��

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // ��ġ�� ���ų� �ִ� ĳ���� ���� �ʰ��ϸ� �������� ����
        if (Input.touchCount == 0 || currentCharacterCount >= maxCharacterCount) return;

        if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
        {
            // ��ġ �� ������ ����
            Pose hitPose = hits[0].pose;

            // ���� �߽����� ������
            Vector3 center = new Vector3(0f, 0f, 0f); // �߽��� ��ǥ
            float radius = 0.5f; // ������

            if (Vector3.Distance(center, hitPose.position) < radius)
            {
                // ĳ���� ����
                GameObject characterObject = Instantiate(characterPrefab, hitPose.position, hitPose.rotation);

                // ������ �������� ĳ���� ȸ��
                float randomRotationY = Random.Range(0f, 360f);
                Quaternion randomRotation = Quaternion.Euler(0f, randomRotationY, 0f);
                characterObject.transform.rotation = randomRotation;

                // ĳ���� ���� ��, ���� ũ�⿡ ���� ĳ������ ũ�� ����
                float scale = Random.Range(0.05f, 0.1f);
                characterObject.transform.localScale = Vector3.one * scale;

                alarm.GetComponent<Text>().text = "";

                // ������ ĳ���Ϳ� AudioSource ������Ʈ�� �߰��ϰ� �뷡�� ���
                GameManager.instance.PlaySoundOnInstantiate(characterObject);

                // ĳ���� �� ����
                currentCharacterCount++;
            }
            else
            {
                alarm.GetComponent<Text>().text = "Where is klee?";
            }
        }
    }

    void OnDisable()
    {
        hits.Clear();
        currentCharacterCount = 0; // ĳ���� �� �ʱ�ȭ
    }
}