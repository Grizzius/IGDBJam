using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public Items item;

    GameMode gamemode;
    Collider2D itemCollider;
    Vector3 mousePositionOffset;
    Vector3 dragStartPosition;
    SpriteRenderer spriteRenderer;

    int colliderCount;

    private void Start()
    {
        gamemode = FindObjectOfType<GameMode>();
        itemCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.sprite;
    }

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        mousePositionOffset = transform.position - GetMouseWorldPosition();
        dragStartPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = new Vector3
        {
            x = Mathf.RoundToInt((GetMouseWorldPosition() + mousePositionOffset).x * (1 / gamemode.gridSize)) * gamemode.gridSize,
            y = Mathf.RoundToInt((GetMouseWorldPosition() + mousePositionOffset).y * (1 / gamemode.gridSize)) * gamemode.gridSize,
            z = -1
        };
        
    }

    private void OnMouseUp()
    {
        transform.position = new Vector3
        {
            x = transform.position.x,
            y = transform.position.y,
            z = 0
        };
        Debug.Log(colliderCount);

        if (colliderCount > 0)
        {
            transform.position = dragStartPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.gameObject.layer == transform.gameObject.layer)
        {
            Debug.Log(itemCollider.bounds.Intersects(collision.bounds));
            colliderCount++;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == transform.gameObject.layer)
        {
            colliderCount--;
        }
    }
}
