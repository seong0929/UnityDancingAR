using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARController : MonoBehaviour
{
    public GameObject characterPrefab; // 캐릭터 프리팹
    public AudioClip musicClip; // 캐릭터 생성 시 재생할 노래 클립
    public GameObject visualFeedbackPrefab; // 시각적인 피드백 프리팹
    public float spawnHeight = 1f;  //스폰 보정값

    private ARRaycastManager arRaycastManager; // AR 레이캐스트 매니저
    private List<ARRaycastHit> hitResults = new List<ARRaycastHit>(); // 레이캐스트 히트 결과를 저장할 리스트
    private GameObject feedbackInstance; // 생성된 시각적인 피드백 인스턴스
    private float minScale = 0.5f; // 캐릭터의 최소 크기
    private float maxScale = 1.5f; // 캐릭터의 최대 크기

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>(); // AR 레이캐스트 매니저 컴포넌트를 가져옴
    }

    void Update()
    {
        if (Input.touchCount == 0) return; // 터치 입력이 없으면 종료

        Touch touch = Input.GetTouch(0); // 첫 번째 터치 입력을 가져옴
        if (arRaycastManager.Raycast(touch.position, hitResults, TrackableType.PlaneWithinPolygon))
        {
            // 터치 입력이 인식된 경우에만 처리
            Pose hitPose = hitResults[0].pose;
            ARPlane plane = hitResults[0].trackable as ARPlane;
            if (plane != null)
            {
                // 시각적인 피드백을 생성하여 평면 인식을 표시
                if (feedbackInstance == null)
                {
                    feedbackInstance = Instantiate(visualFeedbackPrefab, hitPose.position, Quaternion.identity);
                }
                else
                {
                    feedbackInstance.transform.position = hitPose.position;
                }
                GameObject characterObject = Instantiate(characterPrefab, hitPose.position, hitPose.rotation);

                // 랜덤한 방향으로 캐릭터 회전
                float randomRotationY = Random.Range(0f, 360f);
                Quaternion randomRotation = Quaternion.Euler(0f, randomRotationY, 0f);
                characterObject.transform.rotation = randomRotation;

                // 캐릭터 생성 후, 원의 크기에 따라 캐릭터의 크기 조절
                float scale = Mathf.Clamp(plane.size.x, minScale, maxScale);
                characterObject.transform.localScale = Vector3.one * scale;
                characterObject.transform.localScale = new Vector3(scale, scale, scale);

                //위치 조정
                characterObject.transform.position = hitPose.position + new Vector3(0f, spawnHeight, 0f);

                // 생성된 캐릭터에 AudioSource 컴포넌트를 추가하고 노래를 재생
                AudioSource audioSource = characterObject.AddComponent<AudioSource>();
                audioSource.clip = musicClip;
                audioSource.Play();
            }
        }
    }

}
