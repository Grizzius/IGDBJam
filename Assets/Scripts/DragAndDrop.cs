using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public Items item;

    GameMode gamemode;
    BoxCollider2D itemCollider;
    Vector3 mousePositionOffset;
    Vector3 dragStartPosition;
    SpriteRenderer spriteRenderer;
    bool isDragging;
    public bool inBagArea;

    bool justCreated = true;
    bool isBeingDestroyed = false;

    int colliderCount;

    private void Start()
    {
        gamemode = FindObjectOfType<GameMode>();
        itemCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.sprite;
        itemCollider.size = item.size;
    }

    private void Update()
    {
        if (isDragging)
        {
            DragItem();
        }
        
    }

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        if (isDragging)
        {
            EndDrag();
        }
        else
        {
            StartDrag();
        }
        
    }

    public void StartDrag()
    {
        isDragging = true;
        mousePositionOffset = transform.position - GetMouseWorldPosition();
        dragStartPosition = transform.position;
    }

    void EndDrag()
    {
        isDragging = false;
        transform.position = new Vector3
        {
            x = transform.position.x,
            y = transform.position.y,
            z = 0
        };

        if (colliderCount > 0)
        {
            transform.position = dragStartPosition;
            if (justCreated)
            {
                ReturnToInventory();
            }
        }

        if (!inBagArea)
        {
            ReturnToInventory();
        }

        justCreated = false;
    }

    private void DragItem()
    {
        transform.position = new Vector3
        {
            x = Mathf.RoundToInt((GetMouseWorldPosition() + mousePositionOffset).x * (1 / gamemode.gridSize)) * gamemode.gridSize,
            y = Mathf.RoundToInt((GetMouseWorldPosition() + mousePositionOffset).y * (1 / gamemode.gridSize)) * gamemode.gridSize,
            z = -1
        };
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == transform.gameObject.layer)
        {
            colliderCount++;
        }
        else if (collision.transform.gameObject.layer == 7)
        {
            inBagArea = true;
        }
    }

    void ReturnToInventory()
    {
        if (!isBeingDestroyed)
        {
            isBeingDestroyed = true;
            gamemode.ModifyInventory(item, +1);
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == transform.gameObject.layer)
        {
            colliderCount--;
        }
        else if (collision.transform.gameObject.layer == 7)
        {
            inBagArea = false;
        }
    }
}
