using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] AudioClip jumpclip;
    [SerializeField] AudioClip crouch;
    [SerializeField] EventSystem es;
    public float swipe;
    Vector2 starttouchposition;
    Vector2 endtouchposition;
    Vector2 direction;
    AudioSource ad;
    Rigidbody rb;
    float jumpforce = 6;
    bool groundcheck;
    int lane = 1;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            swipe = PlayerPrefs.GetFloat("Sensitivity");
        }
        else
        {
            swipe = 260f;
        }
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody>();
        ad = GetComponent<AudioSource>();
    }

    void Update()
    {
        groundcheck = Physics.Raycast(transform.position, Vector3.down, 2.5f, layer);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            starttouchposition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endtouchposition = Input.GetTouch(0).position;
            direction = starttouchposition - endtouchposition;
        }
        if (direction.x <= -swipe)
        {
            Rightturn();
        }
        if (direction.x >= swipe)
        {
            Leftturn();
        }
        if (direction.y >= swipe)
        {
            Down();
        }
        if (direction.y <= -swipe)
        {
            Up();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Leftturn();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
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
        }
        else if (lane == 1)
        {
            transform.position = new Vector3(0f, transform.position.y, 0f);
        }
        else if (lane == 2)
        {
            transform.position = new Vector3(10f, transform.position.y, 0f);
        }
        else
        {
            Time.timeScale = 0f;
        }
        if (groundcheck)
        {
            ad.Pause();
        }
        else
        {
            ad.Play();
        }
    }

    public void Leftturn()
    {

        lane -= 1;
        transform.rotation = Quaternion.Euler(0f, -60f, 0f);
        Invoke("Backtonormalrotation", 0.6f);
        direction = Vector2.zero;

    }

    public void Rightturn()
    {

        lane += 1;
        transform.rotation = Quaternion.Euler(0f, 60f, 0f);
        Invoke("Backtonormalrotation", 0.6f);
        direction = Vector2.zero;

    }

    public void Up()
    {

        if (groundcheck)
        {
            ad.PlayOneShot(jumpclip);
            rb.AddForce(Vector3.up * jumpforce * 10f, ForceMode.Impulse);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        direction = Vector2.zero;

    }

    public void Down()
    {

        if (groundcheck)
        {
            ad.PlayOneShot(crouch);
            transform.localScale = new Vector3(1f, 0.5f, 1f);
            Invoke("Backtonormalscale", 2f);
        }
        else
        {
            rb.AddForce(Vector3.down * jumpforce * 10f, ForceMode.Impulse);
        }
        direction = Vector2.zero;

    }

    void Backtonormalscale()
    {

        transform.localScale = new Vector3(1f,1f,1f);

    }

    void Backtonormalrotation()
    {

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

    }

}
