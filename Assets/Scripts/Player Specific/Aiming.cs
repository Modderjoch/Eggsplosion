using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aiming : MonoBehaviour
{
    public Transform player;
    public float radius;
    public Animator anim;

    private Transform pivot;
    private float horizontal;
    private float vertical;
    private Vector3 position;
    private Vector2 inputVector;

    private float rotateSpeed;
    private float lastAngle;

    private bool aimStopped = false;

    void Start()
    {
        SetupAiming();
        
    }

    void Update()
    {
        HandleAiming();
        anim.SetFloat("HorizontalAim", position.x);
    }

    public void Aim(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
        if (context.canceled)
        {
            aimStopped = true;

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
        if (aimStopped == false)
        {
            position = inputVector;
            //Vector3 orbVector = Camera.main.WorldToScreenPoint(player.position);
            //orbVector =  position- orbVector;
            float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;

            pivot.position = player.position;
            pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            rotateSpeed = horizontal;
            //.RotateAround(player.transform.position, Vector3.up, rotateSpeed);
            lastAngle = angle;
            //Debug.Log(lastAngle);
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
