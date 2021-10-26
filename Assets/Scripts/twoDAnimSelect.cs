using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDAnimSelect : MonoBehaviour
{
    [SerializeField]
    KeyCode forward;
    [SerializeField]
    KeyCode back;
    [SerializeField]
    KeyCode left;
    [SerializeField]
    KeyCode right;
    [SerializeField]
    KeyCode run;

    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void changeVelocity (bool forwardPressed, bool backPressed, bool leftPressed, bool rightPressed, bool runPressed, float currMaxVelocity)
    {
        //press forward => increase velocity z
        if (forwardPressed && velocityZ < currMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        //press back => decrease velocity z
        if (backPressed && velocityZ > -currMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }

        //press left => increase velocity x
        if (leftPressed && velocityX > -currMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        //increase velocity in right direction
        if (rightPressed && velocityX < currMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        //decrease velocity z
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        //increase velocity z if back isn't pressed and velocity z > 0
        if (!backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }

        //increase velocity x if left isn't pressed and velocity x < 0
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        //decrease velocity x if right isn't pressed and velocity x > 0
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    void lockOrResetVelocity (bool forwardPressed, bool backPressed, bool leftPressed, bool rightPressed, bool runPressed, float currMaxVelocity)
    {
        //reset velocity z
        //if (!forwardPressed && velocityZ < 0.0f)
        //{
        //    velocityZ = 0.0f;
        //}

        if (!backPressed && !forwardPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f))
        {
            velocityZ = 0.0f;
        }

        //reset velocity x
        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }

        //lock forward
        if (forwardPressed && runPressed && velocityZ > currMaxVelocity)
        {
            velocityZ = currMaxVelocity;
        }
        //decelerate to max walk velocity
        else if (forwardPressed && velocityZ > currMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;

            //round to the currMaxVelocity if within offset
            if (velocityZ > currMaxVelocity && velocityZ < (currMaxVelocity + 0.05f))
            {
                velocityZ = currMaxVelocity;
            }
        }
        // round to currMaxVellicty if within offset
        else if (forwardPressed && velocityZ < currMaxVelocity && velocityZ > (currMaxVelocity - 0.05f))
        {
            velocityZ = currMaxVelocity;
        }

        //lock back
        if (backPressed && runPressed && velocityX < -currMaxVelocity)
        {
            velocityZ = -currMaxVelocity;
        }
        //decelerate to max walk velocity
        else if (backPressed && velocityZ < -currMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;

            //round to the currMaxVelocity if within offset
            if (velocityZ < -currMaxVelocity && velocityZ > (-currMaxVelocity - 0.05f))
            {
                velocityZ = -currMaxVelocity;
            }
        }
        // round to currMaxVellicty if within offset
        else if (backPressed && velocityZ > -currMaxVelocity && velocityZ < (-currMaxVelocity + 0.05f))
        {
            velocityZ = -currMaxVelocity;
        }

        //lock left
        if (leftPressed && runPressed && velocityX < -currMaxVelocity)
        {
            velocityX = -currMaxVelocity;
        }
        //decelerate to max walk velocity
        else if (leftPressed && velocityX < -currMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;

            //round to the currMaxVelocity if within offset
            if (velocityX < -currMaxVelocity && velocityX > (-currMaxVelocity - 0.05f))
            {
                velocityX = -currMaxVelocity;
            }
        }
        // round to currMaxVellicty if within offset
        else if (leftPressed && velocityX > -currMaxVelocity && velocityX < (-currMaxVelocity + 0.05f))
        {
            velocityX = -currMaxVelocity;
        }

        //lock right
        if (rightPressed && runPressed && velocityX > currMaxVelocity)
        {
            velocityX = currMaxVelocity;
        }
        //decelerate to max walk velocity
        else if (rightPressed && velocityX > currMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;

            //round to the currMaxVelocity if within offset
            if (velocityX > currMaxVelocity && velocityX < (currMaxVelocity + 0.05f))
            {
                velocityX = currMaxVelocity;
            }
        }
        // round to currMaxVellicty if within offset
        else if (rightPressed && velocityX < currMaxVelocity && velocityX > (currMaxVelocity - 0.05f))
        {
            velocityX = currMaxVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(forward);
        bool backPressed = Input.GetKey(back);
        bool leftPressed = Input.GetKey(left);
        bool rightPressed = Input.GetKey(right);
        bool runPressed = Input.GetKey(run);

        //set current max velocity
        float currMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

        //handles changes in velocity
        changeVelocity(forwardPressed, backPressed, leftPressed, rightPressed, runPressed, currMaxVelocity);
        lockOrResetVelocity(forwardPressed, backPressed, leftPressed, rightPressed, runPressed, currMaxVelocity);

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);
    }
}
