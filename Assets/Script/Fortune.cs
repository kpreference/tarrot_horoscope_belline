using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortune : MonoBehaviour
{
    public float fadeDuration = 1.5f; // 페이드 아웃에 걸리는 시간
    private SpriteRenderer spriteRenderer; // 오브젝트의 스프라이트 렌더러

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.tag == "tarrot_card")
        {
            // 클릭한 위치에서의 Raycast
            Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);


            // 콜라이더를 가진 오브젝트에 클릭이 닿았는지 확인
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                OnMouseDown();
            }
        }
    }
    void OnMouseDown()
    {
        // 오브젝트가 클릭되었을 때 페이드 아웃 시작
        StartCoroutine(FadeOut());
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
    
}
