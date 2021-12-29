
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {
    public float Speed;
    public float RotSpeed;
    public float Rotation;
    public float Gravity;
    private Vector3 MoveDirection;
    private CharacterController CharCtrlr;
    public Animator Anim;
    public Rigidbody Rig;

    private bool coroutinePlay;
    
    List<Transform> EnemiesList = new List<Transform>();
    public float ColliderRadius;

    // Start is called before the first frame update
    void Start() {
        CharCtrlr =  gameObject.GetComponent<CharacterController>();
        Rig = gameObject.GetComponent<Rigidbody>();

        Anim = gameObject.GetComponent<Animator>();
        coroutinePlay = false;
    }

    // Update is called once per frame
    void Update() {
        Move();
        GetMouseInput();
    }
    void Move(){
        if(CharCtrlr.isGrounded){
            if(Input.GetKey(KeyCode.W)){
                if(!Anim.GetBool("AnimSetAtkBool")){
                    Anim.SetBool("AnimSetRunBool", true);
                    Anim.SetInteger("AnimTransitionState", 1);
                    MoveDirection =  Vector3.forward * Speed;
                    MoveDirection = transform.TransformDirection(MoveDirection);
                }else{
                    Anim.SetBool("AnimSetRunBool", false);
                    MoveDirection = Vector3.zero;
                    StartCoroutine(Attack(1));
                }
            }

            if (Input.GetKeyUp(KeyCode.W)){
                Anim.SetBool("AnimSetRunBool", false);
                Anim.SetInteger("AnimTransitionState", 0);
                MoveDirection = Vector3.zero;
            }
        }
        Rotation += Input.GetAxis("Horizontal") * RotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, Rotation, 0);

        MoveDirection.y -= Gravity * Time.deltaTime;
        CharCtrlr.Move(MoveDirection * Time.deltaTime);
    }
    void GetMouseInput(){
        if(CharCtrlr.isGrounded){
            if(Input.GetMouseButtonDown(0)){
                if(Anim.GetBool("AnimSetRunBool")){
                    Anim.SetBool("AnimeSetRunBool", false);
                    Anim.SetInteger("AnimTrasitionState", 0);
                }
                if(!Anim.GetBool("AnimSetRunBool")){
                    StartCoroutine(Attack(0));
                }
            }
        }
        
    }
    IEnumerator Attack(int transitionVal){
        if(!coroutinePlay){
            coroutinePlay = true;
            Anim.SetBool("AnimSetAtkBool", true);
            Anim.SetInteger("AnimTransitionState", 2);
            
            yield return new WaitForSeconds(0.3f);

            GetEnemiesRange();
            foreach(Transform enemies in EnemiesList){
                //executar acao de dano (script no inimigo)
                EnemyScript enemy = enemies.GetComponent<EnemyScript>();
                if ( enemy != null){
                    enemy.GetHit();
                }
            }
            yield return new WaitForSeconds(1f);

            Anim.SetInteger("AnimTransitionState", transitionVal);
            Anim.SetBool("AnimSetAtkBool", false);
            coroutinePlay = false;
        }
        
    }
    void GetEnemiesRange(){
        foreach(Collider c in Physics.OverlapSphere((transform.position + transform.forward * ColliderRadius), ColliderRadius)){
            if(c.gameObject.CompareTag("Enemy")){
                EnemiesList.Add(c.transform);
            }
        }
    }
    private void OnDraawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, ColliderRadius);
    }

}
