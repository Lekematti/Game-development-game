using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D body;
    private Animator anim;
    private int count;
    public TextMeshProUGUI countText;
	public GameObject winTextObject;
    
    
    private void Awake()
    {   
        //Grabs references for rigidbody and animator from game object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        count = 0;
		SetCountText ();
        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
    }
 
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip player when facing left/right.
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // code for jumping
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        //sets animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
    }
 
    private void Jump()
    {
         if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            //anim.SetTrigger("jump");
        }
    }
 
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

/////////////////////////////////////////////////////////////////////////////////////////////////

    void OnTriggerEnter2D(Collider2D collider)
	{
		// if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (collider.CompareTag("Blood") /*&& GetComponent<Health>().health <= 0*/)
		{
            collider.gameObject.SetActive (false);
			// Add one to the score variable 'count'
			count++;

			// Run the 'SetCountText()' function (see below)
			SetCountText();
		}
	}

    void SetCountText()
	{
		countText.text = "Blood collected: " + count.ToString();

		if (count >= 5) 
		{
                    // Set the text value of your 'winText'
                    winTextObject.SetActive(true);
		}
	}
}

        


