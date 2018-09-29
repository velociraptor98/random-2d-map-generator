using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public float speed = 5.0f;
    private Vector3 StartPosition;
    private bool IsMoving = false;
    private void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartPosition = Input.mousePosition;
            IsMoving = true;
        }
        if(Input.GetMouseButtonUp(0)&& IsMoving)
        {
            IsMoving = false;
        }
        if(IsMoving)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - StartPosition);
            Vector3 move = new Vector3(pos.x * speed, pos.y * speed, 0);
            this.transform.Translate(move);
        }
    }

}
