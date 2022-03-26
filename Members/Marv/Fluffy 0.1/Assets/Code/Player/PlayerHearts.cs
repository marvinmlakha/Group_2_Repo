using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHearts : MonoBehaviour
{
    
    private int maxHeartAmount = 6;
    public int startHearts = 4;
    public int currentHealth;
    private int maxHealth;
    private int healthPerHeart = 2;

    [SerializeField] PlayerMovement playerMovement;

    public Image[] healthImages;
    public Sprite[] healthSprites;



    void Start()
    {
        currentHealth = startHearts * healthPerHeart;
        maxHealth = maxHeartAmount * healthPerHeart;
        checkHealthAmount();
        UpdateHearts();
    }

    void checkHealthAmount()
    {
        for(int i = 0; i < maxHeartAmount; i++){
            if(startHearts <= i){
                healthImages[i].enabled = false;
            } else {
                healthImages[i].enabled = true;
            }
        }
        UpdateHearts();
    }

    void UpdateHearts(){
        bool empty = false;
        int i = 0;

        foreach (Image image in healthImages){
            if(empty){
                image.sprite = healthSprites[0];
            }else{
                i++;
                if(currentHealth >= i * healthPerHeart){
                    image.sprite = healthSprites[2];
                }
                else{
                    int curentHeartHealth = (int)(healthPerHeart - (healthPerHeart* i - currentHealth));
                    int healthPerImage = healthPerHeart / (healthSprites.Length-1);
                    int imageIndex = currentHealth / healthPerImage;
                    image.sprite = healthSprites[1];
                    empty = true;
                }
            }
        }
    }

    public void TakeDamage(int amount){
        currentHealth -= amount;
        if(currentHealth < 0) {
            // die();
        }
        UpdateHearts();
    }

    public void AddHeartContainer(){
        startHearts++;
        if(startHearts > maxHeartAmount){
            startHearts = maxHeartAmount;
        } 
        checkHealthAmount();
    }

    void Update()
    {
        checkHealthAmount();
        UpdateHearts();
    }
}
