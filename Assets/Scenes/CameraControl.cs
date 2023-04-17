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
    public float maximum = 25;
    public float minmum = 10;
    

    public void Start()
    {
        first.x = transform.position.x;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minmum, maximum);
            Camera.main.orthographicSize = Camera.main.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * ChangeSpeed;
        }
    }
}
