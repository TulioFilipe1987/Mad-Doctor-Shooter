using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float health = 100f;

    private Enemy enemyScript;

    [SerializeField]
    private Slider enemyHealthSlider;  // barra de saude 

    private void Awake()
    {
        enemyScript = GetComponent<Enemy>();


    }

    public void TakeDamage(float damageAmount)
    {

        if (health <= 0)
            return;


        health -= damageAmount;
        
        if(health <= 0f)
        {
            health = 0;

            // kill the enemy
            enemyScript.EnemyDied();

            EnemySpawner.instance.EnemyDied(gameObject);  // 1
  
            GameplayController.instance.EnemyKilled(); //2
        }

        enemyHealthSlider.value = health;
    
    }


}// class
