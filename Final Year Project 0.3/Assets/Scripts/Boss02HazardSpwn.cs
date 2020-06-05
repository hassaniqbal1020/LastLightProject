using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02HazardSpwn : MonoBehaviour
{
    public GameObject hazard;
    public GameObject hazardWarning;

    public float SpawnTimer;


    private void OnEnable()
    {
        SpawnTimer = 0.85f;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer > 0.05 && SpawnTimer < 0.15)
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
            SpawnTimer = 0.85f;

        }
    }
}
