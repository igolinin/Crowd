using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AIController : MonoBehaviour
{
    //public GameObject goal;
    NavMeshAgent agent;
    public float accuracy = 6f;
    string[] shoppingList = {"fish", "meat", "veg", "chees"};
    public List<GameObject> wp = new List<GameObject>(); 
    private GameObject goal;
    private int goalIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        makePath();
        goal = wp[goalIndex];
        agent = this.GetComponent<NavMeshAgent>();
        //agent.SetDestination(goal.transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(goal.transform.position);
        if(Vector3.Distance(goal.transform.position, transform.position) < accuracy&&goalIndex<4){
            goal = wp[++goalIndex];
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