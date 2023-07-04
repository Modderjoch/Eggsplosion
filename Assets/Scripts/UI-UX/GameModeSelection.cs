using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameModeSelection : MonoBehaviour, ISelectHandler, IDeselectHandler, ICancelHandler
{
    public string level;
    public GameObject ButtonGameObject;
    public GameObject selectedObject;
    [SerializeField]
    private GameObject extendCasual;
    [SerializeField]
    private GameObject extendComp;
    [SerializeField]
    private GameObject backButton;
    [SerializeField]
    private GameObject casualFFA;
    [SerializeField]
    private GameObject compFFA;

    [SerializeField]
    private GameObject CasualButtons;
    [SerializeField]
    private GameObject CompButtons;
    [SerializeField]
    private GameObject infoTextCasual;
    [SerializeField]
    private GameObject infoTextComp;
    [SerializeField]
    private GameObject casual;
    [SerializeField]
    private GameObject competitive;

    public void OnSelect(BaseEventData eventData)
    {
        FindObjectOfType<AudioManager>().Play("MenuHover");

        //Debug.Log(this.ButtonGameObject.name + " was selected");

        if (eventData.selectedObject.gameObject.name == "Casual")
        {
            CompButtons.SetActive(false);
            CasualButtons.SetActive(false);
            infoTextCasual.SetActive(true);
            infoTextComp.SetActive(false);
        }

        if (eventData.selectedObject.gameObject.name == "Competitive")
        {
            CasualButtons.SetActive(false);
            CompButtons.SetActive(false);
            infoTextCasual.SetActive(false);
            infoTextComp.SetActive(true);
        }

        if (eventData.selectedObject.gameObject.name == "BackButton")
        {
            CasualButtons.SetActive(false);
            CompButtons.SetActive(false);
            infoTextCasual.SetActive(false);
            infoTextComp.SetActive(false);
        }

        if (eventData.selectedObject.name == "FreeForAllCasual" || eventData.selectedObject.name == "2vs2Casual")
        {
            extendCasual.SetActive(true);
        }
        else
        {
            extendCasual.SetActive(false);
        }

        if (eventData.selectedObject.name == "FreeForAllComp" || eventData.selectedObject.name == "2vs2Comp")
        {
            extendComp.SetActive(true);
        }
        else
        {
            extendComp.SetActive(false);
        }
    }

    public void OnCancel(BaseEventData eventData)
    {
        eventData.selectedObject = backButton;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //Debug.Log(this.ButtonGameObject.name + " was deselected");
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene(level);
        FindObjectOfType<AudioManager>().Play("MenuClick");
    }
}
