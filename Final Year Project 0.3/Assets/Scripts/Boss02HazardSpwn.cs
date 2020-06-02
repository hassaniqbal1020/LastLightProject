using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02HazardSpwn : MonoBehaviour
{
    public GameObject hazard;
    public GameObject hazardWarning;

    public float SpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTimer = 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer > 0.1 && SpawnTimer < 0.25)
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
            SpawnTimer = 0.9f;

        }
    }
}
