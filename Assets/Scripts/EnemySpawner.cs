using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float velocity;

	private float xmin;
	private float xmax;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}

		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 topLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 bottomRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distance));
		xmin = topLeft.x + width/2;
		xmax = bottomRight.x - width/2;
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height, 0));
	}

	void Update(){
		gameObject.transform.position = new Vector3((velocity * Time.deltaTime) + gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		ClampPosition ();
	}
	
	void ClampPosition(){
		float newx = Mathf.Clamp(gameObject.transform.position.x, xmin, xmax);
		if (newx < gameObject.transform.position.x){
			velocity = - Mathf.Abs(velocity);
		}
		else if(newx > gameObject.transform.position.x){
			velocity = Mathf.Abs(velocity);
		}
		gameObject.transform.position = new Vector3 (newx, gameObject.transform.position.y, gameObject.transform.position.z);
	}
}
