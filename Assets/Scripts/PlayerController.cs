using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalBoundary;
    public float horizontalSpeed;
    public float maxSpeed;

    public BulletManager bulletManager;

    private Rigidbody2D rb;
    private Vector3 touchesEnd;
    // Start is called before the first frame update
    void Start()
    {
        touchesEnd = new Vector3();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _Move();
        _CheckBounds();
        _FireBullet();
    }

    private void _FireBullet()
    {
        if (Time.frameCount % 20 == 0)
        {
            bulletManager.GetBullet(transform.position);
        }
    }
    private void _Move()
    {
        float direction = 0.0f;

       

        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);
            if (worldTouch.x >  transform.position.x)
            {
                direction = 1.0f;
            }
            if (worldTouch.x < transform.position.x)
            {
                direction = -1.0f;
            }
            touchesEnd = worldTouch;
        }        
       
        if((Input.GetAxis("Horizontal") >= 0.1f))
        {
            direction = 1.0f;
        }
        if ((Input.GetAxis("Horizontal") <= -0.1f))
        {
            direction = -1.0f;
        }
        rb.velocity = Vector2.ClampMagnitude( rb.velocity + new Vector2(direction * horizontalSpeed,0), maxSpeed);
        rb.velocity *= 0.99f;
        if(touchesEnd.x != 0)
        {
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, touchesEnd.x, 0.01f), transform.position.y);
        }

    }
    private void _CheckBounds()
    {
        if (transform.position.x >= horizontalBoundary)
        {
            transform.position = new Vector3(horizontalBoundary,transform.position.y,0);
        }
        if (transform.position.x <= -horizontalBoundary)
        {
            transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0);
        }
    }
    private void _Reset()
    {
       
    }
}
