using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setAiming : MonoBehaviour
{
    public KeyCode aim;
    public KeyCode fire;

    [Header("Aiming Settings")]
    RaycastHit hit;
    public LayerMask aimLayers;
    Ray ray;

    bool isAiming;

    bool hitDetected;

    public Bow bowScript;
    public FollowCamRotate rotate;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isAiming = Input.GetKey(aim);
        if (bowScript.bowSettings.arrowCount < 1)
            isAiming = false;

        if (isAiming)
        {
            animator.SetBool("isAiming", true);
            Aim();

            RotateCharacterSpine();

            bowScript.EquipBow();
            if (bowScript.bowSettings.arrowCount > 0)
            {
                animator.SetBool("pullString", Input.GetKey(fire));
                if (Input.GetKey(fire))
                    bowScript.PullString();
            }

            if (Input.GetKeyUp(fire))
            {
                bowScript.ReleaseString();
                animator.SetTrigger("fire");
                if (hitDetected)
                {
                    bowScript.Fire(hit.point);
                }
                else
                {
                    bowScript.Fire(ray.GetPoint(300f));
                }
            }
        }
        else
        {
            animator.SetBool("isAiming", false);
            bowScript.UnEquipBow();
            bowScript.RemoveCrosshair();
            bowScript.DisableArrow();
            bowScript.ReleaseString();
        }
    }
    
    void Aim()
    {
        Vector3 camPosition = Camera.main.transform.position;
        Vector3 dir = Camera.main.transform.forward;

        ray = new Ray(camPosition, dir);
        if (Physics.Raycast(ray, out hit, 500f, aimLayers))
        {
            hitDetected = true;
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            bowScript.ShowCrosshair(hit.point);
        }
        else
        {
            hitDetected = false;
            bowScript.RemoveCrosshair();
        }
    }
    void RotateCharacterSpine()
    {
        rotate.RotateCharacterSpine(ray);
    }
}
