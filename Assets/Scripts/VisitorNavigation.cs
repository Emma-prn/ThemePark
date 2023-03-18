using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class VisitorNavigation : MonoBehaviour {

    private List<POI> pois;
    private NavMeshAgent agent;
    private Renderer renderer;
    private Collider collider;
    private VisitorNavigation before = null;
    private Transform goal;
    private POI attraction;
    private bool isIn = false;
    public enum status
    {
        Walk,
        Wait,
        Visit
    }
    public status currentStatus {get; private set;}
    public float distanceBetweenVisitor = 2.0f;

    public void newDestination(){
        int goalIndex = Random.Range(0,pois.Count);
        attraction = pois[goalIndex];
        agent.SetDestination(attraction.getWaitingList());
    }

    public void setNewDestination(Vector3 destination){
        agent.SetDestination(destination);
    }

    public void setInWaitingLine(VisitorNavigation visitor_before){
        before = visitor_before;
    }

    public bool canEnter(){
        return isIn = true;
    }

    public void setStatus(status req_status){
        switch(req_status){
            case status.Walk:
                agent.avoidancePriority = 50;
                newDestination();
                currentStatus = status.Walk;
                break;
            case status.Wait:
                setNewDestination(transform.position);
                agent.avoidancePriority = 1;
                currentStatus = status.Wait;
                break;
            case status.Visit:
                currentStatus = status.Visit;
                //StartCoroutine("AttractionVisit");
                break;
        }
    }

    /*private IEnumerator AttractionVisit(){
        yield return new WaitForSeconds(attraction.visit_time);
        attraction.removeOneVisitor();
        transform.position = new Vector3(attraction.poi_exit.position.x, agent.transform.position.y, attraction.poi_exit.position.z);
        renderer.enabled = true;
        collider.enabled = true;
        agent.enabled = true;
        newDestination();
    }*/

    private void Follow(){
        if(before != null){
            Vector3 dist = transform.position - before.transform.position;

            Quaternion rotation = new Quaternion();
            rotation.SetLookRotation(-dist);
            transform.rotation = rotation;

            if(dist.magnitude > distanceBetweenVisitor){
                setNewDestination(before.transform.position + dist.normalized * distanceBetweenVisitor);
            }
        }
        else {
            setNewDestination(attraction.poi_entry.position);
        }
    }

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
        pois = PoiManager.Instance.pois;
        setStatus(status.Walk);
    }

    
    void Update () {
        switch(currentStatus)
        {
            case status.Walk:
                Vector3 waiting_line_position = attraction.getWaitingList();

                if((agent.destination - waiting_line_position).magnitude > 0.05){
                    setNewDestination(waiting_line_position);
                }
                break;

            case status.Wait:
                Follow();
                break;

            case status.Visit:
                /*if(isIn){
                    Debug.Log("Entrée");
                    attraction.addOneVisitor();
                    renderer.enabled = false;
                    collider.enabled = false;
                    agent.enabled = false;
                    isIn = false;
                }*/
                break;
        }
    }
}