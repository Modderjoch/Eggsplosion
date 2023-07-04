using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private PlayerMovement mover;
    private Shooting shooter;
    private Aiming aimer;
    private GameModeSelection canceler;

    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Animator animator;

    private PlayerControls controls;

    private void Awake()
    {
        mover = GetComponent<PlayerMovement>();
        aimer = GetComponentInChildren<Aiming>();
        shooter = GetComponent<Shooting>();
        controls = new PlayerControls();
        animator = GetComponentInChildren<Animator>();
    }

    public void InitializePlayer(PlayerConfiguration config)
    {
        playerConfig = config;
        sprite.sprite = config.playerSprite;
        animator.runtimeAnimatorController = config.animatorOverrideController;
        config.playerInput.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.Player.Move.name)
        {
            OnMove(obj);
        }
        else if (obj.action.name == controls.Player.Fire1.name)
        {
            OnShoot(obj);
        }
        else if(obj.action.name == controls.Player.Look.name)
        {
            OnAim(obj);
        }
        else if (obj.action.name == controls.Player.Dashing.name)
        {
            OnDash(obj);
        }
        else if (obj.action.name == controls.Player.Dancing.name)
        {
            OnDance(obj);
        }

        else if (obj.action.name == controls.Player.Dancing1.name)
        {
            OnDance1(obj);
        }

        else if (obj.action.name == controls.Player.Dancing2.name)
        {
            OnDance2(obj);
        }
    }
    public void OnDance(CallbackContext context)
    {
        mover.DanceInput(context);
    }
    public void OnDance1(CallbackContext context)
    {
        mover.NaeNaeInput(context);
    }
    public void OnDance2(CallbackContext context)
    {
        mover.FlagDanceInput(context);
    }
    public void OnMove(CallbackContext context)
    {
        
        if (mover != null)
            mover.SetInputVector(context.ReadValue<Vector2>());
    }
    public void OnShoot(CallbackContext context)
    {
        if (shooter != null)
            shooter.Fire1(context);
    }
    public void OnAim(CallbackContext context)
    {
        if (aimer != null)
            aimer.SetInputVector(context.ReadValue<Vector2>());
        if (context.canceled)
        {
            aimer.SetAimingBool(true);
        }
        else if (context.started)
        {
            aimer.SetAimingBool(false);
        }
    }

    public void OnDash(CallbackContext context)
    {
        if (mover != null)
        {
            mover.Dashing(context);
        }
    }
}