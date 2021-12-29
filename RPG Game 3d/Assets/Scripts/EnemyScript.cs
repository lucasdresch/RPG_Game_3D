using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour{
    public float TotalHealth;
    public float CurrentHealth;
    public float AttackDamage;
    public float MovementSpeed;

    private Animator Anim;

    void Start(){
        Anim =  gameObject.GetComponent<Animator>();
    }

    public void GetHit(){
        Debug.Log("Morri!");
        Destroy(gameObject);
    }

    IEnumerator RecoveryFromHit(){
        yield return new WaitForSeconds(1f);
        Anim.SetInteger("TransitionState", 0);
    }



}