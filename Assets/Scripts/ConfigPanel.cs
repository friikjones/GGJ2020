using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigPanel : MonoBehaviour
{

    Animator anim;
    bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void ToggleConfigPanel() {
        if (anim) {
            isOpen = !isOpen;
            anim.SetBool("open", isOpen);
        }
    }

}
