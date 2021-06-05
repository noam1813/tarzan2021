using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance;

    private Vector2 centerPosition;

    private LineRenderer _lineRenderer;

    [SerializeField] private float acceleration;
    [SerializeField] private float maximumSpeed;
    [SerializeField] private Vector2 currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Accel();
        Move();
        
        SetCenterPoint();
        DrawLine();
       
    }


    private void Accel()
    {
        Vector2 axis = new Vector2();

        axis.x = transform.position.x < centerPosition.x ? 1 : -1;
        axis.y = transform.position.y < centerPosition.y ? 1 : -1;

        currentSpeed.x += acceleration * Time.deltaTime * axis.x;
        currentSpeed.y += acceleration * Time.deltaTime * axis.y;
    }


    private void Move()
    {
        transform.position += (Vector3)currentSpeed*Time.deltaTime;
    }


    private void SetCenterPoint()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePos = Input.mousePosition;
            centerPosition = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log(centerPosition);
        }
    }

    private void DrawLine()
    {
        Vector3[] positions = new Vector3[]
        {
            centerPosition,
            transform.position
        };
        _lineRenderer.SetPositions(positions);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            currentSpeed *= -1f/2f;
        }
        
    }
}
