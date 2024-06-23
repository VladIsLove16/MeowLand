using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public bool Desktop = true;
    public bool UIToolkit = true;
    private void Awake()
    {
         if(instance == null)
            instance=this;
    }
    void Update()
    {
        if (Desktop)
        {
            
        }
        else
        {
            if (UIToolkit)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
                        if (hit)
                        {
                            Cat cat = hit.collider.GetComponent<Cat>();
                            if (cat != null)
                            {
                                cat.OnClick();
                            }
                        }
                    }
                }
            }
        }
    }
}
