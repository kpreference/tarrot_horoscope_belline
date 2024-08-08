using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public program_manager p_manager;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.tag == "Btn")
        {
            // Ŭ���� ��ġ������ Raycast
            Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);


            // �ݶ��̴��� ���� ������Ʈ�� Ŭ���� ��Ҵ��� Ȯ��
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                string objectName = hit.collider.gameObject.name;
                /*Debug.Log("Clicked object name: " + objectName);
                if (objectName == "btn_back")
                {
                    Debug.Log("asdfasdfasfdasfd");
                }
                else
                {
                    Debug.Log("qwerewrqqrwe");
                }*/
                p_manager.MovePage(objectName);
            }
        }
    }

}
