using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;


[RequireComponent(typeof(Camera))]
public class MultiplePlayerCamera : MonoBehaviour
{
    public List<Transform> targets;

    [SerializeField]
    private float pixelsPerUnit = 32;
    [SerializeField] // Uncomment if you want to watch scaling in the editor
    private float pixelsPerUnitScale = 1;

    private int screenHeight;

    private float cameraSize;

    public Vector3 offset;
    public float smoothTime = .5f;

    public float fourPlayerSize;
    public float threePlayerSize;
    public float twoPlayerSize;

    public float maxZoom = 40f;
    public float minZoom = 10f;
    public float zoomLimiter = 50f;

    public bool fourPlayers = false;
    public bool threePlayers = false;
    public bool twoPlayers = false;

    public CinemachineVirtualCamera virtualCamera;

    private Vector3 velocity;
    private Camera cam;
    private GameObject[] players;
    public bool lerp = false;
    PixelPerfectCamera perfectCamera;
    

    void Start()
    {
        cam = GetComponent<Camera>();
        perfectCamera = GetComponent<PixelPerfectCamera>();
    }

    void FixedUpdate()
    {
        if (targets.Count == 0)
            return;
        if (screenHeight != Screen.height)
        {
            screenHeight = Screen.height;
        }
        Move();
        Zoom();
        if (targets.Count == 4)
        {
            fourPlayers = true;
            threePlayers = false;
            twoPlayers = false;
        }
        else if (targets.Count == 3)
        {
            threePlayers = true;
            twoPlayers = false;
            fourPlayers = false;
        }
        else if (targets.Count == 2)
        {
            threePlayers = false;
            fourPlayers = false;
            twoPlayers = true;
        }
    }

    void Zoom()
    {      
        if (fourPlayers)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, fourPlayerSize, Time.deltaTime);
        }
        else if (threePlayers)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, threePlayerSize, Time.deltaTime);
        }
        else if (twoPlayers)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, twoPlayerSize, Time.deltaTime);
        }
        int newZoom = (int)Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        //Debug.Log(newZoom);
        //virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, newZoom, Time.deltaTime);
        //UpdateCameraScale();
        //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
        if (lerp)
        {
            perfectCamera.assetsPPU = (int)Mathf.Lerp(perfectCamera.assetsPPU, newZoom, Time.deltaTime);

           
        }
        else if (!lerp)
        {
            perfectCamera.assetsPPU = newZoom;
            
        }
    }
    private void UpdateCameraScale()
    {
        // The magic formular from teh Unity Docs
       // cameraSize = (screenHeight / (pixelsPerUnitScale * pixelsPerUnit)) * 0.5f;
       // cam.orthographicSize = cameraSize;
    }
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
    private void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (targets.Count < players.Length)
            {
                targets.Add(player.transform);
            }
        }
    }

}