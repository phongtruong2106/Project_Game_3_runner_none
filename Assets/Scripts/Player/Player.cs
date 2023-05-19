using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public float gravity; //luc hap dan (tuy chinh do cao cua nhan vat khi jump)
   public Vector2 velocity; //toc do nhan vat
   public float maxVelocity = 100f;
   public float distance = 0; //bien dem 
   public float maxAcceleration = 10f;
   public float acceleration = 10f;

   public float jumpVelocity = 20f; // toc do jump c
   public float groundHeight = 10f; //dp cao cua 1 khoi
   public bool isGrounded = false; //kiem tra thuc the

   public bool isHoldingJump = false;
   public float maxHoldJumpTime = 0.4f;
   public float holdJumpTimer = 0.0f;

   public float jumpGroundThreshold = 1f;


   private void Update()
    {

        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        if(isGrounded || groundDistance < jumpGroundThreshold)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded= false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
   }


   private void FixedUpdate() 
   {
        Vector2 pos = transform.position;//thay doi toa do nhan vat
        if(!isGrounded)
        {
            if(isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if(holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            pos.y += velocity.y * Time.fixedDeltaTime;
            if(!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }
            if(pos.y <= groundHeight)
            {
                pos.y = groundHeight; //toa do nhan vat  bang voi do cao ground da duoc dat truoc doa
                isGrounded = true;
                holdJumpTimer = 0; //khi holdjUMPtime dat den nguong maxHoldTimeJump => se tro ve 0
            }
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if(isGrounded)
        {

            float velocityRatio = velocity.x / maxVelocity;
           
            acceleration = maxAcceleration * (1 - velocityRatio);
            velocity.x += acceleration * Time.fixedDeltaTime;
            if(velocity.x >= maxVelocity)
            {
                velocity.x = maxVelocity;
            }
        }
        transform.position = pos;
   }



}
