using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendAnimationSelector : MonoBehaviour
{
    [SerializeField]
    KeyCode forward;
    [SerializeField]
    KeyCode runForward;

    float velocity;
    float acceleration = 0.1f;
    float deceleration = 0.5f;
    int VelocityHash;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        VelocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(forward);
        bool runPressed = Input.GetKey(runForward);

        if (forwardPressed)
        {
            velocity += Time.deltaTime + acceleration;
        }

        if (!forwardPressed && velocity > 0)
        {
            velocity -= Time.deltaTime + deceleration;
        }

        animator.SetFloat(VelocityHash, velocity);
    }
}
