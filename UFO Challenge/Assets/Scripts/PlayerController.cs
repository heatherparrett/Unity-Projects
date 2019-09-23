using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text nameText;
    public Text lifeText;
    public Text loseText;

    private Rigidbody2D rb2d;
    private int count;
    private int life;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        life = 3;
        winText.text = "";
        nameText.text = "";
        loseText.text = "";
        SetCountText();
        SetLifeText();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag ("PickUp"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag ("EnemyPickup"))
        {
            other.gameObject.SetActive(false);
            count = count - 1;
            life = life - 1;
            SetLifeText();
            SetCountText();
        }

        if (count == 12)
        {
            transform.position = new Vector2(70.24f, -0.45f);
        }
    }

    void SetLifeText()
    {
        lifeText.text = "<b> Lives: </b>" + life.ToString();

        if (life <= 0)
        {
            rb2d.gameObject.SetActive(false);
            loseText.text = "<i>You lose!</i>";
            nameText.text = "Game created by Heather Parrett!";
        }
    }

    void SetCountText()
    {
        countText.text = "<b>Count:</b> " + count.ToString();

        if (count >= 20)
        {
            winText.text = "<i>You win!</i>";
            nameText.text = "Game created by Heather Parrett!";
        }
    }
}