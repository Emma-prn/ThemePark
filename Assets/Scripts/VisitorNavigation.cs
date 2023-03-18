using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class VisitorNavigation : MonoBehaviour {

    private List<POI> pois;
    private NavMeshAgent agent;
    private VisitorNavigation before = null;
    private Transform goal;
    private POI attraction;
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
                break;
        }
    }

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
                break;
        }
    }
}