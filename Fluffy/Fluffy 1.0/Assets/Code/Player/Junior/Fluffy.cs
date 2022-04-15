using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fluffy : MonoBehaviour
{
    BoxCollider2D myFluffyCollider;
    PlayerMovement playerMovement;
    Vector3 position;
    Vector3 newPosition;

    [SerializeField] Sprite[] fluffySprites;
    [SerializeField] SpriteRenderer image;
    static int i = 0;

    int waitBetweenShake = 20;

    float shake;

    void Start()
    {
        myFluffyCollider = GetComponent<BoxCollider2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        image = GetComponent<SpriteRenderer>();
        shake = 0.05f;

        CheckImage();
    }

    void Update()
    {  
        waitBetweenShake -=1; 
        position = playerMovement.transform.position + new Vector3 (-1,2);

        if(waitBetweenShake <= 0){
            newPosition = UnityEngine.Random.insideUnitSphere * shake;
            transform.position = position + newPosition;
            waitBetweenShake = 25;
        }
        else{
            transform.position = position + newPosition;
        }
    }

    public void CheckImage(){
        if (SceneManager.GetActiveScene().buildIndex == 2){
            image.sprite = fluffySprites[0];
        }
        else if(SceneManager.GetActiveScene().buildIndex == 3){
            image.sprite = fluffySprites[1];
        }
        else if(SceneManager.GetActiveScene().buildIndex == 4){
            image.sprite = fluffySprites[3];
        }
        else if(SceneManager.GetActiveScene().buildIndex == 5){
            image.sprite = fluffySprites[4];
        }
        else if(SceneManager.GetActiveScene().buildIndex == 6){
            image.sprite = fluffySprites[5];
        }
    }

    public void ChangeImage(){
        i++;
        image.sprite = fluffySprites[i];
    }

    public void ResetImage(){
        i = 0;
        image.sprite = fluffySprites[i];
    }
}
