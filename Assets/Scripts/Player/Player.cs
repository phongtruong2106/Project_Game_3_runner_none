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
   public float maxMaxHoldJumpTime = 0.4f;
   public float holdJumpTimer = 0.0f;

   public float jumpGroundThreshold = 1f;

   public bool isDead = false;


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

        if(isDead)
        {
            return;
        }

        if(pos.y < -20)
        {
            isDead = true; 
        }

        if(!isGrounded)
        {
            if(isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if(holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
                Debug.Log(holdJumpTimer);
            }
            else
            {
                holdJumpTimer -= Time.fixedDeltaTime; // Giảm giá trị của holdJumpTimer sau mỗi lần nhảy
                if (holdJumpTimer < 0)
                {
                    holdJumpTimer = 0;
                }
            }

            pos.y += velocity.y * Time.fixedDeltaTime;
            if(!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if(hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if(ground != null)
                {
                    if(pos.y >= ground.groundHeight)
                    {
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;
                        velocity.y = 0;
                        isGrounded = true;
                    }
                  
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

            Vector2 wallOrigin = new Vector2(pos.x, pos.y);
            RaycastHit2D wallhit  = Physics2D.Raycast(wallOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime);
            if(wallhit.collider != null)
            {
                Ground ground = wallhit.collider.GetComponent<Ground>();
                if(ground != null)
                {
                     if(pos.y < ground.groundHeight)
                     {
                        velocity.x = 0; 
                     }
                }
                
            }
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if(isGrounded)
        {

            float velocityRatio = velocity.x / maxVelocity;
           
            acceleration = maxAcceleration * (1 - velocityRatio);
            maxHoldJumpTime = maxMaxHoldJumpTime * velocityRatio;

            velocity.x += acceleration * Time.fixedDeltaTime;
            if(velocity.x >= maxVelocity)
            {
                velocity.x = maxVelocity;
            }

            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if(hit2D.collider == null)
            {
                isGrounded = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
        }

        Vector2 obsOrigin = new Vector2(pos.x , pos.y);
        RaycastHit2D obstHitX =Physics2D.Raycast(obsOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime);
        if(obstHitX.collider != null)
        {
            Obstacle obstacle = obstHitX.collider.GetComponent<Obstacle>();
            if(obstacle != null)
            {
                hitObstacle(obstacle);
            }
        }
        RaycastHit2D obstHitY =Physics2D.Raycast(obsOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime);
        if(obstHitY.collider != null)
        {
            Obstacle obstacle = obstHitY.collider.GetComponent<Obstacle>();
            if(obstacle != null)
            {
                hitObstacle(obstacle);
            }
        }
        transform.position = pos;
   }

    private void hitObstacle(Obstacle obstacle)
    {
        Destroy(obstacle.gameObject);
        velocity.x *= 0.7f;
    }

}
