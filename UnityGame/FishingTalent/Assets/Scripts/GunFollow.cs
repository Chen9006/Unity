using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollow : MonoBehaviour
{

    public RectTransform UGUICanvas;
    public Camera mainCamera;

    void Update()
    {
        Vector3 mousePosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(UGUICanvas,(Vector2)Input.mousePosition,mainCamera, out mousePosition);
        float z;
        if(mousePosition.x>transform.position.x)
        {
            z = -Vector3.Angle(Vector3.up,mousePosition-transform.position);
        }
        else
        {
            z = Vector3.Angle(Vector3.up, mousePosition - transform.position);
        }

        transform.localRotation = Quaternion.Euler(0, 0, z);
    }
}
