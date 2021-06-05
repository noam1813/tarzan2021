using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    [SerializeField]private float speed;

    public bool isMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMove)
        {
            transform.eulerAngles += new Vector3(0f,0f,speed * Time.deltaTime);   
        }
    }
}
