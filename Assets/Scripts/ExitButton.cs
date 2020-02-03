using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExitButton : MonoBehaviour
{

    public float transitionDuration = 0.5f;

    Vector2 originalPosition; 
    Vector2 hidingPosition; 

    void Start()
    {
        originalPosition = transform.position;
        hidingPosition = transform.position + new Vector3(+130,0,0);
        transform.position = hidingPosition;
    }

    public void ShowAnim(){
        transform.DOMove(originalPosition, transitionDuration);
    }

    public void HideAnim(){
        transform.DOMove(hidingPosition, transitionDuration);
    }

}
