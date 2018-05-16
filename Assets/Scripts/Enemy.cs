using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float hp;
	public float fireRate;
	public float projectileSpeed;
	public GameObject projectile;


	void OnTriggerEnter2D(Collider2D collider){
		Projectile projectile = collider.gameObject.GetComponent<Projectile> ();
		if (projectile) {
			hp -= projectile.GetDamage();
			projectile.Hit();
			if(hp <=0){
				Destroy (gameObject);
			}
		}
	}

	void Update(){
		float sample = Random.value;
		if(sample > fireRate){
			Fire();
		}
	}

	void Fire(){
		Vector3 projectileSpawnPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1f);
		GameObject beam = Instantiate(projectile, projectileSpawnPosition, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector2(0f, -projectileSpeed*Time.deltaTime);
	}
}
