using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private bool isHop = false;
    public float power;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }

    private void FixedUpdate()
    {
        Controller();
    }

    void Controller()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isHop)
        {
            _animator.Play("ON");
            isHop = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) && isHop)
        {
            _animator.Play("OFF");
            isHop = false;
        }
    }
}
