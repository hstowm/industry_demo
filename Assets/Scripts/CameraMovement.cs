using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    float ZoomAmount; //With Positive and negative values
    float MaxToClamp;
    float ROTSpeed;
    float ZOOMSpeed;
    public GameObject demo;
    Vector3 m_pos_delta;
    Vector3 m_prev_pos;
    bool dragging;
    void Start()
    {
        MaxToClamp = 12;
        ROTSpeed = 720*2;
        ZOOMSpeed = 10;
        m_pos_delta = Vector3.zero;
        m_prev_pos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();


        if (Input.GetMouseButton(0))
        {
            RotateAround();
            m_prev_pos = Input.mousePosition;
            dragging = true;
        }
        else
        {
            dragging = false;
        }

        
    }

    private void RotateAround()
    {

        if (!dragging) return;
        m_pos_delta = Input.mousePosition - m_prev_pos;
        if(Vector3.Dot(demo.transform.up,Vector3.up )>=0)
        {
            demo.transform.Rotate(demo.transform.up, -Vector3.Dot(m_pos_delta, Camera.main.transform.right)/20, Space.World);
        }
        else
        {
            demo.transform.Rotate(demo.transform.up, Vector3.Dot(m_pos_delta, Camera.main.transform.right)/20, Space.World);
        }

        demo.transform.Rotate(Camera.main.transform.right, Vector3.Dot(m_pos_delta, Camera.main.transform.up)/20, Space.World);

    }

    public void Zoom()
    {
        ZoomAmount += Input.GetAxis("Mouse ScrollWheel");
        Debug.Log("Middle mouse movement " + Input.GetAxis("Mouse ScrollWheel"));
        ZoomAmount = Mathf.Clamp(ZoomAmount, -MaxToClamp, MaxToClamp);
        var translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), MaxToClamp - Mathf.Abs(ZoomAmount));
        Camera.main.transform.Translate(0, 0, translate * ZOOMSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));
    }
}
