using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
public class ARController : MonoBehaviour
{
    public GameObject characterPrefab; // 캐릭터 프리팹
    public ARRaycastManager arRaycastManager; // AR 레이캐스트 매니저
    public GameObject alarm;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>(); // 레이캐스트 히트 결과를 저장할 리스트
    private bool isCharacterSpawned = false; // 캐릭터 생성 여부를 추적하는 변수

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // 터치가 없으면 실행 X
        if (Input.touchCount == 0) return;

        // 이미 캐릭터가 생성되어 있는 경우, 추가 생성 방지
        if (isCharacterSpawned) return;

        if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
        {
            // 터치 한 곳에서 생성
            Pose hitPose = hits[0].pose;

            // 원의 중심점과 반지름
            Vector3 center = new Vector3(0f, 0f, 0f); // 중심점 좌표
            float radius = 0.5f; // 반지름

            if (Vector3.Distance(center, hitPose.position) < radius)
            {
                // 캐릭터 생성
                GameObject characterObject = Instantiate(characterPrefab, hitPose.position, hitPose.rotation);

                // 랜덤한 방향으로 캐릭터 회전
                float randomRotationY = Random.Range(0f, 360f);
                Quaternion randomRotation = Quaternion.Euler(0f, randomRotationY, 0f);
                characterObject.transform.rotation = randomRotation;

                // 캐릭터 생성 후, 원의 크기에 따라 캐릭터의 크기 조절
                float scale = Random.Range(0.05f, 0.1f);
                characterObject.transform.localScale = Vector3.one * scale;

                alarm.GetComponent<Text>().text = "";

                // 생성된 캐릭터에 AudioSource 컴포넌트를 추가하고 노래를 재생
                GameManager.instance.PlaySoundOnInstantiate(characterObject);

                // 캐릭터가 생성되었음을 표시
                isCharacterSpawned = true;
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
        isCharacterSpawned = false; // 캐릭터 생성 여부 초기화
    }
}
