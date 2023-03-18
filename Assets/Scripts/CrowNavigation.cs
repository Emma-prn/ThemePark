using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;


public class CrowNavigation : MonoBehaviour
{
    private NavMeshAgent agent;

    /*public Vector3 chooseRandomPosition(){
        NavMeshHit navHit;
        NavMesh.SamplePosition(Random.insideUnitSphere*navMesh.sourceBounds.extents.magnitude, out navHit, navMesh.sourceBounds.extents.magnitude, NavMesh.AllAreas);
        return navHit.position;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Transform goal = new GameObject().transform;
        //goal.position = chooseRandomPosition();
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
