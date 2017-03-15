using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	private int life = 4;

    public GameObject grapeController;


    public GameObject ui_life_bar;

    private bool is_drunk = false;

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

    void Start () {
		invertGrav = gravity * airTime;
        distanceToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
        GetGrounds();
    }

    //switch case
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wave" && !invunerable)
        {
            CollideWithWave();
        }

        else if (collision.gameObject.tag == "Grape")
        {
            grapeController.GetComponent<GrapeController>().GetGrape();
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Bubbly")
        {
            GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().AddScore(4);

            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Rose")
        {
            GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().AddScore(10);

            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Beer")
        {
            Is_Drunk();
            Destroy(collision.gameObject);
        }
    }

    public void Is_Drunk()
    {
        GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().AddScore(-5);
    }




    private void CollideWithWave()
    {
        life -= 1;
        CheckIfIsDead();
        CheckIfGiantChickenAppears();

        ui_life_bar.GetComponent<UI_life_controller>().ChangePlayerUIPosition(life);
        invunerable = true;
    }

    private void CheckIfGiantChickenAppears()
    {
        if (life == 1)
        {
            grapeController.GetComponent<GrapeController>().EnterChickenMode();
            //Invoka a galinha gigante
        }
    }

    private void CheckIfIsDead()
    {
        if (life == 0)
            SceneManager.LoadScene("Menu");
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
			if (Input.GetButton("Jump")) { 
				forceY = jumpSpeed;
                on_ground = false;
            }
        }

		if (Input.GetButton("Jump") && forceY != 0) {
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

    public void GainLife()
    {
        life += 1;
    }

    public int GetLife()
    {
        return life;
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
        /*
        if (Input.GetKey("left"))
            transform.Rotate(0, 0, 100 * Time.deltaTime, Space.Self);
        if (Input.GetKey("right"))
            transform.Rotate(0, 0, -100 * Time.deltaTime, Space.Self);
        */
        Jump ();
        CheckIfItsDamaged();
	}
}
