using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //public Transform sp;
    public float nextSpawn = 2f;
    public float SpawnRate = 2f;
    public GameObject NPC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextSpawn){
            nextSpawn = Time.time +SpawnRate;
            Instantiate (NPC, this.transform.position, this.transform.rotation);
        }
    }
}
