using System.Linq;
using UnityEngine;

public class FollowPath : MonoBehaviour {

    Transform goal;
    float speed = 5.0f;
    float accuracy = 1.0f;
    float rotSpeed = 2.0f;
    public GameObject wpManager;
    GameObject[] uwps;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    void Start() {

        uwps = wpManager.GetComponent<WPManager>().waypoints;
        var sortedlist = wpManager.GetComponent<WPManager>().waypoints.OrderBy(go => go.name).ToList();
        wps = sortedlist.ToArray();
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[7];
    }

    // Update is called once per frame
    public void GotToHeli() {

        g.AStar(currentNode, wps[1]);
        currentWP = 0;
    }

    public void GoToRuin() {

        g.AStar(currentNode, wps[5]);
        currentWP = 0;
    }

    public void GoToTanks() {

        g.AStar(currentNode, wps[9]);
        currentWP = 0;
    }

    private void LateUpdate() {

        if (g.getPathLength() == 0 || currentWP == g.getPathLength()) {

            return;
        }

        // The node we are closest to at this point
        currentNode = g.getPathPoint(currentWP);

        // If we are close enought to the current waypoint move to next
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy) {

            currentWP++;
        }

        if (currentWP < g.getPathLength()) {

            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x,
                this.transform.position.y,
                goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * rotSpeed);

            this.transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        }
    }
}
