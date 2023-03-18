using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitingLine : MonoBehaviour
{
    private Queue<VisitorNavigation> waintingVisitors = new Queue<VisitorNavigation>();
    private POI attraction;
    private VisitorNavigation last = null;
    private Collider hit;

    private void Start()
    {
        attraction = transform.parent.GetComponent<POI>();
        hit = GetComponent<Collider>();
    }

    public bool ContainsVisitor(){
        return waintingVisitors.Count > 0;
    }

    public int getNumberVisitor(){
        return waintingVisitors.Count;
    }

    public VisitorNavigation GetFirst(){
        VisitorNavigation first = waintingVisitors.Dequeue();
        if(ContainsVisitor()){
            waintingVisitors.Peek().setInWaitingLine(null);
        }
        else {
            last = null;
            transform.parent = attraction.gameObject.transform;
            transform.position = attraction.poi_entry.position;
        }
        return first;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out VisitorNavigation visitor))
        {
            if(visitor.currentStatus == VisitorNavigation.status.Walk){
                hit.enabled = false;
                visitor.setStatus(VisitorNavigation.status.Wait);

                if(!attraction.isFull()){
                    Debug.Log("Peut entrer");
                    //visitor.canEnter();
                    //visitor.setStatus(VisitorNavigation.status.Visit);
                    attraction.VisitorEnter(visitor);
                }
                else {
                    waintingVisitors.Enqueue(visitor);
                    if(last != null){
                        visitor.setInWaitingLine(last);
                    }
                    last = visitor;
                    transform.position = visitor.transform.position;
                    transform.parent = visitor.transform;
                }
                hit.enabled = true;
            }
        }
        Debug.Log(getNumberVisitor());
    }
}
