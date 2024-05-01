using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    private Dictionary<Vector3, Sprite> directionToIdle;
    Vector3 lastDirection; // Per calcolare se il player si è mosso
    Vector3 idleOrientation; // Indicherà quale sprite di idle utilizzare
    //Ref
    [SerializeField] Transform targetMovePoint;
    [SerializeField] Transform holdPoint;
    [SerializeField] Sprite[] idleSprites;

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
        

        if (direction != Vector3.zero && GameManager.PlayerIsOnGrid)
        {
            if(GameManager.IsHoldingAnObject)
            {
                float dot = Vector3.Dot(transform.position, holdPoint.position);
                if (dot == 0.5f)
                {
                    anim.SetFloat("X", direction.x);
                    anim.SetFloat("Z", 0);
                }
                else if (dot == 1 || dot == -1)
                {
                    anim.SetFloat("X", 0);
                    anim.SetFloat("Z", direction.z);
                }
                lastDirection = direction;
                return;
            }

           
            anim.SetFloat("X", direction.x);
            anim.SetFloat("Z", direction.z);

        }
        else
        {
            if (lastDirection != Vector3.zero )
            {
                if (transform.position == targetMovePoint.position && !GameManager.IsHoldingAnObject)
                {
                    spriteRenderer.sprite = directionToIdle[CalculateIdle(holdPoint)];
                    anim.enabled = false;
                }
                else if(transform.position == targetMovePoint.position && GameManager.IsHoldingAnObject)
                {
                    spriteRenderer.sprite = directionToIdle[CalculateIdle(holdPoint)];
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

    Vector3 CalculateIdle( Transform actualHoldPoint)
    {
        if(actualHoldPoint.localPosition.x == 0.5) { idleOrientation = Vector3.right; }
        else if(actualHoldPoint.localPosition.x == -0.5) { idleOrientation = Vector3.left; }
        if(actualHoldPoint.localPosition.z == 0.5) { idleOrientation = Vector3.forward; }
        else if (actualHoldPoint.localPosition.z == -0.5) { idleOrientation = Vector3.back;}

        return idleOrientation;
    }
        

}
