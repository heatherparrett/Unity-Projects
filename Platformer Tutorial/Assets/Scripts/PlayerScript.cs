using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text countText;
    public Text winText;
    public Text lifeText;
    public Text loseText;

    public AudioSource winSource;

    private int count;
    private int life;

    Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        winSource = GetComponent<AudioSource>();
        rd2d = GetComponent<Rigidbody2D>();
        count = 0;
        life = 3;
        winText.text = "";
        loseText.text = "";
        SetCountText();
        SetLifeText();
        anim = GetCompon
    }

    private void Update()
    {

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            Destroy(collision.collider.gameObject);

            count = count + 1;
            SetCountText();
        }

        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            life = life - 1;
            SetLifeText();
            SetCountText();
        }


    }


private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void SetLifeText()
    {
        lifeText.text = "<b> Lives: </b>" + life.ToString();

        if (life <= 0)
        {
            loseText.text = "You lose! Game created by Heather Parrett";
            rd2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }

    void SetCountText()
    {
        countText.text = "<b>Count:</b> " + count.ToString();

        if (count >= 8)
        {
            winText.text = "You win! Game created by Heather Parrett";
            winSource.Play();
        }

        if (count == 4)
        {
            transform.position = new Vector2(0.0f, 41.5f);
            life = 3;
            lifeText.text = "<b> Lives: </b>" + life.ToString();
        }
    }
}