using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class POI : MonoBehaviour
{
    public float visit_time;
    public int number_visitor;
    //private int current_visitor = 0;
    public Transform poi_entry;
    public Transform poi_exit;
    public Transform waiting_list_position;
    private Queue<VisitorNavigation> current_visitor = new Queue<VisitorNavigation>();
    [SerializeField] private WaitingLine line;

    void Start(){
    }

    public Vector3 getWaitingList()
    {
        return waiting_list_position.position;
    }

    public int getCurrentVisitor(){
        return current_visitor.Count;
    }

    /*public int addOneVisitor(){
        return current_visitor++;
    }

    public int removeOneVisitor(){
        return current_visitor--;
    }*/

    public bool isFull(){
        return current_visitor.Count == number_visitor;
    }

    public void VisitorEnter(VisitorNavigation visitor){
        visitor.setStatus(VisitorNavigation.status.Visit);
        visitor.gameObject.SetActive(false);
        current_visitor.Enqueue(visitor);
        StartCoroutine("AttractionVisit");
    }

    private IEnumerator AttractionVisit(){
        yield return new WaitForSeconds(visit_time);
        VisitorNavigation previous = current_visitor.Dequeue();
        previous.transform.position = new Vector3(poi_exit.position.x, previous.transform.position.y, poi_exit.position.z);
        previous.gameObject.SetActive(true);
        previous.setStatus(VisitorNavigation.status.Walk);
        previous.newDestination();

        if(line.ContainsVisitor()){
            VisitorEnter(line.GetFirst());
        }
    }
}
