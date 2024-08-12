using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortune : MonoBehaviour
{
    public float fadeDuration = 1.5f; // 페이드 아웃에 걸리는 시간
    private SpriteRenderer spriteRenderer; // 오브젝트의 스프라이트 렌더러
    public program_manager pmanager; // 페이지 관리자를 참조

    // 랜덤하게 선택될 스프라이트 배열
    public Sprite[] randomSprites;
    public Sprite[] randomSprites_result_couple;
    public Sprite[] randomSprites_result_solo;
    public Sprite[] randomSprites_result_daily;
    // 전역 변수로 변경
    private static bool isProcessing = false;

    // 이전 페이지에서 선택된 스프라이트를 저장하는 변수
    private Sprite selectedSprite;
    private int selectedIndex; // 선택된 스프라이트의 인덱스
    Sprite resultSprite;
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

        // 랜덤 스프라이트로 변경 및 저장
        if (randomSprites.Length > 0)
        {
            selectedIndex = Random.Range(0, randomSprites.Length);
            selectedSprite = randomSprites[selectedIndex];
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
            if (pmanager.pageIndex < pmanager.Pages.Length) // 페이지 인덱스가 범위를 벗어나지 않도록 확인
            {
                pmanager.Pages[pmanager.pageIndex].SetActive(true);

                // 새 페이지의 이름이 "tarrot_back"인 오브젝트 찾기
                GameObject tarrotBack = GameObject.Find("back_will_change");
                if (tarrotBack != null)
                {
                    // tarrot_back의 SpriteRenderer를 가져와서 이전에 선택된 스프라이트로 변경
                    SpriteRenderer tarrotBackRenderer = tarrotBack.GetComponent<SpriteRenderer>();
                    if (tarrotBackRenderer != null)
                    {
                        tarrotBackRenderer.sprite = selectedSprite;
                    }
                }

                // 새 페이지의 이름이 "card_result"인 오브젝트 찾기
                GameObject cardResult = GameObject.Find("card_result");
                if (cardResult != null )
                {
                    // card_result의 SpriteRenderer를 가져옴
                    SpriteRenderer cardResultRenderer = cardResult.GetComponent<SpriteRenderer>();
                    if (cardResultRenderer != null)
                    {
                        
                        // randomSprites_result 배열에서 동일 인덱스의 스프라이트 선택
                        if (pmanager.what_fortune == 0)
                        {
                            resultSprite = randomSprites_result_couple[selectedIndex];
                        }
                        else if (pmanager.what_fortune == 1)
                        {
                            resultSprite = randomSprites_result_solo[selectedIndex];
                        }
                        else if (pmanager.what_fortune == 2)
                        {
                            resultSprite = randomSprites_result_daily[selectedIndex];
                        }
                         

                        // 현재 스프라이트의 크기와 새 스프라이트의 크기를 비교하여 로컬 스케일 조정
                        AdjustSpriteScale(cardResultRenderer, resultSprite);

                        // 새 스프라이트 설정
                        cardResultRenderer.sprite = resultSprite;
                    }
                }
            }
            else
            {
                // 페이지 인덱스가 범위를 넘어가면 처리 (예: 마지막 페이지일 경우)
                pmanager.pageIndex = 0; // 예를 들어, 처음 페이지로 돌아가게 할 수도 있음
                pmanager.Pages[pmanager.pageIndex].SetActive(true);

                // 새 페이지의 이름이 "tarrot_back"인 오브젝트 찾기
                GameObject tarrotBack = GameObject.Find("back_will_change");
                if (tarrotBack != null)
                {
                    // tarrot_back의 SpriteRenderer를 가져와서 이전에 선택된 스프라이트로 변경
                    SpriteRenderer tarrotBackRenderer = tarrotBack.GetComponent<SpriteRenderer>();
                    if (tarrotBackRenderer != null)
                    {
                        tarrotBackRenderer.sprite = selectedSprite;
                    }
                }

                // 새 페이지의 이름이 "card_result"인 오브젝트 찾기
                GameObject cardResult = GameObject.Find("card_result");

                if (cardResult != null)
                {
                    // card_result의 SpriteRenderer를 가져옴
                    SpriteRenderer cardResultRenderer = cardResult.GetComponent<SpriteRenderer>();
                    if (cardResultRenderer != null)
                    {
                        // randomSprites_result 배열에서 동일 인덱스의 스프라이트 선택
                        if (pmanager.what_fortune == 0)
                        {
                            resultSprite = randomSprites_result_couple[selectedIndex];
                        }
                        else if (pmanager.what_fortune == 1)
                        {
                            resultSprite = randomSprites_result_solo[selectedIndex];
                        }
                        else if (pmanager.what_fortune == 2)
                        {
                            resultSprite = randomSprites_result_daily[selectedIndex];
                        }

                        // 현재 스프라이트의 크기와 새 스프라이트의 크기를 비교하여 로컬 스케일 조정
                        AdjustSpriteScale(cardResultRenderer, resultSprite);

                        // 새 스프라이트 설정
                        cardResultRenderer.sprite = resultSprite;
                    }
                }
            }
        }
    }

    void AdjustSpriteScale(SpriteRenderer spriteRenderer, Sprite newSprite)
    {
        // 현재 스프라이트와 새 스프라이트의 크기 정보 가져오기
        Vector2 currentSize = spriteRenderer.sprite.bounds.size;
        Vector2 newSize = newSprite.bounds.size;

        // 현재 스프라이트와 새 스프라이트의 크기 비율 계산
        Vector2 scale = spriteRenderer.transform.localScale;
        scale.x *= currentSize.x / newSize.x;
        scale.y *= currentSize.y / newSize.y;

        // 오브젝트의 로컬 스케일을 새 스프라이트에 맞게 조정
        spriteRenderer.transform.localScale = scale;
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
