using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    [SerializeField] MenuHandler menuHandler;

    UI_Controls controls;
    UI_Controls.UIActions uiActions;

    public static UIInputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[Singleton] Trying to create another instance of singleton");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        controls = new UI_Controls();
        uiActions = controls.UI;

        uiActions.Navigate.performed += _ => menuHandler.DetectInputDevice();
        uiActions.Leaderboard.performed += _ => menuHandler.Leaderboard();
        uiActions.Eggsplanation.performed += _ => menuHandler.Eggsplanation();
        uiActions.Cancel.performed += _ => menuHandler.Cancel();
        uiActions.Options.performed += _ => menuHandler.Options();
        uiActions.AddRound.performed += _ => menuHandler.AddRound();
        uiActions.SubtractRound.performed += _ => menuHandler.SubtractRound();
        uiActions.NextBoard.performed += _ => menuHandler.NextBoard();
        uiActions.PreviousBoard.performed += _ => menuHandler.PreviousBoard();
    }

    private void Update()
    {
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }
}
