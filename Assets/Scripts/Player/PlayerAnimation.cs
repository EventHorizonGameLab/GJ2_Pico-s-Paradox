using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    [SerializeField] Transform targetMovePoint;
    [SerializeField] Sprite[] idleSprites;
    private Dictionary<Vector3, Sprite> directionToIdle;
    Vector3 lastDirection;

    enum SpriteIndex
    {
        FORWARD, BACKWARD, LEFT, RIGHT
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        InitializeDirectionToSpriteMap();
    }

    private void Start()
    {
        anim.enabled = false;
        spriteRenderer.sprite = idleSprites[0];
    }

    private void InitializeDirectionToSpriteMap()
    {
        directionToIdle = new Dictionary<Vector3, Sprite>
    {
        { Vector3.forward, idleSprites[(int)SpriteIndex.FORWARD] },
        { Vector3.back, idleSprites[(int)SpriteIndex.BACKWARD] },
        { Vector3.left, idleSprites[(int)SpriteIndex.LEFT] },
        { Vector3.right, idleSprites[(int)SpriteIndex.RIGHT] }
    };


    }

    void HandleSpriteAnimations(Vector3 direction)
    {
        if (direction != Vector3.zero )
        {
            anim.SetFloat("X", direction.x);
            anim.SetFloat("Z", direction.z);

        }
        else
        {
            if (lastDirection != Vector3.zero)
            {
                if (transform.position == targetMovePoint.position)
                {
                    spriteRenderer.sprite = directionToIdle[lastDirection];
                    anim.enabled = false;
                }
            }
        }
    }

    private void Update()
    {
        if(InputManager.IsMoving(out Vector3 direction))
        {
            lastDirection = direction;
            HandleSpriteAnimations(direction);
            anim.enabled = true;
        }
        else
        {
            HandleSpriteAnimations(Vector3.zero);
            
        }
    }

}
