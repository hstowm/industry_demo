using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnotationManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<LandmarkAnnotation> list_annotation;
    public GameObject canvas;

    public Vector3 pivot_point;
    float ZoomAmount; //With Positive and negative values
    float MaxToClamp;
    float ROTSpeed;
    float ZOOMSpeed;
    public GameObject demo;
    bool animated_camera;
    float transition_time;
    Vector3 camera_destination;
    Quaternion camera_rotation_destination;
    Vector3 camera_start_position;
    Quaternion camera_start_rotation;
    Vector3 original_position;
    float move_time;
    AnnotationObjects current_annotations;

    Vector3 m_pos_delta;
    Vector3 m_prev_pos;
    bool dragging;

    void Start()
    {
        MaxToClamp = 12;
        ROTSpeed = 90;
        ZOOMSpeed = 40;
        animated_camera = false;
        transition_time = 2;

        m_pos_delta = Vector3.zero;
        m_prev_pos = Vector3.zero;

        foreach (LandmarkAnnotation l in list_annotation)
        {
            GenerateAnnotation(l);
        }

        pivot_point = demo.transform.position;
        original_position = demo.transform.position;
    }

    internal void Active(LandmarkAnnotation data,AnnotationObjects current)
    {
        if(current_annotations!=null)
        {
            current_annotations.DisableDisplay();
        }
       
        animated_camera = true;

        demo.transform.rotation = Quaternion.identity;
        demo.transform.position = original_position;
        pivot_point = demo.transform.TransformPoint(data.pivotPoint);
        move_time = 0;
        camera_destination = data.cameraPosition;
       
        camera_start_position = Camera.main.transform.position;
        camera_rotation_destination = data.CameraRotation;
        camera_start_rotation = Camera.main.transform.rotation;

        current_annotations = current;
        
    }

    private void MoveCameraToLandmark()
    {

        move_time += Time.deltaTime;

        Camera.main.transform.position = Vector3.Lerp(camera_start_position,camera_destination,move_time/transition_time);
        
        Camera.main.transform.rotation = Quaternion.Slerp(camera_start_rotation,camera_rotation_destination,move_time/transition_time);
        if(move_time >= transition_time)
        {
            animated_camera = false;
        }
    }

    public void GenerateAnnotation(LandmarkAnnotation landmark)
    {
        GameObject annotation_mark = Instantiate(landmark.prefab, canvas.transform);
        AnnotationObjects annotationObjects = annotation_mark.GetComponent<AnnotationObjects>();
        annotationObjects.data = landmark;
        annotationObjects.manager = this;
        annotationObjects.anchor_point = demo;
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

        if (animated_camera)
        {
            
            MoveCameraToLandmark();
        }
    }

    private void RotateAround()
    {
        //demo.transform.Rotate(0, -Input.GetAxis("Mouse X") * ROTSpeed * Time.deltaTime, -Input.GetAxis("Mouse Y") * ROTSpeed * Time.deltaTime, Space.World);
        //demo.transform.RotateAround(pivot_point, Camera.main.transform.up, -Input.GetAxis("Mouse X") * ROTSpeed * Time.deltaTime);
        //demo.transform.RotateAround(pivot_point, Camera.main.transform.right, Input.GetAxis("Mouse Y") * ROTSpeed * Time.deltaTime);

        if (!dragging) return;
        m_pos_delta = Input.mousePosition - m_prev_pos;
        if (Vector3.Dot(demo.transform.up, Vector3.up) >= 0)
        {
            demo.transform.RotateAround(pivot_point,demo.transform.up, -Vector3.Dot(m_pos_delta, Camera.main.transform.right) / 20);
        }
        else
        {
            demo.transform.RotateAround(pivot_point, demo.transform.up, Vector3.Dot(m_pos_delta, Camera.main.transform.right) / 20);
        }

        demo.transform.RotateAround(pivot_point, Camera.main.transform.right, Vector3.Dot(m_pos_delta, Camera.main.transform.up) / 20);
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
