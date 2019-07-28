using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFragments : MonoBehaviour//, IPunObservable
{
    public float LifeTime;
    private void Awake() {
        transform.parent=null;
    }
    void Start()
    {
       
        if (LifeTime != 0)
        {
            LifeTime = Random.Range(LifeTime - 1, LifeTime);
            Destroy(gameObject,LifeTime);
        }
      
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Death")){
            Destroy(gameObject);
        }
    }
   
   
    
}