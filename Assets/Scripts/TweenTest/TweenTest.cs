using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        transform.DOMove(new Vector3(2,3,4), 1);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
