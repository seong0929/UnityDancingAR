# UnityDancingAR
tiktok앱에 원신 캐릭터 '클레'라는 캐릭터가 AR로 춤을 추고 노래를 부르는 klee filter가 존재한다.

이 프로젝트는 유니티 AR을 이용하여 춤추는 캐릭터 보여주는 프로젝트이다.

아래는 해당 프로젝트 실행 영상과 실행 파일(안드로이드)이다. 평면을 우선 인식하고, 평면에 원이 존재하는 곳에 클릭을 해야 캐릭터가 등장하여 춤을 춘다.
- [실행 영상](https://github.com/seong0929/UnityDancingAR/assets/50050003/124521cd-7a08-4e2e-b55a-6733fb92f425)
- [실행 파일](https://github.com/seong0929/UnityDancingAR/blob/main/AndroidDancingAR.apk)

해당 프로젝트의 핵심 코드이다.
<pre>
<code>
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ARController : MonoBehaviour
{
    public GameObject characterPrefab; // 캐릭터 프리팹
    public ARRaycastManager arRaycastManager; // AR 레이캐스트 매니저
    public GameObject alarm;    // 알람 문구
    public int maxCharacterCount = 5; // 최대 캐릭터 수

    private List<ARRaycastHit> hits = new List<ARRaycastHit>(); // 레이캐스트 히트 결과를 저장할 리스트
    private List<GameObject> characters = new List<GameObject>(); // 생성된 캐릭터를 저장할 리스트
    private bool isTouchInProgress = false; // 터치 진행 중 여부

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // 터치가 없거나 최대 캐릭터 수를 초과하면 실행하지 않음
        if (Input.touchCount == 0 || characters.Count >= maxCharacterCount) return;

        if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
        {
            // 중복 터치 입력 방지
            isTouchInProgress = true;

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

                // 캐릭터 생성 후, 랜덤하게 캐릭터의 크기 조절
                float scale = Random.Range(0.05f, 0.1f);
                characterObject.transform.localScale = Vector3.one * scale;

                alarm.GetComponent<Text>().text = "";

                // 생성된 캐릭터에 AudioSource 컴포넌트를 추가하고 노래를 재생
                characterObject.GetComponent<AudioSource>().volume = GameManager.instance.volume;
                //characterObject.GetComponent<AudioSource>().Play();

                // 캐릭터를 리스트에 추가
                characters.Add(characterObject);

                // 캐릭터 수가 최대 캐릭터 수를 초과하는 경우 가장 오래된 캐릭터를 삭제
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
        // 생성된 모든 캐릭터를 삭제
        foreach (GameObject character in characters)
        {
            Destroy(character);
        }

        // 리스트 초기화
        characters.Clear();
    }
    // 캐릭터 생성 후 삭제하는 부분
    void OnDestroy()
    {
        isTouchInProgress = false; // 터치 상태 초기화
    }
}
</code>
</pre>
