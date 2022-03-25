using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnnotationsInformationDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public LandmarkAnnotation current;
    public TMP_Text title;
    public TMP_Text description;
    public void Display(LandmarkAnnotation annotation)
    {
        current = annotation;
        title.text = current.Title;
        description.text = current.Description;

    }
}
