using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro�� ����ϱ� ���� �߰�

public class Fortune : MonoBehaviour
{
    public float fadeDuration = 1.5f; // ���̵� �ƿ��� �ɸ��� �ð�
    private SpriteRenderer spriteRenderer; // ������Ʈ�� ��������Ʈ ������
    public program_manager pmanager; // ������ �����ڸ� ����

    // �����ϰ� ���õ� ��������Ʈ �迭 �� ���� �迭
    public Sprite[] randomSprites;
    public string[] randomMessages;

    // ���� ������ ����
    private static bool isProcessing = false;
    private Sprite selectedSprite;
    private string selectedMessage;

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

        // ���� ��������Ʈ�� ������ ���� �� ����
        if (randomSprites.Length > 0)
        {
            int randomIndex = Random.Range(0, randomSprites.Length);
            selectedSprite = randomSprites[randomIndex];
            selectedMessage = randomMessages[randomIndex]; // ���õ� ������ ����
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
            if (pmanager.pageIndex < pmanager.Pages.Length)
            {
                pmanager.Pages[pmanager.pageIndex].SetActive(true);

                // �� �������� �̸��� "back_will_change"�� ������Ʈ ã��
                GameObject tarrotBack = GameObject.Find("back_will_change");
                if (tarrotBack != null)
                {
                    // tarrot_back�� SpriteRenderer�� �����ͼ� ������ ���õ� ��������Ʈ�� ����
                    SpriteRenderer tarrotBackRenderer = tarrotBack.GetComponent<SpriteRenderer>();
                    if (tarrotBackRenderer != null)
                    {
                        tarrotBackRenderer.sprite = selectedSprite;

                        // ������ ����� ���ο� �ؽ�Ʈ ������Ʈ ����
                        GameObject messageObject = new GameObject("Message");
                        messageObject.transform.SetParent(tarrotBack.transform);

                        // �ؽ�Ʈ ��ġ ���� (������Ʈ �Ʒ�)
                        messageObject.transform.localPosition = new Vector3(0, -tarrotBackRenderer.bounds.size.y / 2 - 0.5f, 0);

                        // TextMeshPro ������Ʈ �߰� �� ����
                        TextMeshPro textMeshPro = messageObject.AddComponent<TextMeshPro>();
                        textMeshPro.text = selectedMessage;
                        textMeshPro.fontSize = 24;
                        textMeshPro.alignment = TextAlignmentOptions.Left; // ���� ����

                        // �ؽ�Ʈ ���� ����
                        textMeshPro.color = Color.black;

                        // �ؽ�Ʈ ũ�⸦ �����ϴ� �Լ� ȣ��
                        AdjustTextSizeToCamera(textMeshPro);
                    }
                }
            }
            else
            {
                // ������ �ε����� ������ �Ѿ�� ó�� (��: ������ �������� ���)
                pmanager.pageIndex = 0;
                pmanager.Pages[pmanager.pageIndex].SetActive(true);

                // �� �������� �̸��� "back_will_change"�� ������Ʈ ã��
                GameObject tarrotBack = GameObject.Find("back_will_change");
                if (tarrotBack != null)
                {
                    // tarrot_back�� SpriteRenderer�� �����ͼ� ������ ���õ� ��������Ʈ�� ����
                    SpriteRenderer tarrotBackRenderer = tarrotBack.GetComponent<SpriteRenderer>();
                    if (tarrotBackRenderer != null)
                    {
                        tarrotBackRenderer.sprite = selectedSprite;

                        // ������ ����� ���ο� �ؽ�Ʈ ������Ʈ ����
                        GameObject messageObject = new GameObject("Message");
                        messageObject.transform.SetParent(tarrotBack.transform);

                        // �ؽ�Ʈ ��ġ ���� (������Ʈ �Ʒ�)
                        messageObject.transform.localPosition = new Vector3(0, -tarrotBackRenderer.bounds.size.y / 2 - 0.5f, 0);

                        // TextMeshPro ������Ʈ �߰� �� ����
                        TextMeshPro textMeshPro = messageObject.AddComponent<TextMeshPro>();
                        textMeshPro.text = selectedMessage;
                        textMeshPro.fontSize = 24;
                        textMeshPro.alignment = TextAlignmentOptions.Left; // ���� ����

                        // �ؽ�Ʈ ���� ����
                        textMeshPro.color = Color.black;

                        // �ؽ�Ʈ ũ�⸦ �����ϴ� �Լ� ȣ��
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

        // �ؽ�Ʈ�� ���� ī�޶� ȭ���� �Ѿ�� ���� ũ�⸦ ����
        while (textMeshPro.preferredWidth > screenWidth)
        {
            textMeshPro.fontSize -= 1; // fontSize�� �ٿ��� ���� ũ�⸦ ����
            if (textMeshPro.fontSize <= 12) // �ּ� ũ�⸦ ������ ���� ���� ����
            {
                textMeshPro.fontSize = 12;
                break;
            }
        }

        // �ٹٲ� ���� (�ʿ� �� �ڵ� �ٹٲ�)
        textMeshPro.enableWordWrapping = true;
        textMeshPro.overflowMode = TextOverflowModes.Overflow;
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
