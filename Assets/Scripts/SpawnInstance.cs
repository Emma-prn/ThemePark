using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using UnityEngine.UI;

public class SpawnInstance : MonoBehaviour
{
    public GameObject NewGuest;
    public int GuestNumber;
    private int TotalGuest;
    public Text GuestText;

    public GameObject NewCrow;
    public int CrowNumber;
    private int TotalCrow;
    public Text CrowText;

    private Vector3 terrainSize;

    public bool getRandomPosition(Vector3 origin, float distance, out Vector3 result){
        for(int i = 0; i < 30; i++){
            Vector3 randomPosition = Random.insideUnitSphere * distance;
            randomPosition += origin;
            NavMeshHit navHit;
            if(NavMesh.SamplePosition(randomPosition, out navHit, distance, NavMesh.AllAreas)) {
                result = navHit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    
    public void addPrefab(int prefabNumber, GameObject prefab, Vector3 terrainSize, ref int totalToIncrement){
        for(int i = 0; i < prefabNumber; i++){
            Vector3 NewPos = new Vector3(Random.Range(0,terrainSize.x), 50, Random.Range(0,terrainSize.z));
            Vector3 PrefabPosition = NewPos;
            Vector3 PositionInNavMesh;
            if(getRandomPosition(PrefabPosition, 10.0f, out PositionInNavMesh)){
                Instantiate(prefab, PositionInNavMesh, Quaternion.identity);
                totalToIncrement++;
            }
        }
    }

    void Start()
    {
        var terrain = Terrain.activeTerrain;
        terrainSize = terrain.terrainData.size;
        
        addPrefab(GuestNumber, NewGuest, terrainSize, ref TotalGuest);
        addPrefab(CrowNumber, NewCrow, terrainSize, ref TotalCrow);

        GuestText.text = TotalGuest.ToString();
        CrowText.text = TotalCrow.ToString();
    }

    void Update()
    {
        // Add Guest
        if(Input.GetKeyDown(KeyCode.G)){
            addPrefab(25, NewGuest, terrainSize, ref TotalGuest);
        }

        // Add Crow
        if(Input.GetKeyDown(KeyCode.C)){
            addPrefab(25, NewCrow, terrainSize, ref TotalCrow);
        }

        GuestText.text = TotalGuest.ToString();
        CrowText.text = TotalCrow.ToString();
    }
}
