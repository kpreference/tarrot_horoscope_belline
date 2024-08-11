using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera1; // 메인 카메라
    public float moveSpeed = 10f; // 이동 속도
    public program_manager p_manager;
    public float minY ;
    public float maxY ;
    public int pageIndex;
    void Update()
    {
        if (p_manager.pageIndex == 2)
        {
            // 마우스 휠 입력 받기
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            // 카메라의 위치를 마우스 휠 입력에 따라 조정
            Vector3 position = camera1.transform.position;
            position.y += scrollInput * moveSpeed; // y축 이동 (위아래)
            position.y = Mathf.Clamp(position.y, minY, maxY);
            camera1.transform.position = position;
        }
        else
        {
            y_zero();
        }
        
    }
    public void y_zero()
    {
        Vector3 position = camera1.transform.position;
        position.y = 0f;
        camera1.transform.position = position;
    }
}
