using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Vector2 Margin, Smoothing;
    private Vector2 first = Vector2.zero;//鼠标第一次落下点
    private Vector2 second = Vector2.zero;//鼠标第二次位置（拖拽位置）
    private Vector3 vecPos = Vector3.zero;
    private bool IsNeedMove = false;//是否需要移动

    public float ChangeSpeed = 10.0f;
    public float maximum = 16;
    public float minmum = 7;
    public float CameraSizeChanged = 0;
    public float x = 0;
    public float y = 0;


    public Grid grid_; // Tilemap的Grid组件

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
            //记录鼠标按下的位置
        }
        if (Event.current.type == EventType.MouseDrag)
        {
            //记录鼠标拖动的位置
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
            // 将鼠标点击的屏幕坐标转换为世界坐标
            Vector3 wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 将世界坐标转换为瓦片坐标
            Vector3Int cellPos = grid_.WorldToCell(wPos);
            // 由于是2D, 所以手动将瓦片的Z坐标改为0, 实际项目可以先查看瓦片的Z坐标值.
            cellPos.z = 0;

        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minmum, maximum);
            Camera.main.orthographicSize = Camera.main.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * ChangeSpeed;
        }
        x = transform.position.x;
        y = transform.position.y;
        x = x - vecPos.x;//向量偏移
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

