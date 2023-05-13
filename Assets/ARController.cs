using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
public class ARController : MonoBehaviour
{
    public GameObject characterPrefab; // 캐릭터 프리팹
    public AudioClip musicClip; // 캐릭터 생성 시 재생할 노래 클립
    public GameObject messageText; //알림 Text 객체
    public ARRaycastManager arRaycastManager; // AR 레이캐스트 매니저

    private List<ARRaycastHit> hitResults = new List<ARRaycastHit>(); // 레이캐스트 히트 결과를 저장할 리스트
    private float minScale = 0.5f; // 캐릭터의 최소 크기
    private float maxScale = 1.5f; // 캐릭터의 최대 크기
    private bool isPlacingObject = false; // 캐릭터 생성 중인지 여부
    void Update()
    {
        if (!isPlacingObject) return; // 캐릭터 생성 중이 아니면 종료

        if (arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hitResults, TrackableType.PlaneWithinPolygon))
        {
            //화면 중앙 평면 인식
            Pose hitPose = hitResults[0].pose;
            ARPlane plane = hitResults[0].trackable as ARPlane;
            if (plane != null)
            {
                GenerateCharacter(hitPose, plane);
            }
            else
            {
                // 평면 인식 실패 시 메시지 출력
                messageText.GetComponent<Text>().text = "Where are you?";
            }
        }
    }
    public GameObject GenerateCharacter(Pose hitPose, ARPlane plane)
    {
        //캐릭터 생성
        GameObject characterObject = Instantiate(characterPrefab, hitPose.position, hitPose.rotation);

        // 랜덤한 방향으로 캐릭터 회전
        float randomRotationY = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(0f, randomRotationY, 0f);
        characterObject.transform.rotation = randomRotation;

        // 캐릭터 생성 후, 원의 크기에 따라 캐릭터의 크기 조절
        float scale = Mathf.Clamp(plane.size.x, minScale, maxScale);
        characterObject.transform.localScale = Vector3.one * scale;

        // 생성된 캐릭터에 AudioSource 컴포넌트를 추가하고 노래를 재생
        AudioSource audioSource = characterObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.Play();

        isPlacingObject = true; // 캐릭터 생성 완료
        messageText.GetComponent<Text>().text = "";

        return characterObject;
    }
}