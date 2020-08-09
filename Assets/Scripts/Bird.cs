using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Bird : MonoBehaviour {

	[SerializeField] private float upForce = 100;
	[SerializeField] private bool isDead;
	[SerializeField] public bool isFire;
	[SerializeField] private int score;
	[SerializeField] private Fireball fireball;
	[SerializeField] private Text scoreText;

	[SerializeField] private  UnityEvent OnJump,OnDead,OnFire;
    [SerializeField] private UnityEvent OnAddPoint;

	private Rigidbody2D rigidBody2d;
	private Animator animator;
	

	// Use this for initialization
	void Start () {

		// Mendapatkan component ketika game baru jalan
		rigidBody2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		// Melakukan pengecekan jika belum mati dan klik kiri pada mouse
		if(!isDead && Input.GetMouseButtonDown(0)){
			Jump();
		}

		if(!isFire && Input.GetMouseButtonDown(1)){
			Fire();
		}
	}
			
	// Fungsi untuk mengecek sudah mati apa belum
	public bool IsDead()
	{
		return isDead;
	}

	// Membuat burung mati
	public void Dead(){

		// Pengecekan jika belum mati dan value OnDead tidak sama dengan Nukk
		if(!isDead && OnDead != null){
			// Memanggil semua event pada OnDead
			OnDead.Invoke();
		}

		// Mengeset variable Dead menjadi True
		isDead = true;
	}

	void Jump(){

		// Mengecek rigidbody null atau tidak
		if(rigidBody2d){

			// Menghentikan kecepatan burung ketika jatuh
			rigidBody2d.velocity = Vector2.zero;

			// Menambahkan gaya ke arah sumbu y agar burung meloncat
			rigidBody2d.AddForce(new Vector2(0, upForce));
		}

		// Pengecekan null variable
		if(OnJump != null){

			// Menjalankan semua event OnJump event
			OnJump.Invoke();
		}
	}

	public void Fire(){

		Fireball newFireball   =  Instantiate(fireball, transform.position + new Vector3(0.9f,0,0), Quaternion.identity);
        newFireball.gameObject.SetActive(true);
		OnFire.Invoke();
		isFire = true;
	}


	private void OnCollisionEnter2D(Collision2D collision){

		// Menghentikan animasi burung ketika bersentuhan dengan object lain
		animator.enabled = false;
	}

	public void AddScore(int value)
    {
        //Menambahkan Score value
        score += value;
		scoreText.text = score.ToString();

        //Pengecekan Null Value
        if (OnAddPoint != null)
        {
            //Memanggil semua event pada OnAddPoint
            OnAddPoint.Invoke();
        }
    }
	
}
