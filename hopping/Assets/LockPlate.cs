using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPlate : MonoBehaviour
{
    [SerializeField] private string target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(target))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
