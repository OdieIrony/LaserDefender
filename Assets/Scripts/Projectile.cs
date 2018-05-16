using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	private float damage = 1;

	public float GetDamage (){
		return damage;
	}

	public void Hit(){
		Destroy (gameObject);
	}
}
