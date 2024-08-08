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
            // 클릭한 위치에서의 Raycast
            Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);


            // 콜라이더를 가진 오브젝트에 클릭이 닿았는지 확인
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
