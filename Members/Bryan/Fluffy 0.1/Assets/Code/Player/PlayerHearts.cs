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
    GameObject player;


    void Start()
    {
        currentHealth = startHearts * healthPerHeart;
        maxHealth = maxHeartAmount * healthPerHeart;
        checkHealthAmount();
        UpdateHearts();
        player = GameObject.Find("Junior");
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
            i++;
            int curentHeartHealth = (int)(healthPerHeart - (healthPerHeart* i - currentHealth));
            // int healthPerImage = healthPerHeart / (healthSprites.Length-1);
            // int imageIndex = currentHealth / healthPerImage;
            if(currentHealth >= i * healthPerHeart){
                image.sprite = healthSprites[2];
            }
            else if(curentHeartHealth == 1){
                image.sprite = healthSprites[1];
            }
            else{
                image.sprite = healthSprites[0];
            }
        }
    }

    public void TakeDamage(int amount){
        currentHealth -= amount;
        if(currentHealth <= 0) {
            player.GetComponent<PlayerMovement>().Die();
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