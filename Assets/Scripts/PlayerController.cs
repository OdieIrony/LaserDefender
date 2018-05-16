using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float maxVelocity = 5f;
	public float acceleration = 1f; //A scaler for this.controlDirection
	public float dampFactor = 0.85f;
	public float xPadding;
	public float yPadding;
	public GameObject projectile;
	public float projectileSpeed;
	public float fireRate;


	private Vector2 controlDirection;
	private Vector2 force;
	private Rigidbody2D theRigidBody;
	private float xmin;
	private float xmax;
	private float ymin;
	private float ymax;
	//private int sqrtHalf = 0.7071;

	void Start(){
		theRigidBody = gameObject.GetComponent<Rigidbody2D> ();

		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 topLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 bottomRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distance));

		xmin = topLeft.x + xPadding;
		xmax = bottomRight.x - xPadding;
		ymin = topLeft.y - yPadding;
		ymax = bottomRight.y + yPadding;
		controlDirection = new Vector2 (0f, 0f);

	}

	// Update is called once per frame
	void Update () {
		GetControlVector ();
		ScaleVelocity ();
		ClampPosition ();
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, fireRate);
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}
	}

	void Fire(){
		Vector3 projectileSpawnPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1f);
		GameObject beam = Instantiate(projectile, projectileSpawnPosition, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector2(0f, projectileSpeed);
	}

	//Reads the input of the arrow keys and sets
	//controlDirection appropriately, where the magnitude of
	//controlDirection is equal to one.
	void GetControlVector(){
		if (Input.GetKey (KeyCode.UpArrow)) {
			//If up and left are pressed:
			if (Input.GetKey (KeyCode.LeftArrow)) {
				controlDirection.Set (-0.7071f, 0.7071f);
				//If up and right are pressed:
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				controlDirection.Set (0.7071f, 0.7071f);
				//If up is the only thing pressed:
			} else {
				controlDirection.Set (0f, 1f);
			}
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			//If down and left are pressed:
			if (Input.GetKey (KeyCode.LeftArrow)) {
				controlDirection.Set (-0.7071f, -0.7071f);
				//If down and right are pressed:
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				controlDirection.Set (0.7071f, -0.7071f);
				//If down is the only thing pressed:
			} else {
				controlDirection.Set (0f, -1f);
			}
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			controlDirection.Set (-1f, 0f);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			controlDirection.Set(1f, 0f);
		}else{
			controlDirection.Set (0f, 0f);
		}
	}

	void ScaleVelocity(){
		theRigidBody.velocity *= dampFactor;
		force = controlDirection * acceleration;
		theRigidBody.velocity += force;
		theRigidBody.velocity = Vector2.ClampMagnitude (theRigidBody.velocity, maxVelocity);
	}

	void ClampPosition(){
		float newx = Mathf.Clamp(gameObject.transform.position.x, xmin, xmax);
		float newy = Mathf.Clamp(gameObject.transform.position.y, ymin, ymax);
		gameObject.transform.position = new Vector3 (newx, newy, gameObject.transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.GetType() is Projectile){
			Debug.Log("Player hit by projectile");
		}
	}
}
