using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static bool MoblieTouchBeginCheck(GameObject g)
    {
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        //        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
        //        if (hit.collider != null && hit.collider.gameObject == g) return true;
        //    }
        //}
        foreach (var touch in Input.touches)
        {
            var ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject != null && hit.collider.gameObject == g) return true;
            }
        }
        return false;
    }
    public static bool MobileTouchingCheck(GameObject g)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                
                if (hit.collider != null && hit.collider.gameObject == g) return true;
            }
        }
        return false;
    }
    public static bool MoblieTouchEndCheck(GameObject g)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == g) return true;
            }
        }
        return false;
    }

}
