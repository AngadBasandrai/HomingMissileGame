using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour {

	public Vector3 target;

	public float speed = 5f;
	public float rotateSpeed = 200f;

	private Rigidbody2D rb;

	public GameObject missile;

	public GameObject explosion;

	public Vector3 spawnPos;

	public bool ai = false;

	public AudioSource source;

	public List<AudioClip> booms = new List<AudioClip>();

	public bool overriden = false;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		Invoke("GetWrecked", 10);
	}
	
	void FixedUpdate()
	{
		if (!overriden)
		{
			if (!ai)
			{
				target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
			else
			{
				if (FindObjectOfType<Spaceship>())
				{
					target = FindObjectOfType<Spaceship>().transform.position;
				}
			}
		}

        else
        {
			target = -FindObjectOfType<Spaceship>().transform.position;
		}

		Vector2 direction = (Vector2)target - rb.position;

		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.up).z;

		rb.angularVelocity = -rotateAmount * rotateSpeed;

		rb.velocity = transform.up * speed;
	}

	void GetWrecked()
    {
		source.clip = booms[Random.Range(0, booms.Count - 1)];
		source.Play();
		Spaceship.score += (int)((speed * rotateSpeed));
		Spaceship.dest += 1;
		Destroy(gameObject);
		HomingMissile m = Instantiate(missile, spawnPos, Quaternion.Euler(new Vector3(0, 0, 45))).GetComponent<HomingMissile>();
		float r = Random.Range(-0.1f, 0.1f);
		m.speed = speed + r;
		m.spawnPos = spawnPos;
		m.rotateSpeed = rotateSpeed - r * 25;
		m.enabled = true;
		m.ai = ai;
		m.booms = booms;
		m.source = source;
		m.GetComponent<Collider2D>().enabled = true;
		Instantiate(explosion, transform.position, transform.rotation);
    }

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag != "Force Field 2")
		{
			source.clip = booms[Random.Range(0, booms.Count - 1)];
			source.Play();
			Spaceship.score += (int)((speed * rotateSpeed));
			Spaceship.dest += 1;
			CancelInvoke("GetWrecked");
			Destroy(gameObject);
			HomingMissile m = Instantiate(missile, spawnPos, Quaternion.Euler(new Vector3(0, 0, 45))).GetComponent<HomingMissile>();
			float r = Random.Range(-0.1f, 0.1f);
			m.speed = speed + r;
			m.spawnPos = spawnPos;
			m.rotateSpeed = rotateSpeed - r * 15;
			m.enabled = true;
			m.ai = ai;
			m.booms = booms;
			m.source = source;
			m.GetComponent<Collider2D>().enabled = true;
			Instantiate(explosion, transform.position, transform.rotation);
		}
        else
        {
			StartCoroutine("Override");
        }
	}

	IEnumerator Override()
    {
		overriden = true;
		yield return new WaitForSeconds(5f);
		overriden = false;
	}
}
