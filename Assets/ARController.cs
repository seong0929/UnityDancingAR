using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARController : MonoBehaviour
{
    public GameObject characterPrefab; // ĳ���� ������
    public AudioClip musicClip; // ĳ���� ���� �� ����� �뷡 Ŭ��
    public float spawnHeight = 1f;
    private ARRaycastManager arRaycastManager; // AR ����ĳ��Ʈ �Ŵ���
    private List<ARRaycastHit> hitResults = new List<ARRaycastHit>(); // ����ĳ��Ʈ ��Ʈ ����� ������ ����Ʈ
    private float minScale = 0.5f; // ĳ������ �ּ� ũ��
    private float maxScale = 1.5f; // ĳ������ �ִ� ũ��

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>(); // AR ����ĳ��Ʈ �Ŵ��� ������Ʈ�� ������
    }

    void Update()
    {
        if (Input.touchCount == 0) return; // ��ġ �Է��� ������ ����

        Touch touch = Input.GetTouch(0); // ù ��° ��ġ �Է��� ������
        if (touch.phase != TouchPhase.Began) return; // ��ġ�� ���۵��� �ʾ����� ����

        if (arRaycastManager.Raycast(touch.position, hitResults, TrackableType.PlaneWithinPolygon))
        {
            // ����ĳ��Ʈ�� ���� ���� �ν��ϰ�, ĳ���͸� ������ ��ġ�� ����
            Pose hitPose = hitResults[0].pose;
            ARPlane plane = hitResults[0].trackable as ARPlane;
            if (plane != null)
            {
                GameObject characterObject = Instantiate(characterPrefab, hitPose.position, hitPose.rotation);

                // ������ �������� ĳ���� ȸ��
                float randomRotationY = Random.Range(0f, 360f);
                Quaternion randomRotation = Quaternion.Euler(0f, randomRotationY, 0f);
                characterObject.transform.rotation = randomRotation;

                // ĳ���� ���� ��, ���� ũ�⿡ ���� ĳ������ ũ�� ����
                float scale = Mathf.Clamp(plane.size.x, minScale, maxScale);
                characterObject.transform.localScale = Vector3.one * scale;
                characterObject.transform.localScale = new Vector3(scale, scale, scale);

                //��ġ ����
                characterObject.transform.position = hitPose.position + new Vector3(0f, spawnHeight, 0f);


                // ������ ĳ���Ϳ� AudioSource ������Ʈ�� �߰��ϰ� �뷡�� ���
                AudioSource audioSource = characterObject.AddComponent<AudioSource>();
                audioSource.clip = musicClip;
                audioSource.Play();
            }
        }
    }
}
