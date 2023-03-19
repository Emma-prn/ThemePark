using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;


public class CrowNavigation : MonoBehaviour
{
    private NavMeshAgent agent;

    void chooseRandomPosition(){
        NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 50f, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        chooseRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
