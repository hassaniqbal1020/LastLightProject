using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlock : MonoBehaviour
{
    public GameObject enemyRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyRef.activeSelf)
        {
            StartCoroutine(ActivateWallBlock());

        }else if (!enemyRef.activeSelf)
        {
            gameObject.SetActive(false);

        }
    }

    IEnumerator ActivateWallBlock()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(true);
    }
}
