using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    CinemachineFreeLook vcam;
    public KeyCode aim;
    public float originalFieldofView = 70;
    public float zoomFieldofView = 20;
    public float zoomSpeed = 5;
    public Transform LookAtZoom;
    public Transform LookAt;

    public PlayerMovement move;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineFreeLook>();
        vcam.m_CommonLens = true;
        vcam.m_Lens.FieldOfView = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(aim))
        {
            vcam.LookAt = LookAtZoom;
            ZoomCameraIn();
            move.rotateToCam();
        }
        else
        {
            vcam.LookAt = LookAt;
            ZoomCameraOut();
        }
    }
    void ZoomCameraIn()
    {
        vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, zoomFieldofView, zoomSpeed * Time.deltaTime);
    }
    void ZoomCameraOut()
    {
        vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, originalFieldofView, zoomSpeed * Time.deltaTime);
    }
}
