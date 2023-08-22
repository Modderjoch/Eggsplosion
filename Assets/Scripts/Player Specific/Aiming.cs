using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Aiming : MonoBehaviour
{
    public Transform player;
    public float radius;
    public Animator anim;

    private Transform pivot;
    private float horizontal;
    private float vertical;
    private Vector3 lookDir;
    private Vector2 inputVector;

    private float rotateSpeed;
    private float lastAngle;

    private bool aimStopped = false;
    private PlayerStats playerStats;

    void Start()
    {
        SetupAiming();
        playerStats = GetComponentInParent<PlayerStats>();
    }

    void Update()
    {
        HandleAiming();
        anim.SetFloat("HorizontalAim", lookDir.x);
    }

    public void Aim(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;

        lookDir.x = horizontal;
        lookDir.y = vertical;
       
        if (context.canceled)
        {
            aimStopped = true;
            if (playerStats.controlscheme != "Gamepad")
            {
                Debug.Log("stoped");
            }
        }
        else if (horizontal > 0 || vertical > 0)
        {
            aimStopped = false;
        }
        
        
    }
    void SetupAiming()
    {
        pivot = player.transform;
        transform.parent = pivot;
        transform.position += Vector3.up * radius;
        //radius = pivot.localScale.x / 4;
    }
    void HandleAiming()
    {
        float angle = 0;
        float speed = 10;
        if (aimStopped == false)
        {
            
            if (playerStats.controlscheme == "Gamepad")
            {
                lookDir = lookDir.normalized;
                angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                pivot.position = player.position;
                pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            }
            else //mouse inputs
            {
                Vector2 dir = Camera.main.ScreenToWorldPoint(lookDir) - pivot.position;
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);
                pivot.rotation = Quaternion.Slerp(pivot.rotation, rotation, speed * Time.deltaTime);
                
            }
            lastAngle = angle;
           
        }
        if (aimStopped)
        {
            //Debug.Log("Aiming Stopped");
            pivot.rotation = Quaternion.AngleAxis(lastAngle - 90, Vector3.forward);
        }
    }

    public void SetInputVector(Vector2 vector)
    {
        inputVector = vector;

    }
    public void SetAimingBool(bool aiming)
    {
        aimStopped = aiming;
    }
}
