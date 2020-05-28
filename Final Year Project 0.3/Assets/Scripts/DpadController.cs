using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DpadController : MonoBehaviour
{
    public bool IsLeft, IsRight;
    private float _LastX;

    public static DpadController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("XboxDpadX");

        IsLeft = false;
        IsRight = false;

        if (_LastX != x)
        {
            if (x == -1)
                IsLeft = true;
            else if (x == 1)
                IsRight = true;
        }

        _LastX = x;

    }
}
