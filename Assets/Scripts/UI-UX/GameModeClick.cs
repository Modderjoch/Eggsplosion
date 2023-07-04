using UnityEngine;
using UnityEngine.EventSystems;

public class GameModeClick : MonoBehaviour
{
    [SerializeField]
    private GameObject casualFFA;
    [SerializeField]
    private GameObject compFFA;

    public EventSystem eventSystem;
    private BaseEventData eventData;

    private void Awake()
    {
        eventData = new BaseEventData(eventSystem);
    }

    public void ClickCasual()
    {
        eventData.selectedObject = casualFFA;
    }

    public void ClickComp()
    {
        eventData.selectedObject = compFFA;
    }
}
