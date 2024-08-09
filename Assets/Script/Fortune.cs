using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortune : MonoBehaviour
{
    public float fadeDuration = 1.5f; // ���̵� �ƿ��� �ɸ��� �ð�
    private SpriteRenderer spriteRenderer; // ������Ʈ�� ��������Ʈ ������

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.tag == "tarrot_card")
        {
            // Ŭ���� ��ġ������ Raycast
            Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);


            // �ݶ��̴��� ���� ������Ʈ�� Ŭ���� ��Ҵ��� Ȯ��
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                OnMouseDown();
            }
        }
    }
    void OnMouseDown()
    {
        // ������Ʈ�� Ŭ���Ǿ��� �� ���̵� �ƿ� ����
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        // �ʱ� ���� (���� 1)
        Color initialColor = spriteRenderer.color;
        Color transparentColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);

        // ���̵� �ƿ�
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            spriteRenderer.color = Color.Lerp(initialColor, transparentColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���������� ������ �����ϰ� ����
        spriteRenderer.color = transparentColor;
    }
    
}
