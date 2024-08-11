using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera1; // ���� ī�޶�
    public float moveSpeed = 10f; // �̵� �ӵ�
    public program_manager p_manager;
    public float minY ;
    public float maxY ;
    public int pageIndex;
    void Update()
    {
        if (p_manager.pageIndex == 2)
        {
            // ���콺 �� �Է� �ޱ�
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            // ī�޶��� ��ġ�� ���콺 �� �Է¿� ���� ����
            Vector3 position = camera1.transform.position;
            position.y += scrollInput * moveSpeed; // y�� �̵� (���Ʒ�)
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
