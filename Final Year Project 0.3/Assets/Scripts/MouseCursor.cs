using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{

    public float horizontal = 0;
    public float vertical = 0;

    public float Speed;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;


    }

    // Update is called once per frame
    void Update()
    {
        //print(gameObject.transform.position);

        horizontal =  Input.GetAxisRaw("HORIZONTAL_RIGHT_ANALOGUE") * Time.deltaTime;
        vertical = Input.GetAxisRaw("VERTICAL_RIGHT_ANALOGUE") * Time.deltaTime;

        transform.position += new Vector3(horizontal * Speed, vertical * Speed, 0);


    }

    private void FixedUpdate()
    {
        
    }
}
