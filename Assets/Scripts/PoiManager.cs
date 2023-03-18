using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiManager : MonoBehaviour
{
    public static PoiManager Instance { get; private set; }
    public List<POI> pois = new List<POI>();

    public void Awake(){
        if(Instance != null){
            Debug.Log("Error");
        }
        Instance = this;
    }
}
