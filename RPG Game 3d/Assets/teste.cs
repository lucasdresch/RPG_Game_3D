using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teste : MonoBehaviour {
    #region Gizmos
    public GameObject objRef;
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public float radius;

    public int testeGismosType;
    
    public MovementScript scriptImport;
    #endregion Gizmos

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
    void OnDrawGizmosSelected(){
        if(testeGismosType == 0){
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(objRef.transform.position + new Vector3(offsetX, offsetY, offsetZ), radius);
        }
        if(testeGismosType == 1){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(objRef.transform.position + transform.forward, radius);
        }

    }
}