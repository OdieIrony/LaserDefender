using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {
	public float velocity;

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere (transform.position, 0.25f);
	}
}
