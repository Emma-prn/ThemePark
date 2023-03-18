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
    
    //public void addPrefab(prefabNumber, prefab, terrainSize)

    // Start is called before the first frame update
    // Create fonction for position Navemesh
    void Start()
    {
        var terrain = Terrain.activeTerrain;
        terrainSize = terrain.terrainData.size;
        /*for(int i = 0; i < GuestNumber; i++){
            Vector3 NewPos = new Vector3(Random.Range(0,terrainSize.x), 50, Random.Range(0,terrainSize.z));
            Vector3 GuestPosition = NewPos;
            Vector3 PositionInNavMesh;
            if(getRandomPosition(GuestPosition, 10.0f, out PositionInNavMesh)){
                Instantiate(NewGuest, PositionInNavMesh, Quaternion.identity);
                TotalGuest++;
            }
        }*/

        for(int i = 0; i < CrowNumber; i++){
            Vector3 NewPos = new Vector3(Random.Range(2.6f,terrainSize.x), 50, Random.Range(0,terrainSize.z));
            Vector3 CrowPosition = NewPos;
            Vector3 PositionInNavMesh;
            if(getRandomPosition(CrowPosition, 10.0f, out PositionInNavMesh)){
                Instantiate(NewCrow, PositionInNavMesh, Quaternion.identity);
                TotalCrow++;
            }
        }
        GuestText.text = TotalGuest.ToString();
        CrowText.text = TotalCrow.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Add Guest
        if(Input.GetKeyDown(KeyCode.G)){
            for(int i = 0; i < 25; i++){
                Vector3 NewPos = new Vector3(Random.Range(2.6f,terrainSize.x), 50, Random.Range(0,terrainSize.z));
                Vector3 GuestPosition = NewPos;
                Vector3 PositionInNavMesh;
                if(getRandomPosition(GuestPosition, 10.0f, out PositionInNavMesh)){
                    Instantiate(NewGuest, PositionInNavMesh, Quaternion.identity);
                    TotalGuest++;
                }
            }
        }

        // Add Crow
        if(Input.GetKeyDown(KeyCode.C)){
            for(int i = 0; i < 25; i++){
                Vector3 NewPos = new Vector3(Random.Range(2.6f,terrainSize.x), 50, Random.Range(0,terrainSize.z));
                Vector3 CrowPosition = NewPos;
                Vector3 PositionInNavMesh;
                if(getRandomPosition(CrowPosition, 10.0f, out PositionInNavMesh)){
                    Instantiate(NewCrow, PositionInNavMesh, Quaternion.identity);
                    TotalCrow++;
                }
            }
        }

        GuestText.text = TotalGuest.ToString();
        CrowText.text = TotalCrow.ToString();
    }
}
