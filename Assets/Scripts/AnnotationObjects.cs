using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnotationObjects : MonoBehaviour
{
    // Start is called before the first frame update
    public AnnotationManager manager;
    public LandmarkAnnotation data;
    public AnnotationsInformationDisplay information_panel;
    public GameObject anchor_point;

    public void Onclick()
    {
        manager.Active(data,this);
        DisplayInformation();
    }

    public void DisplayInformation()
    {
        information_panel.gameObject.SetActive(true);
        information_panel.Display(data);
    }

    public void DisplayAnnotation()
    {

        var w_point = anchor_point.transform.TransformPoint(data.pivotPoint);
        var screen_point = Camera.main.WorldToScreenPoint(w_point);
        transform.position = screen_point;
        
       
    }

    public void DisableDisplay()
    {
        information_panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayAnnotation();
    }
}
