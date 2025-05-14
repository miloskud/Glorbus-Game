using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    public int health;
    public int mana;
    public int maxHealth;
    public int maxMana;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Sprite emptyMana;
    public Sprite fullMana;
    public Image[] hearts;
    public Image[] manaBar;
    public TMP_Text manaPotsDisplay;
    public TMP_Text healthPotsDisplay; 
    public TMP_Text keyDisplay; 

    public int manaPots;
    public int healthPots;
    public int keys;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        manaPotsDisplay.text = manaPots.ToString();
        healthPotsDisplay.text = healthPots.ToString();
        keyDisplay.text = keys.ToString();

        
        for(int i = 0; i < hearts.Length; i++){
            if(i < health){
                hearts[i].sprite = fullHeart;
            }
            else{
                hearts[i].sprite = emptyHeart;
            }
            if(i < maxHealth){
                hearts[i].enabled = true;
            }
            else{
                hearts[i].enabled = false;
            }
        }
        for(int i = 0; i < manaBar.Length; i++){
            if(i < mana){
                manaBar[i].sprite = fullMana;
            }
            else{
                manaBar[i].sprite = emptyMana;
            }
            if(i < maxMana){
                manaBar[i].enabled = true;
            }
            else{
                manaBar[i].enabled = false;
            }
        }
    }
    
    
}
