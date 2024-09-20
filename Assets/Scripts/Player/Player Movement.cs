using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Dictionary<string, List<Sprite>> animations;

    private int currentFrame = 0;
    private float frameDelay = 0.1f;
    private float lastFrameTime;

    private Vector3 direction = Vector3.right;
    private string currentDirection = "right";

    private float speed = 4f;
    private Vector2 movement;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animations = new Dictionary<string, List<Sprite>>
        {
            { "up", AnimationLoader.LoadAnimation("Animations/Player/up") },
            { "down", AnimationLoader.LoadAnimation("Animations/Player/down") },
            { "left", AnimationLoader.LoadAnimation("Animations/Player/left") },
            { "right", AnimationLoader.LoadAnimation("Animations/Player/right") },
            { "static", AnimationLoader.LoadAnimation("Animations/Player/static") }
        };
    }

    void FixedUpdate()
    {
        Move();
        UpdateAnimation();
    }

    void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            if (movement.x < 0)
                currentDirection = "left";
            else if (movement.x > 0)
                currentDirection = "right";
            else if (movement.y > 0)
                currentDirection = "up";
            else if (movement.y < 0)
                currentDirection = "down";
        }
        else
        {
            currentDirection = "static";
        }
        transform.Translate(movement * speed * Time.fixedDeltaTime);
    }

    void UpdateAnimation()
    {
        if (Time.time - lastFrameTime >= frameDelay)
        {
            currentFrame++;
            if (currentFrame >= animations[currentDirection].Count)
            {
                currentFrame = 0;
            }

            spriteRenderer.sprite = animations[currentDirection][currentFrame];
            lastFrameTime = Time.time;
        }
    }

}

