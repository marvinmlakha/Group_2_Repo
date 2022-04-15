using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHearts : MonoBehaviour
{
    
    private int maxHeartAmount = 6;
    public static int startHearts = 6;
    private static int healthPerHeart = 2;
    public static int currentHealth = startHearts * healthPerHeart;
    private int maxHealth;

    [SerializeField] PlayerMovement playerMovement;

    public Image[] healthImages;
    public Sprite[] healthSprites;
    GameObject player;
    AudioManager music;


    void Start()
    {   
        if (currentHealth <= 0)
        {
            currentHealth = startHearts * healthPerHeart;
        }

        music = FindObjectOfType<AudioManager>();

        maxHealth = maxHeartAmount * healthPerHeart;
        CheckHealthAmount();
        UpdateHearts();
        player = GameObject.Find("Junior");
    }

    void CheckHealthAmount()
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

        music.Play("Hit");

        if(currentHealth <= 0) {
            player.GetComponent<PlayerMovement>().Die();
        }
        UpdateHearts();
        if (amount > 0) // I added this line so the player won't get knockback after killing an enemy and being healed -Eli
        {
            player.GetComponent<PlayerMovement>().KnockBack();
        }
    }

    public void Heal(int amount){
        currentHealth += amount;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
        UpdateHearts();
    }

    public void AddHeartContainer(){
        startHearts++;
        if(startHearts > maxHeartAmount){
            startHearts = maxHeartAmount;
        } 
        CheckHealthAmount();
    }

    void Update()
    {
        CheckHealthAmount();
        UpdateHearts();
    }
}
