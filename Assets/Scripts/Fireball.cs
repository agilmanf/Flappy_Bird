using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Fireball : MonoBehaviour {

	[SerializeField] private float speed = 4f;
	[SerializeField] private Bird bird;
	[SerializeField] private GameObject explosionRef;
	[SerializeField] private  UnityEvent OnDestroy;
	
	// Update is called once per frame
	void Update () {
			//Membuat Fireball bergerak kesebelah kanan dengan kecepatan tertentu
			 transform.Translate(Vector3.right * speed * Time.deltaTime,Space.World); 
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        //Memusnahkan object ketika bersentuhan
		if(collision.gameObject.tag == "Pipe"){
			OnDestroy.Invoke();
			bird.isFire = false;
			GameObject newExplosion = Instantiate(explosionRef, transform.position, transform.rotation);
			newExplosion.SetActive(true);
			Destroy (newExplosion, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.4f);
			Destroy(collision.gameObject);
			Destroy(this.gameObject);
		}
        
    }
}
