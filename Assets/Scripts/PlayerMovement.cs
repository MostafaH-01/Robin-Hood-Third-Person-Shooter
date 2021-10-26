using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Regular Movement and Camera Controls
    public CharacterController controller;
    public Transform cam;
    

    public float walkingSpeed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Gravity
    public float gravity = -9.81f;
    Vector3 GravityVelocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    void Update()
    {
        float speed;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(Input.GetKey("left shift")){
            speed = 2*walkingSpeed;
        }else{
            speed = walkingSpeed;
        }

        if(direction.magnitude >= 0.1f){

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && GravityVelocity.y < 0){
            GravityVelocity.y = -2f;
        }

        GravityVelocity.y += gravity * Time.deltaTime;
        controller.Move(GravityVelocity * Time.deltaTime);
    }

    public void rotateToCam()
    {
        //transform.Rotate(0, Camera.main.transform.rotation.y * turnSmoothTime * Time.deltaTime, 0);
    }
}
