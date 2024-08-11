using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro를 사용하기 위해 추가

public class Fortune : MonoBehaviour
{
    public float fadeDuration = 1.5f; // 페이드 아웃에 걸리는 시간
    private SpriteRenderer spriteRenderer; // 오브젝트의 스프라이트 렌더러
    public program_manager pmanager; // 페이지 관리자를 참조

    // 랜덤하게 선택될 스프라이트 배열 및 문구 배열
    public Sprite[] randomSprites;
    public string[] randomMessages;

    // 전역 변수로 변경
    private static bool isProcessing = false;
    private Sprite selectedSprite;
    private string selectedMessage;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 카드가 클릭된 상태가 아닐 때만 반응하도록
        if (!isProcessing && Input.GetMouseButtonDown(0) && gameObject.tag == "tarrot_card")
        {
            // 클릭한 위치에서의 Raycast
            Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);

            // 콜라이더를 가진 오브젝트에 클릭이 닿았는지 확인
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // 카드 클릭 처리
                StartCoroutine(ProcessCardClick());
            }
        }
    }

    IEnumerator ProcessCardClick()
    {
        // 카드가 클릭 중이라는 상태로 설정
        isProcessing = true;

        // 페이드 아웃 시작
        yield return StartCoroutine(FadeOut());

        // 랜덤 스프라이트와 문구로 변경 및 저장
        if (randomSprites.Length > 0)
        {
            int randomIndex = Random.Range(0, randomSprites.Length);
            selectedSprite = randomSprites[randomIndex];
            selectedMessage = randomMessages[randomIndex]; // 선택된 문구를 저장
            spriteRenderer.sprite = selectedSprite;
        }

        // 페이드 인 시작
        yield return StartCoroutine(FadeIn());

        // 카드 클릭 처리가 끝났다는 상태로 설정
        isProcessing = false;

        // 페이지 이동 처리
        if (pmanager != null)
        {
            // 현재 페이지를 비활성화
            pmanager.Pages[pmanager.pageIndex].SetActive(false);

            // 페이지 인덱스를 증가시키고 새로운 페이지를 활성화
            pmanager.pageIndex++;
            if (pmanager.pageIndex < pmanager.Pages.Length)
            {
                pmanager.Pages[pmanager.pageIndex].SetActive(true);

                // 새 페이지의 이름이 "back_will_change"인 오브젝트 찾기
                GameObject tarrotBack = GameObject.Find("back_will_change");
                if (tarrotBack != null)
                {
                    // tarrot_back의 SpriteRenderer를 가져와서 이전에 선택된 스프라이트로 변경
                    SpriteRenderer tarrotBackRenderer = tarrotBack.GetComponent<SpriteRenderer>();
                    if (tarrotBackRenderer != null)
                    {
                        tarrotBackRenderer.sprite = selectedSprite;

                        // 문구를 출력할 새로운 텍스트 오브젝트 생성
                        GameObject messageObject = new GameObject("Message");
                        messageObject.transform.SetParent(tarrotBack.transform);

                        // 텍스트 위치 설정 (오브젝트 아래)
                        messageObject.transform.localPosition = new Vector3(0, -tarrotBackRenderer.bounds.size.y / 2 - 0.5f, 0);

                        // TextMeshPro 컴포넌트 추가 및 설정
                        TextMeshPro textMeshPro = messageObject.AddComponent<TextMeshPro>();
                        textMeshPro.text = selectedMessage;
                        textMeshPro.fontSize = 24;
                        textMeshPro.alignment = TextAlignmentOptions.Left; // 왼쪽 정렬

                        // 텍스트 색상 설정
                        textMeshPro.color = Color.black;

                        // 텍스트 크기를 조정하는 함수 호출
                        AdjustTextSizeToCamera(textMeshPro);
                    }
                }
            }
            else
            {
                // 페이지 인덱스가 범위를 넘어가면 처리 (예: 마지막 페이지일 경우)
                pmanager.pageIndex = 0;
                pmanager.Pages[pmanager.pageIndex].SetActive(true);

                // 새 페이지의 이름이 "back_will_change"인 오브젝트 찾기
                GameObject tarrotBack = GameObject.Find("back_will_change");
                if (tarrotBack != null)
                {
                    // tarrot_back의 SpriteRenderer를 가져와서 이전에 선택된 스프라이트로 변경
                    SpriteRenderer tarrotBackRenderer = tarrotBack.GetComponent<SpriteRenderer>();
                    if (tarrotBackRenderer != null)
                    {
                        tarrotBackRenderer.sprite = selectedSprite;

                        // 문구를 출력할 새로운 텍스트 오브젝트 생성
                        GameObject messageObject = new GameObject("Message");
                        messageObject.transform.SetParent(tarrotBack.transform);

                        // 텍스트 위치 설정 (오브젝트 아래)
                        messageObject.transform.localPosition = new Vector3(0, -tarrotBackRenderer.bounds.size.y / 2 - 0.5f, 0);

                        // TextMeshPro 컴포넌트 추가 및 설정
                        TextMeshPro textMeshPro = messageObject.AddComponent<TextMeshPro>();
                        textMeshPro.text = selectedMessage;
                        textMeshPro.fontSize = 24;
                        textMeshPro.alignment = TextAlignmentOptions.Left; // 왼쪽 정렬

                        // 텍스트 색상 설정
                        textMeshPro.color = Color.black;

                        // 텍스트 크기를 조정하는 함수 호출
                        AdjustTextSizeToCamera(textMeshPro);
                    }
                }
            }
        }
    }

    void AdjustTextSizeToCamera(TextMeshPro textMeshPro)
    {
        Camera mainCamera = Camera.main;
        float screenWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;

        // 텍스트의 폭이 카메라 화면을 넘어가면 글자 크기를 줄임
        while (textMeshPro.preferredWidth > screenWidth)
        {
            textMeshPro.fontSize -= 1; // fontSize를 줄여서 글자 크기를 줄임
            if (textMeshPro.fontSize <= 12) // 최소 크기를 설정해 무한 루프 방지
            {
                textMeshPro.fontSize = 12;
                break;
            }
        }

        // 줄바꿈 설정 (필요 시 자동 줄바꿈)
        textMeshPro.enableWordWrapping = true;
        textMeshPro.overflowMode = TextOverflowModes.Overflow;
    }

    IEnumerator FadeOut()
    {
        // 초기 색상 (투명도 1)
        Color initialColor = spriteRenderer.color;
        Color transparentColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);

        // 페이드 아웃
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            spriteRenderer.color = Color.Lerp(initialColor, transparentColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종적으로 완전히 투명하게 설정
        spriteRenderer.color = transparentColor;
    }

    IEnumerator FadeIn()
    {
        // 투명 상태에서 시작
        Color initialColor = spriteRenderer.color;
        Color visibleColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            spriteRenderer.color = Color.Lerp(initialColor, visibleColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종적으로 완전히 보이게 설정
        spriteRenderer.color = visibleColor;
    }
}
