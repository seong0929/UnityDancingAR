using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ARController : MonoBehaviour
{
    public GameObject characterPrefab; // ĳ���� ������
    public ARRaycastManager arRaycastManager; // AR ����ĳ��Ʈ �Ŵ���
    public GameObject alarm;    // �˶� ����
    public int maxCharacterCount = 5; // �ִ� ĳ���� ��

    private List<ARRaycastHit> hits = new List<ARRaycastHit>(); // ����ĳ��Ʈ ��Ʈ ����� ������ ����Ʈ
    private List<GameObject> characters = new List<GameObject>(); // ������ ĳ���͸� ������ ����Ʈ
    private bool isTouchInProgress = false; // ��ġ ���� �� ����

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // ��ġ�� ���ų� �ִ� ĳ���� ���� �ʰ��ϸ� �������� ����
        if (Input.touchCount == 0 || characters.Count >= maxCharacterCount) return;

        if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
        {
            // �ߺ� ��ġ �Է� ����
            isTouchInProgress = true;

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

                // ĳ���� ���� ��, �����ϰ� ĳ������ ũ�� ����
                float scale = Random.Range(0.05f, 0.1f);
                characterObject.transform.localScale = Vector3.one * scale;

                alarm.GetComponent<Text>().text = "";

                // ������ ĳ���Ϳ� AudioSource ������Ʈ�� �߰��ϰ� �뷡�� ���
                characterObject.GetComponent<AudioSource>().volume = GameManager.instance.volume;
                //characterObject.GetComponent<AudioSource>().Play();

                // ĳ���͸� ����Ʈ�� �߰�
                characters.Add(characterObject);

                // ĳ���� ���� �ִ� ĳ���� ���� �ʰ��ϴ� ��� ���� ������ ĳ���͸� ����
                if (characters.Count > maxCharacterCount)
                {
                    GameObject oldestCharacter = characters[0];
                    characters.RemoveAt(0);
                    Destroy(oldestCharacter);
                }
            }
            else
            {
                alarm.GetComponent<Text>().text = "Where is Klee?";
            }
        }
    }
    void OnDisable()
    {
        // ������ ��� ĳ���͸� ����
        foreach (GameObject character in characters)
        {
            Destroy(character);
        }

        // ����Ʈ �ʱ�ȭ
        characters.Clear();
    }
    // ĳ���� ���� �� �����ϴ� �κ�
    void OnDestroy()
    {
        isTouchInProgress = false; // ��ġ ���� �ʱ�ȭ
    }
}