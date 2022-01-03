using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyScript : MonoBehaviour{
    public float TotalHealth = 15f;
    public float CurrentHealth;
    public float AttackDamage;
    public float MovementSpeed;

    private Animator Anim;
    private CapsuleCollider CapCol;
    private MovementScript ListEnemies;


    public float LookRadius;
    public Transform Target;
    private NavMeshAgent Agent;


    void Start(){
        Anim =  gameObject.GetComponent<Animator>();
        CurrentHealth = TotalHealth;
        CapCol = gameObject.GetComponent<CapsuleCollider>();
        Agent = GetComponent<NavMeshAgent>();
    }
    void Update(){
        float distance = Vector3.Distance(Target.position, transform.position);
        if(distance <= LookRadius){
            Agent.SetDestination(Target.position);

        }
    }
    void LookTarget (){
        Vector3 direction = (Target.position - transform.position).normalized;
        Quaternion lookRotaion = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotaion, Time.deltaTime * 5f);
    }
    private void OnDrawGizmosSelected(){
        Gizmos.color =  Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
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
            //MovementScript.EnemiesList.Remove(gameObject.transform);

        }
    }



}