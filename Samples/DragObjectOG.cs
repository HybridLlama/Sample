using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectOG : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    void OnMouseDown()
    {
        mZCoord =  CameraScripts.Instance.CurrentCamera.WorldToScreenPoint(
            gameObject.transform.position).z;

        //Stores offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        //Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        //z coordinate of game object on screen
        mousePoint.z = mZCoord;

        //Convert it to world points
        return CameraScripts.Instance.CurrentCamera.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        Vector3 targetPos = GetMouseAsWorldPoint() + mOffset;
        float yValue = targetPos.y;
        yValue = Mathf.Clamp(yValue, 0, Mathf.Infinity);
        targetPos = new Vector3(targetPos.x,yValue,targetPos.z);
        transform.position = targetPos;
    }
}

