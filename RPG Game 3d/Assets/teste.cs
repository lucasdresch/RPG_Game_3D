using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teste : MonoBehaviour
{
    public float radius;
    private void OnDraawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(0f, 0f, 0f), 5f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
