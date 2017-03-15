using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public int life = 4;

	public Rigidbody2D body;

    public GameObject playerUI;
    public GameObject[] ui_lifes;


	public float jumpSpeed = 1.0f;
	public float gravity = 30.0f;
	public float gravityForce = 3.0f;
	public float airTime = 1.0f;

	private Vector3 moveDirection = Vector3.zero;
	private float invertGrav;

	private float forceY = 0;

	private bool on_ground = true;

    private float distanceToGround;

    private GameObject[] grounds;

    private bool invunerable = false;
    private int time_invunerable = 0;
    const int max_time_invunerable = 60;

    public bool transparente = false;
    Image image;
    Color c;

    void Start () {
		body = GetComponent<Rigidbody2D> ();
		invertGrav = gravity * airTime;
        distanceToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
        GetGrounds();
        image = GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(invunerable);
        if (collision.gameObject.tag == "Wave" && !invunerable){
            life -= 1;
            ChangePlayerUIPosition();
            invunerable = true;
        }
    }

    private void ChangePlayerUIPosition() {

        playerUI.transform.position = ui_lifes[life - 1].transform.position;
    }

    
    public void GetGrounds()
    {
        grounds = GameObject.FindGameObjectsWithTag("Ground");
    }

    public void CheckIfOnTheGround()
    {
        BoxCollider2D bc = gameObject.GetComponent<BoxCollider2D>();
        if (bc.IsTouching(grounds[0].GetComponent<BoxCollider2D>()))
            on_ground = true;
    }

    public void Jump(){
		
		if (on_ground) {
			forceY = 0;
			invertGrav = gravity * airTime;
			if (Input.GetKey(KeyCode.Space)) { 
				forceY = jumpSpeed;
                on_ground = false;
            }
        }

		if (Input.GetKey (KeyCode.Space) && forceY != 0) {
			invertGrav -= Time.deltaTime;
			forceY += invertGrav * Time.deltaTime;
		}

        if (!on_ground) {
		    forceY -= gravity * Time.deltaTime * gravityForce;
		    moveDirection.y = forceY;
		    gameObject.transform.position += (moveDirection * Time.deltaTime);
            CheckIfOnTheGround();
        }
           
    }


    private void CheckIfItsDamaged() {
        if (invunerable){
            BlinkPlayer();
            UpdateDamageStatus();
        }
    }

    private void UpdateDamageStatus(){
        if (time_invunerable < max_time_invunerable)
            time_invunerable++;
        else {
            invunerable = false;
            time_invunerable = 0;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    void BlinkPlayer(){
        GetComponent<SpriteRenderer>().color = (transparente) ? new Color(1f, 1f, 1f, 0f) : new Color(1f, 1f, 1f, 1f);
        transparente = !transparente;
    }

    void FixedUpdate () {
        if (grounds == null)
            GetGrounds();
		Jump ();
        CheckIfItsDamaged();
	}
}
