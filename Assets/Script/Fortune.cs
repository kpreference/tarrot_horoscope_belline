using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortune : MonoBehaviour
{
    public float fadeDuration = 1.5f; // ���̵� �ƿ��� �ɸ��� �ð�
    private SpriteRenderer spriteRenderer; // ������Ʈ�� ��������Ʈ ������
    public program_manager pmanager; // ������ �����ڸ� ����

    // �����ϰ� ���õ� ��������Ʈ �迭
    public Sprite[] randomSprites;
    public Sprite[] randomSprites_result_couple;
    public Sprite[] randomSprites_result_solo;
    public Sprite[] randomSprites_result_daily;
    // ���� ������ ����
    private static bool isProcessing = false;

    // ���� ���������� ���õ� ��������Ʈ�� �����ϴ� ����
    private Sprite selectedSprite;
    private int selectedIndex; // ���õ� ��������Ʈ�� �ε���
    Sprite resultSprite;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ī�尡 Ŭ���� ���°� �ƴ� ���� �����ϵ���
        if (!isProcessing && Input.GetMouseButtonDown(0) && gameObject.tag == "tarrot_card")
        {
            // Ŭ���� ��ġ������ Raycast
            Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);

            // �ݶ��̴��� ���� ������Ʈ�� Ŭ���� ��Ҵ��� Ȯ��
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // ī�� Ŭ�� ó��
                StartCoroutine(ProcessCardClick());
            }
        }
    }

    IEnumerator ProcessCardClick()
    {
        // ī�尡 Ŭ�� ���̶�� ���·� ����
        isProcessing = true;

        // ���̵� �ƿ� ����
        yield return StartCoroutine(FadeOut());

        // ���� ��������Ʈ�� ���� �� ����
        if (randomSprites.Length > 0)
        {
            selectedIndex = Random.Range(0, randomSprites.Length);
            selectedSprite = randomSprites[selectedIndex];
            spriteRenderer.sprite = selectedSprite;
        }

        // ���̵� �� ����
        yield return StartCoroutine(FadeIn());

        // ī�� Ŭ�� ó���� �����ٴ� ���·� ����
        isProcessing = false;

        // ������ �̵� ó��
        if (pmanager != null)
        {
            // ���� �������� ��Ȱ��ȭ
            pmanager.Pages[pmanager.pageIndex].SetActive(false);

            // ������ �ε����� ������Ű�� ���ο� �������� Ȱ��ȭ
            pmanager.pageIndex++;
            if (pmanager.pageIndex < pmanager.Pages.Length) // ������ �ε����� ������ ����� �ʵ��� Ȯ��
            {
                pmanager.Pages[pmanager.pageIndex].SetActive(true);

                // �� �������� �̸��� "tarrot_back"�� ������Ʈ ã��
                GameObject tarrotBack = GameObject.Find("back_will_change");
                if (tarrotBack != null)
                {
                    // tarrot_back�� SpriteRenderer�� �����ͼ� ������ ���õ� ��������Ʈ�� ����
                    SpriteRenderer tarrotBackRenderer = tarrotBack.GetComponent<SpriteRenderer>();
                    if (tarrotBackRenderer != null)
                    {
                        tarrotBackRenderer.sprite = selectedSprite;
                    }
                }

                // �� �������� �̸��� "card_result"�� ������Ʈ ã��
                GameObject cardResult = GameObject.Find("card_result");
                if (cardResult != null )
                {
                    // card_result�� SpriteRenderer�� ������
                    SpriteRenderer cardResultRenderer = cardResult.GetComponent<SpriteRenderer>();
                    if (cardResultRenderer != null)
                    {
                        
                        // randomSprites_result �迭���� ���� �ε����� ��������Ʈ ����
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
                         

                        // ���� ��������Ʈ�� ũ��� �� ��������Ʈ�� ũ�⸦ ���Ͽ� ���� ������ ����
                        AdjustSpriteScale(cardResultRenderer, resultSprite);

                        // �� ��������Ʈ ����
                        cardResultRenderer.sprite = resultSprite;
                    }
                }
            }
            else
            {
                // ������ �ε����� ������ �Ѿ�� ó�� (��: ������ �������� ���)
                pmanager.pageIndex = 0; // ���� ���, ó�� �������� ���ư��� �� ���� ����
                pmanager.Pages[pmanager.pageIndex].SetActive(true);

                // �� �������� �̸��� "tarrot_back"�� ������Ʈ ã��
                GameObject tarrotBack = GameObject.Find("back_will_change");
                if (tarrotBack != null)
                {
                    // tarrot_back�� SpriteRenderer�� �����ͼ� ������ ���õ� ��������Ʈ�� ����
                    SpriteRenderer tarrotBackRenderer = tarrotBack.GetComponent<SpriteRenderer>();
                    if (tarrotBackRenderer != null)
                    {
                        tarrotBackRenderer.sprite = selectedSprite;
                    }
                }

                // �� �������� �̸��� "card_result"�� ������Ʈ ã��
                GameObject cardResult = GameObject.Find("card_result");

                if (cardResult != null)
                {
                    // card_result�� SpriteRenderer�� ������
                    SpriteRenderer cardResultRenderer = cardResult.GetComponent<SpriteRenderer>();
                    if (cardResultRenderer != null)
                    {
                        // randomSprites_result �迭���� ���� �ε����� ��������Ʈ ����
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

                        // ���� ��������Ʈ�� ũ��� �� ��������Ʈ�� ũ�⸦ ���Ͽ� ���� ������ ����
                        AdjustSpriteScale(cardResultRenderer, resultSprite);

                        // �� ��������Ʈ ����
                        cardResultRenderer.sprite = resultSprite;
                    }
                }
            }
        }
    }

    void AdjustSpriteScale(SpriteRenderer spriteRenderer, Sprite newSprite)
    {
        // ���� ��������Ʈ�� �� ��������Ʈ�� ũ�� ���� ��������
        Vector2 currentSize = spriteRenderer.sprite.bounds.size;
        Vector2 newSize = newSprite.bounds.size;

        // ���� ��������Ʈ�� �� ��������Ʈ�� ũ�� ���� ���
        Vector2 scale = spriteRenderer.transform.localScale;
        scale.x *= currentSize.x / newSize.x;
        scale.y *= currentSize.y / newSize.y;

        // ������Ʈ�� ���� �������� �� ��������Ʈ�� �°� ����
        spriteRenderer.transform.localScale = scale;
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

    IEnumerator FadeIn()
    {
        // ���� ���¿��� ����
        Color initialColor = spriteRenderer.color;
        Color visibleColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            spriteRenderer.color = Color.Lerp(initialColor, visibleColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���������� ������ ���̰� ����
        spriteRenderer.color = visibleColor;
    }
}
