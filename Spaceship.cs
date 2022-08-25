using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour {

	public float speed = 1f;
	public GameObject explosion;

	public static int score = 0;

	public Text scoreT;
	public Text bestMissDefl;
	public Text time;
	public GameObject gameOver;
	public Button rest;
	public Button menu;

	public static Vector2Int bestDef = Vector2Int.zero;

	public List<AudioClip> clips = new List<AudioClip>();
	public AudioSource source;

	public static int dest;

	public int toStopAtDest;

	public bool canBoost;
	public bool canShrink;
	public bool canForceField;
	public bool canForceField2;

	public bool isShrinking = false;

	public GameObject forceFieldObj;
	public GameObject forceFieldObj2;

	public GameObject boostImg;
	public GameObject shrinkImg;
	public GameObject forceFieldImg;
	public GameObject forceFieldImg2;

	void Awake()
    {
		Time.timeScale = 1f;
		score = 0;
		dest = 0;
		toStopAtDest = 5;
		bestDef = Vector2Int.zero;
		gameOver.SetActive(false);
		boostImg.SetActive(false);
		shrinkImg.SetActive(false);
		forceFieldImg.SetActive(false);
		forceFieldImg2.SetActive(false);
		rest.gameObject.SetActive(false);
		rest.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
		menu.gameObject.SetActive(false);
		menu.onClick.AddListener(() => SceneManager.LoadScene(0));
	}

	private void Update()
	{
		if (dest == toStopAtDest)
        {
			toStopAtDest += 5;
			float r = Random.value;
			if (r < 0.25f)
            {
				canBoost = true;
            }
			else if (r < 0.5f)
            {
				canShrink = true;
			}
            else if (r < 0.75f)
            {
				canForceField2 = true;
			}
			else
			{
				canForceField = true;
			}
		}

		boostImg.SetActive(canBoost);
		shrinkImg.SetActive(canShrink);
		forceFieldImg.SetActive(canForceField);
		forceFieldImg2.SetActive(canForceField2);

		if (canBoost && Input.GetKeyDown(KeyCode.Alpha1))
        {
			Boost();
		}
		if (canShrink && Input.GetKeyDown(KeyCode.Alpha2))
		{
			StartCoroutine("Shrink");
		}
		if (canForceField && Input.GetKeyDown(KeyCode.Alpha3))
		{
			ForceField();
		}
		if (canForceField2 && Input.GetKeyDown(KeyCode.Alpha4))
		{
			ForceField2();
		}

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		Vector2 inp = new Vector2(horizontal, vertical);
		horizontal = inp.x * speed * Time.deltaTime;
		vertical = inp.y * speed * Time.deltaTime;
		scoreT.text = "Score: " + score.ToString();
		transform.Translate(horizontal, vertical, 0f);
	}

	void OnTriggerEnter2D(Collider2D coll)
    {
		if (coll.gameObject.tag != "Force Field" && coll.gameObject.tag != "Force Field 2")
		{
			source.clip = clips[Random.Range(0, clips.Count - 1)];
			source.Play();
			Destroy(gameObject);
			Instantiate(explosion, transform.position, transform.rotation);
			gameOver.SetActive(true);
			rest.gameObject.SetActive(true);
			menu.gameObject.SetActive(true);
			bestMissDefl.text = "Missiles defended against: " + dest;
			time.text = "Time Taken: " + (int)(Time.timeSinceLevelLoad / 3600) + " hrs " + ((int)(Time.timeSinceLevelLoad / 60) - (int)((Time.timeSinceLevelLoad / 3600) * 60)) + " min " + (int)(Time.timeSinceLevelLoad % 60) + " sec";
			Time.timeScale = 0f;
		}
	}

	void Boost()
    {
		canBoost = false;
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		Vector2 inp = new Vector2(horizontal, vertical);
		horizontal = inp.x * speed * Time.deltaTime;
		vertical = inp.y * speed * Time.deltaTime;
		transform.Translate(horizontal * 100, vertical * 100, 0f);
    }

	IEnumerator Shrink()
    {
		canShrink = false;
		speed = 24f;
		transform.localScale = Vector3.one * 2f;
		yield return new WaitForSeconds(5f);
		transform.localScale = Vector3.one * 4f;
		speed = 12f;
	}

	void ForceField()
    {
		Instantiate(forceFieldObj, transform.position, Quaternion.identity);
		canForceField = false;
	}

	void ForceField2()
	{
		Instantiate(forceFieldObj2, transform.position, Quaternion.identity);
		canForceField2 = false;
	}
}
