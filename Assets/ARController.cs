using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
public class ARController : MonoBehaviour
{
    public GameObject characterPrefab; // ĳ���� ������
    public AudioClip musicClip; // ĳ���� ���� �� ����� �뷡 Ŭ��
    public GameObject messageText; //�˸� Text ��ü
    public ARRaycastManager arRaycastManager; // AR ����ĳ��Ʈ �Ŵ���

    private List<ARRaycastHit> hitResults = new List<ARRaycastHit>(); // ����ĳ��Ʈ ��Ʈ ����� ������ ����Ʈ
    private float minScale = 0.5f; // ĳ������ �ּ� ũ��
    private float maxScale = 1.5f; // ĳ������ �ִ� ũ��
    private bool isPlacingObject = false; // ĳ���� ���� ������ ����
    void Update()
    {
        if (!isPlacingObject) return; // ĳ���� ���� ���� �ƴϸ� ����

        if (arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hitResults, TrackableType.PlaneWithinPolygon))
        {
            //ȭ�� �߾� ��� �ν�
            Pose hitPose = hitResults[0].pose;
            ARPlane plane = hitResults[0].trackable as ARPlane;
            if (plane != null)
            {
                GenerateCharacter(hitPose, plane);
            }
            else
            {
                // ��� �ν� ���� �� �޽��� ���
                messageText.GetComponent<Text>().text = "Where are you?";
            }
        }
    }
    public GameObject GenerateCharacter(Pose hitPose, ARPlane plane)
    {
        //ĳ���� ����
        GameObject characterObject = Instantiate(characterPrefab, hitPose.position, hitPose.rotation);

        // ������ �������� ĳ���� ȸ��
        float randomRotationY = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(0f, randomRotationY, 0f);
        characterObject.transform.rotation = randomRotation;

        // ĳ���� ���� ��, ���� ũ�⿡ ���� ĳ������ ũ�� ����
        float scale = Mathf.Clamp(plane.size.x, minScale, maxScale);
        characterObject.transform.localScale = Vector3.one * scale;

        // ������ ĳ���Ϳ� AudioSource ������Ʈ�� �߰��ϰ� �뷡�� ���
        AudioSource audioSource = characterObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.Play();

        isPlacingObject = true; // ĳ���� ���� �Ϸ�
        messageText.GetComponent<Text>().text = "";

        return characterObject;
    }
}