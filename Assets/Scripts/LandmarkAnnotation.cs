using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Annotation/Create Landmark Data", order = 1)]
public class LandmarkAnnotation : ScriptableObject
{
    // Start is called before the first frame update
    public Vector3 pivotPoint;
    public Vector3 cameraPosition;
    public Quaternion CameraRotation;
    public string Title;
    public string Description;
    public string NextScene;
    public GameObject prefab;
    public AnnotationObjects scripts;
    
}
