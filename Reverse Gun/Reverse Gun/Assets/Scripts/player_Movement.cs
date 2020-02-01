using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Movement : MonoBehaviour
{
    public int jump;
    public CharacterController controller;
    bool isJumping = false;
    public float speed = 5f;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpkey;

    private void JumpInput()
    {
        if(Input.GetKeyDown(jumpkey) && !isJumping)
        {
            jump = 0;
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {
        float timeInAir = 0.0f;

        do
        {
            jump++;
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            controller.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (controller.isGrounded == false && controller.collisionFlags != CollisionFlags.Above);

        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        JumpInput();
        Vector3 move = transform.right * x + transform.forward * z;
        controller.SimpleMove(Vector3.ClampMagnitude(move, 1) * speed);
    }
}
