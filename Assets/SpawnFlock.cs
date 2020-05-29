using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlock : MonoBehaviour
{
    public GameObject prefab;
	
	public List<GameObject> allPrefabs = new List<GameObject>();
	
	
	
	
    public float nextSpawn = 2f;
    public float SpawnRate = 2f;
    

	[Header("Crowd Settings")]
	[Range(0.0f, 10.0f)]
	public float accuracy;
	[Range(0.0f, 10.0f)]
	public float avoidDistance;
	[Range(0.0f, 10.0f)]
	public float sensitivity;
	[Range(0.0f, 100.0f)]
	public float flockingRules;
	[Range(0.0f, 5.0f)]
	public float minSpeed;
	[Range(0.0f, 10.0f)]
	public float maxSpeed;
	[Range(1.0f, 10.0f)]
	public float neighbourDistance;
	[Range(0.0f, 10.0f)]
	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
        

		if(Time.time >= nextSpawn){
            nextSpawn = Time.time +SpawnRate;
            var newGuy = (GameObject)Instantiate (prefab, this.transform.position, this.transform.rotation);
			allPrefabs.Add(newGuy);
            newGuy.GetComponent<BlueGuy>().flock = this;
        }
	}
}
