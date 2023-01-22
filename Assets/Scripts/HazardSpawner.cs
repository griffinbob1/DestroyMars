using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject meteor;

    [SerializeField]
    private float meteorCount;
    //private float totalPositions = 32f;
    List<float> usedPositions = new List<float>();

    void Start()
    {
        //Hides the square, the square is there so I can actually choose where to place the meteors
        this.gameObject.SetActive(false);

        //Random number so it has something to check
        usedPositions.Add(43982374);

        for(int i = 0; i < meteorCount; i++)
        {
            SpawnMeteor();
        }
    }

    private void SpawnMeteor()
    {
        int randomPosition = Random.Range(-4, 4);
        
        if (usedPositions.Contains(randomPosition)){
            SpawnMeteor();
        }else{
            usedPositions.Add(randomPosition);
            GameObject tempObj1;
            tempObj1 = Instantiate(meteor) as GameObject;
            tempObj1.transform.position = (new Vector3(randomPosition * 2, transform.position.y, transform.position.z));
        }
    }
}
