using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    private ParticleSystem destroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        destroyEffect = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (destroyEffect)
        {
            if (!destroyEffect.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
