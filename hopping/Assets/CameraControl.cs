using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Vector2 screenSize;

    [SerializeField] private float cameraMinimumDistance;

    [SerializeField] private float percent;

    private Sequence shrinkSequence;

    private bool isShrink = false;
    // Start is called before the first frame update
    void Start()
    {
        screenSize = GetScreenBottomRight(1) - GetScreenTopLeft(1);
        Debug.Log(screenSize);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if(Player.instance.transform.position)
        if (Input.GetMouseButton(0))
        {
            ComparePosition();    
        }
        CheckShrink();
        
    }

    private void Update()
    {
        CheckShrink();
    }


    private void OldComparePosition()
    {
        Vector2 PlayerPos = Player.instance.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > GetScreenBottomRight(percent).x)
        {
            transform.position=new Vector3(Mathf.Lerp(transform.position.x,transform.position.x+(mousePos.x-GetScreenBottomRight(percent).x),0.1f),transform.position.y,transform.position.z);
        }
        else if (mousePos.x < GetScreenTopLeft(percent).x)
        {
            transform.position=new Vector3(Mathf.Lerp(transform.position.x,transform.position.x-(GetScreenTopLeft(percent).x-mousePos.x),0.1f),transform.position.y,transform.position.z);
        }

        if (mousePos.y > GetScreenBottomRight(percent).y)
        {
            
        }
        else if (mousePos.y < GetScreenTopLeft(percent).y)
        {
            
        }
    }
    
    private void ComparePosition()
    {
        Vector2 PlayerPos = Player.instance.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(transform.position, mousePos) > cameraMinimumDistance)
        {
            float rot = GetAngle(transform.position, mousePos);
            Vector2 mov = new Vector2(
                Mathf.Cos(Mathf.Deg2Rad*rot),Mathf.Sin(Mathf.Deg2Rad*rot) 
                )*(Vector2.Distance(transform.position, mousePos)-cameraMinimumDistance);
            transform.position += (Vector3)mov*Time.deltaTime;
        }
    }
    
    private Vector3 GetScreenTopLeft(float percent) {
        // 画面の左上を取得
        Vector3 topLeft = Camera.main.ScreenToWorldPoint (Vector3.zero);
        topLeft += (1f - percent) * (Vector3)screenSize;
        // 上下反転させる
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }

    private Vector3 GetScreenBottomRight(float percent) {
        // 画面の右下を取得
        Vector3 bottomRight = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0.0f));
        bottomRight -= (1f - percent) * (Vector3)screenSize;
        // 上下反転させる
        bottomRight.Scale(new Vector3(1f, -1f, 1f));
        return bottomRight;
    }
    
    float GetAngle(Vector2 start,Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2 (dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;
		
        return degree;
    }

    private void CheckShrink()
    {
        if (!isShrink && Input.GetKeyDown(KeyCode.LeftControl))
        {
            shrinkSequence.Pause();
            isShrink = true;

            shrinkSequence = DOTween.Sequence()
                .OnPlay(() =>
                {
                    Debug.Log("Start");
                })
                .Append(
                    DOTween.To(
                        () => GetComponent<Camera>().orthographicSize,          // 何を対象にするのか
                        num => GetComponent<Camera>().orthographicSize = num,   // 値の更新
                        10,                  // 最終的な値
                        0.1f                  // アニメーション時間
                    )
                ).Play();
        }
        
        if (isShrink && Input.GetKeyUp(KeyCode.LeftControl))
        {
            shrinkSequence.Pause();
            isShrink = false;

            shrinkSequence = DOTween.Sequence()
                .OnPlay(() =>
                {
                    Debug.Log("End");
                })
                .Append(
                    DOTween.To(
                        () => GetComponent<Camera>().orthographicSize,          // 何を対象にするのか
                        num => GetComponent<Camera>().orthographicSize = num,   // 値の更新
                        5,                  // 最終的な値
                        0.1f                  // アニメーション時間
                    )
                ).Play();
        }
    }

    private void ShrinkOn()
    {
        
    }

    private void ShrinkOff()
    {
        
    }
}
