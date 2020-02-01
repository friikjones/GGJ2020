using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameCamera : MonoBehaviour
{

    public Vector3 cameraOffset;
    public float roomTransitionDuration;

    [HideInInspector]
    public bool zoomed = false;
    [HideInInspector]
    public bool tweening = false;

    Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    public void ZoomInRoom(GameObject room){
        if (tweening || zoomed)
            return;
        tweening = true;
        Vector3 zoomPosition = room.transform.position + cameraOffset;
        transform.DOMove(zoomPosition, roomTransitionDuration);
        //Disable button
        StartCoroutine(ZoomInRoomAsync(room.transform));
    }

    IEnumerator ZoomInRoomAsync(Transform target){
        float tweenToLookAtDuration = roomTransitionDuration/2;
        float timeCount = 0f;
        Quaternion originalRotation = target.rotation;
        while(timeCount < roomTransitionDuration){
            //transform.LookAt(target);
            Quaternion lookAtRotation = Quaternion.LookRotation(target.position-transform.position);
            transform.rotation = Quaternion.Lerp(originalRotation, lookAtRotation, timeCount/tweenToLookAtDuration);
            timeCount += Time.deltaTime;
            yield return null;
        }
        tweening = false;
        zoomed = true;
    }

    public void ZoomOutRoom(){
        if (tweening || !zoomed)
            return;
        tweening = true;
        transform.DOMove(originalPosition, roomTransitionDuration);
        StartCoroutine(ZoomOutRoomAsync());
    }

    IEnumerator ZoomOutRoomAsync(){
        float tweenToLookAtDuration = roomTransitionDuration/2;
        float timeCount = 0f;
        Quaternion originalRotation = Quaternion.identity;
        while(timeCount < roomTransitionDuration){
            //transform.LookAt(target);
            Quaternion lookAtRotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = Quaternion.Lerp(originalRotation, lookAtRotation, timeCount/tweenToLookAtDuration);
            timeCount += Time.deltaTime;
            yield return null;
        }
        tweening = false;
        zoomed = false;
    }

}
