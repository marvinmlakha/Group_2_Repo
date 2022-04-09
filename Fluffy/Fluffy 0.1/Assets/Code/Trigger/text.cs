using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour
{
    
    [SerializeField] MeshRenderer popUp;

    void Start()
    {
        popUp = popUp.GetComponent<MeshRenderer>();
        popUp.enabled=false;
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            popUp.enabled=true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            popUp.enabled=false;
        }
    }
}
