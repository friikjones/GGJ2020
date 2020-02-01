using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameCamera : MonoBehaviour
{

    public Vector3 cameraOffset;
    public float roomTransitionDuration;
    public float zoomedSize;

    [HideInInspector]
    public bool tweening = false;

    Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    public void ZoomRoom(GameObject room){
        tweening = false;
        Vector3 zoomPosition = room.transform.position + cameraOffset;
        transform.DOMove(zoomPosition, roomTransitionDuration);
        //GetComponent<Camera>().DOOrthoSize(zoomedSize, roomTransitionDuration);
        //Disable button
        StartCoroutine(ZoomRoomAsync(room.transform));
    }

    IEnumerator ZoomRoomAsync(Transform target){
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
        tweening = true;
    }

}
