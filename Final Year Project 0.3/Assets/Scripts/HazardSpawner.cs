using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    public GameObject hazard;
    public GameObject hazardWarning;

    public float SpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTimer = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if(SpawnTimer > 0.1 && SpawnTimer < 0.5)
        {
            hazardWarning.SetActive(true);


        }
        else
        {
            hazardWarning.SetActive(false);

        }
        if (SpawnTimer <= 0)
        {
            Instantiate(hazard, transform.position, transform.rotation);
            SpawnTimer = 2f;

        }
    }

}
