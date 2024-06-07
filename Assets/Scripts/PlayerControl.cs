using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Vector2 starttouchposition;
    Vector2 endtouchposition;
    Rigidbody rb;
    public float jumpforce;
    [SerializeField] LayerMask layer;
    public bool groundcheck;
    bool down;
    bool up;
    bool right;
    bool left;
    int lane = 1;

    private void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        groundcheck = Physics.Raycast(transform.position, Vector3.down, 1.8f, layer);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            starttouchposition = Input.GetTouch(0).position;
        }
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endtouchposition = Input.GetTouch(0).position;
        }
        if (starttouchposition.x > endtouchposition.x)
        {
            Leftturn();
        }
        else if(starttouchposition.x < endtouchposition.x)
        {
            Rightturn();
        }
        if (starttouchposition.y > endtouchposition.y)
        {
            Up();
        }
        else if(starttouchposition.y < endtouchposition.y)
        {
            Down();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Leftturn();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            Rightturn();
        } 
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Up();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Down();
        }
        if (lane == 0)
        {
            transform.position = new Vector3(-10f, transform.position.y, 0f);
        }else if(lane == 1)
        {
            transform.position = new Vector3(0f, transform.position.y, 0f);
        }else if (lane == 2)
        {
            transform.position = new Vector3(10f, transform.position.y, 0f);
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    public void Leftturn()
    {
        lane -= 1;
        transform.rotation = Quaternion.Euler(0f, -60f, 0f);
        Invoke("Backtonormalrotation", 0.6f);
    }

    public void Rightturn()
    {
        lane += 1;
        transform.rotation = Quaternion.Euler(0f, 60f, 0f);
        Invoke("Backtonormalrotation", 0.6f);
    }

    public void Up()
    {
        if (groundcheck)
        {
            rb.AddForce(Vector3.up * jumpforce * 10f, ForceMode.Impulse);
        }
    }

    public void Down()
    {
        if (groundcheck)
        {
            transform.localScale.Set(1f, 0.5f, 1f);
            Invoke("Backtonormalscale", 5f);
        }
        else
        {
            rb.AddForce(Vector3.down * jumpforce * 10f, ForceMode.Impulse);
        }
    }

    void Backtonormalscale()
    {
        transform.localScale.Set(1f, 1f, 1f);
    }

    void Backtonormalrotation()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

}
