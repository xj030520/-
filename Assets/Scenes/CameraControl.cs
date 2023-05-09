using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Vector2 Margin, Smoothing;
    private Vector2 first = Vector2.zero;//����һ�����µ�
    private Vector2 second = Vector2.zero;//���ڶ���λ�ã���קλ�ã�
    private Vector3 vecPos = Vector3.zero;
    private bool IsNeedMove = false;//�Ƿ���Ҫ�ƶ�

    public float ChangeSpeed = 10.0f;
    public float maximum = 16;
    public float minmum = 7;
    public float CameraSizeChanged = 0;
    public float x = 0;
    public float y = 0;


    public Grid grid_; // Tilemap��Grid���

    public void Start()
    {
        first.x = transform.position.x;
        first.y = transform.position.y;
    }

    public void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            first = Event.current.mousePosition;
            //��¼��갴�µ�λ��
        }
        if (Event.current.type == EventType.MouseDrag)
        {
            //��¼����϶���λ��
            second = Event.current.mousePosition;
            Vector3 fir = Camera.main.ScreenToViewportPoint(new Vector3(first.x, first.y, -1));
            Vector3 sec = Camera.main.ScreenToViewportPoint(new Vector3(second.x, second.y, -1));
            vecPos = (sec - fir) * 50;
            first = second;
            IsNeedMove = true;


        }
        else
        {
            IsNeedMove = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // �����������Ļ����ת��Ϊ��������
            Vector3 wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // ����������ת��Ϊ��Ƭ����
            Vector3Int cellPos = grid_.WorldToCell(wPos);
            // ������2D, �����ֶ�����Ƭ��Z�����Ϊ0, ʵ����Ŀ�����Ȳ鿴��Ƭ��Z����ֵ.
            cellPos.z = 0;

        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minmum, maximum);
            Camera.main.orthographicSize = Camera.main.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * ChangeSpeed;
        }
        x = transform.position.x;
        y = transform.position.y;
        x = x - vecPos.x;//����ƫ��
        y = y + vecPos.y;
        x = Mathf.Clamp(x, -10 - Camera.main.orthographicSize / 6, 10 + Camera.main.orthographicSize / 6);
        y = Mathf.Clamp(y, -15 - Camera.main.orthographicSize/12 , 10 + Camera.main.orthographicSize / 12);
        //y = Mathf.Clamp(y, 30-Camera.main.orthographicSize, 30+Camera.main.orthographicSize);
        if (IsNeedMove == false)
        {
            return;
        }
        transform.position = new Vector3(x, y, -5);
    }
}

