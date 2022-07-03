using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform playerTarget;

    [SerializeField]
    private float moveSpeed = 2f;

    private Vector3 temScale;

    [SerializeField]
    private float stoppingDistance = 1.5f;

    private PlayerAnimation enemyAnimation;  // está instanciando

    [SerializeField]
    private float attackWaitTime = 2.5f;
    private float attackTimer;

    [SerializeField]
    private float attackFinishedWaitTime = 0.5f;
    private float attackFinishedTimer;

    [SerializeField]
    private EnemyDamageArea enemyDamageArea;

    private bool enemyDied;

    [SerializeField]
    private RectTransform healthBarTransform;
    private Vector3 healthBarTempScale;

    private void Awake()
    {

        playerTarget = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;

        enemyAnimation = GetComponent<PlayerAnimation>();


    }

    private void Update()
    {

        if (enemyDied)
            return;  // retrna o valor que foi colocado la 


        SearchForPlayer();
    }

    void SearchForPlayer(){

        if (!playerTarget)
            return;
    
        if(Vector3.Distance(transform.position, playerTarget.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                playerTarget.position, moveSpeed * Time.deltaTime);

            enemyAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);

            HandleFacingDirection();

        }
        else
        {

            CheckIfAttackFinished();
            Attack();

        }
    }

    void HandleFacingDirection(){

        temScale = transform.localScale;

        if (transform.position.x > playerTarget.position.x)
            temScale.x = Mathf.Abs(temScale.x);
        else
            temScale.x = -Mathf.Abs(temScale.x);//absolute = abs

        transform.localScale = temScale;

         

        // health bar scale 

        healthBarTempScale = healthBarTransform.localScale;

        if (transform.localScale.x > 0f)
            healthBarTempScale.x = Mathf.Abs(healthBarTempScale.x);
        else
            healthBarTempScale.x = -Mathf.Abs(healthBarTempScale.x);

        healthBarTransform.localScale = healthBarTempScale; 

    }

    void CheckIfAttackFinished()
    {
        if (Time.time > attackFinishedTimer)
            enemyAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);

    }


    void Attack()
    {
        if(Time.time > attackTimer)
        {

            attackFinishedTimer = Time.time + attackFinishedWaitTime;
            attackTimer = Time.time + attackWaitTime;

            enemyAnimation.PlayAnimation(TagManager.ATTACK_ANIMATIO_NAME);  // as animacoes 



        }
        
    }

    void EnemyAttacked()/// vai pegar do outro script do enemydmage 
    {

        enemyDamageArea.gameObject.SetActive(true);
        enemyDamageArea.ResetDeactivateTimer();
    }


    public void EnemyDied()
    {
        enemyDied = true;
        enemyAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);
        Invoke("DestroyEnemyAfterDelay", 1.5f);
    }

    void  DestroyEnemyAfterDelay()
    {
        Destroy(gameObject);
    }
}
