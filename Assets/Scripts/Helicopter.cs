using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Helicopter : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    [SerializeField] private float moveSpeed;
    private Vector2 moveAmount;

    private int soldierCount;
    
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;

        soldierCount = 0;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveAmount = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector2 currentPosition = transform.position;

        currentPosition += moveSpeed * Time.deltaTime * moveAmount;

        Vector2 viewportPosition = mainCamera.WorldToViewportPoint(currentPosition);

        if (viewportPosition.x < 0.1f || viewportPosition.x > 0.9f)
        {
            currentPosition.x = transform.position.x;
        }

        if (viewportPosition.y < 0.1f || viewportPosition.y > 0.9f)
        {
            currentPosition.y = transform.position.y;
        }

        transform.position = currentPosition;
        
        if (moveAmount.x > 0.0f)
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y = 0.0f;
            transform.rotation = Quaternion.Euler(rotation);
        }
        else if (moveAmount.x < 0.0f)
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y = 180.0f;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soldier") && soldierCount < 3)
        {
            soldierCount++;
            Destroy(other.gameObject);
            if (soldierCount == 3)
            {
                anim.SetBool("helicopterFull", true);
            }
        }
    }
}
