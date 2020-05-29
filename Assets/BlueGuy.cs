using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGuy : MonoBehaviour
{
    public SpawnFlock flock;
    public float speed;
    public float flockingRules;
	public float sensitivity;
	public float avoidDistance;
	
	//bool turning = false;

    string[] shoppingList = {"fish", "meat", "veg", "chees"};
    public float accuracy;
    public List<GameObject> wp = new List<GameObject>();

    public GameObject goal;
    private int goalIndex = 0;
	public bool stopped = false;

	// Use this for initialization
	void Start () {
        makePath();
        goal = wp[goalIndex];
		speed = Random.Range(flock.minSpeed,
								flock.maxSpeed);
		
		
	}
    

    // Update is called once per frame
    void Update()
    {
        //Bounds b = new Bounds(flock.transform.position, flock.swimLimits*2);
		
	
		RaycastHit hit = new RaycastHit();
		Vector3 direction = Vector3.zero;
        flockingRules = flock.flockingRules;
		accuracy = flock.accuracy;
		sensitivity = flock.sensitivity;
		avoidDistance = flock.avoidDistance;

		if (Physics.Raycast(transform.position, this.transform.forward , out hit, sensitivity*speed))
		{
			//turning = true;
            Debug.DrawRay(this.transform.position , this.transform.forward*sensitivity*speed,Color.red);
			direction = Vector3.Reflect(this.transform.forward, hit.normal);
		}
		else {
			Debug.DrawRay(this.transform.position , this.transform.forward*sensitivity*speed,Color.blue);
			direction = goal.transform.position-transform.position;
		}
			//turning = false;
			//direction = goal.transform.position-transform.position;

		
			
			
		transform.rotation = Quaternion.Slerp(transform.rotation,
					                                  Quaternion.LookRotation(direction), 
					                                  flock.rotationSpeed * Time.deltaTime);
		
			/* if(Random.Range(0,100) < 10)
				speed = Random.Range(flock.minSpeed,
								flock.maxSpeed); */
		if(Random.Range(0,100) < flockingRules)
				ApplyRules();
		
		if(Vector3.Distance(goal.transform.position, transform.position) < accuracy&&goalIndex<4){
            goal = wp[++goalIndex];
        }
		if(Vector3.Distance(goal.transform.position, transform.position) <15 && goalIndex==4){
            stopped = true;
        }
		if(!stopped){
			transform.Translate(0, 0, Time.deltaTime * speed);
		}
		
    }
    void ApplyRules()
	{
		List<GameObject> gos;
		gos = flock.allPrefabs;
		
		Vector3 vcentre = Vector3.zero;
		Vector3 vavoid = Vector3.zero;
		float gSpeed = 0.01f;
		float nDistance;
		int groupSize = 0;

		foreach (GameObject go in gos ) 
		{
			BlueGuy anotherGuy = go.GetComponent<BlueGuy>();
            if(go != this.gameObject && anotherGuy.goal == this.goal)
			{
				nDistance = Vector3.Distance(go.transform.position,this.transform.position);
				if(nDistance <= flock.neighbourDistance)
				{
					vcentre += go.transform.position;	
					groupSize++;	
					
					if(nDistance < avoidDistance)		
					{
						vavoid = vavoid + (this.transform.position - go.transform.position);
					}
					
					
					gSpeed = gSpeed + anotherGuy.speed;
				}
			}
		} 
		
		if(groupSize > 0)
		{
			vcentre = vcentre/groupSize + (goal.transform.position - this.transform.position);
			speed = gSpeed/groupSize;
			
			Vector3 direction = (vcentre + vavoid) - transform.position;
			if(direction != Vector3.zero)
				transform.rotation = Quaternion.Slerp(transform.rotation,
					                                  Quaternion.LookRotation(direction), 
					                                  flock.rotationSpeed * Time.deltaTime);
		
		}else{
            Vector3 direction = goal.transform.position - transform.position;
			if(direction != Vector3.zero)
				transform.rotation = Quaternion.Slerp(transform.rotation,
					                                  Quaternion.LookRotation(direction), 
					                                  flock.rotationSpeed * Time.deltaTime);
        }
	}
    void makePath(){
        foreach(string product in shoppingList){
           wp.Add(GameObject.FindGameObjectsWithTag(product)[Random.Range(0,3)]); 
        }
        wp.Add(GameObject.FindWithTag("Stage"));
            //GameObject[] ProductShops;
    }
}
