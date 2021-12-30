using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour{
    public float TotalHealth = 15f;
    public float CurrentHealth;
    public float AttackDamage;
    public float MovementSpeed;

    private Animator Anim;
    private CapsuleCollider CapCol;

    void Start(){
        Anim =  gameObject.GetComponent<Animator>();
        CurrentHealth = TotalHealth;
        CapCol = gameObject.GetComponent<CapsuleCollider>();
    }
    void Update(){
        Die();
    }

    public void GetHit(float Damage){
        CurrentHealth -= Damage;
        if(CurrentHealth > 0f){
            Anim.SetInteger("TransitionState", 3);
            StartCoroutine(RecoveryFromHit());
        }else{
            Anim.SetInteger("TransitionState", 4);
            CapCol.enabled = false;
        }
    }

    IEnumerator RecoveryFromHit(){
        yield return new WaitForSeconds(1f);
        Anim.SetInteger("TransitionState", 0);
    }

    void Die(){
        if( CurrentHealth <= 0){
            Anim.SetInteger("TransitionState", 4);
            Destroy(gameObject, 15f);
        }
    }



}