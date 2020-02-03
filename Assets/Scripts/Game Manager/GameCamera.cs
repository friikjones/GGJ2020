using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameCamera : MonoBehaviour
{

    public Vector3 cameraOffset;
    public float roomTransitionDuration;
    public Transform lastTarget;

    [HideInInspector]
    float tweenToLookAtDuration;
    [HideInInspector]
    public bool zoomed = false;
    [HideInInspector]
    public bool tweening = false;

    Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        tweenToLookAtDuration = 2*roomTransitionDuration/3;
    }

    public bool ZoomInRoom(GameObject room){
        if (tweening || zoomed)
            return false;
        tweening = true;
        Vector3 zoomPosition = room.transform.position + cameraOffset;
        transform.DOMove(zoomPosition, roomTransitionDuration);
        StartCoroutine(ZoomInRoomAsync(room.transform));
        return true;
    }

    IEnumerator ZoomInRoomAsync(Transform target){
        float timeCount = 0f;
        lastTarget = target;
        target.GetComponentInChildren<GenericMachine>().isFocused = true;
        Quaternion originalRotation = target.rotation;
        Quaternion lookAtRotation;
        while(timeCount < roomTransitionDuration){
            lookAtRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Lerp(originalRotation, lookAtRotation, timeCount/tweenToLookAtDuration);
            timeCount += Time.deltaTime;
            yield return null;
        }
        tweening = false;
        zoomed = true;
    }

    public bool ZoomOutRoom(){
        if (tweening || !zoomed)
            return false;
        tweening = true;
        transform.DOMove(originalPosition, roomTransitionDuration);
        StartCoroutine(ZoomOutRoomAsync());
        return true;
    }

    IEnumerator ZoomOutRoomAsync(){
        float timeCount = 0f;
        Quaternion originalRotation = Quaternion.identity;
        lastTarget.GetComponentInChildren<GenericMachine>().isFocused = false;
        while(timeCount < roomTransitionDuration){
            Quaternion lookAtRotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = Quaternion.Lerp(originalRotation, lookAtRotation, timeCount/tweenToLookAtDuration);
            timeCount += Time.deltaTime;
            yield return null;
        }
        tweening = false;
        zoomed = false;
    }

}
