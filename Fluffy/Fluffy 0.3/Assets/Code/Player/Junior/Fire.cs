using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    Animator myAnimator;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void FireAnimation(){
        myAnimator.SetTrigger("onAttack");
    }
}
