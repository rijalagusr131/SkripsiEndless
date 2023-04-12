using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	int currentScore = 44;

	private Touch initialTouch = new Touch ();
	private float distance = 0;
	private bool hasSwiped = false;


	public bool jump = false;
	public bool slide = false;

	public GameObject trigger;
	public Animator anim;

	public float score = 0;
	public float distancescore = 0;
	public float questionscore = 0;

	public bool boost = false;
	public Rigidbody rbody;
	public CapsuleCollider myCollider;

	public bool death = false;
	public Image gameOverImg;
	public Text scoreText;
	public Text bestScoreText;
	public Text distancescoreText;
	public Text questionscoreText;
	public Text bestdistanceScoreText;
	public Text bestquestionScoreText;
	public float lastScore;

	public GameObject distanceScore;

	// Use this for initialization
	private void Start () {
		//PlayerPrefs.DeleteAll();
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody> ();
		myCollider = GetComponent<CapsuleCollider> ();

		lastScore = PlayerPrefs.GetFloat ("MyScore");
		//currentScore = PlayerPrefs.GetInt("CurrentDistance",0);
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
		foreach (Touch t in Input.touches) 
		{
			if (t.phase == TouchPhase.Began) 
			{
				initialTouch = t;
			}
			else if(t.phase == TouchPhase.Moved && !hasSwiped)
			{
				float deltaX = initialTouch.position.x - t.position.x;
				float deltaY = initialTouch.position.y - t.position.y;
				distance = Mathf.Sqrt((deltaX*deltaX) + (deltaY * deltaY));
				bool swipedSideWay = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

				if(distance > 100f)
				{
					if (swipedSideWay && deltaX > 0)  
					{
						// swiped left
					}
					if (swipedSideWay && deltaX <= 0)  
					{
						// swiped right

					}
					if (!swipedSideWay && deltaY > 0) { 
						// swiped down
						slide = true;
						StartCoroutine (SlideController ());
					} 

					if (!swipedSideWay && deltaY <= 0) {  
						// swiped up
						jump = true;
						StartCoroutine (JumpController ());
					} 

					hasSwiped = true;
				}
			}
			else if (t.phase == TouchPhase.Ended)
			{
				initialTouch = new Touch();
				hasSwiped = false;
			}
		}
			

		if (death == true) {
			gameOverImg.gameObject.SetActive (true);
		}

		// Player Controll Start
		if (score >= 100 && death != true) {
			transform.Translate (0, 0, 0.2f);
		} else if (score >= 200 && death != true) {
			transform.Translate (0, 0, 0.3f);
		} else if (death == true) {
			transform.Translate (0,0,0);
		}
		else {
			transform.Translate (0, 0, 0.1f);
		}


		if (boost == true) {
			transform.Translate (0, 0, 1f);
			myCollider.enabled = false;
			rbody.isKinematic = true;
		} else {
			myCollider.enabled = true;
			rbody.isKinematic = false;
		}
			

		if (jump == true) {
			anim.SetBool ("isJump", jump);
			transform.Translate (0, 0.3f, 0.1f);
		} else if (jump == false) {
			anim.SetBool ("isJump", jump);
		}

		if (slide == true) {
			anim.SetBool ("isSlide", slide);
			transform.Translate (0, 0, 0.1f);
			myCollider.height = 1.8f;
		} else if (slide == false) {
			anim.SetBool ("isSlide", slide);
			myCollider.height = 2.05f;
		}

		// Player Control End

		trigger = GameObject.FindGameObjectWithTag ("Obstacle");

		currentScore += (int)transform.position.z;
		SetFinalScore();

	}

	private void SetFinalScore()
    {
		bestdistanceScoreText.text = " Your Distance Score : " + currentScore.ToString();
		scoreText.text = score.ToString();
		if (score > lastScore)
        {
			bestScoreText.text = "Best Score : " + score.ToString();
        }
		else
        {
			bestScoreText.text = "Your Score :" + score.ToString();
		}

		distancescoreText.text = currentScore.ToString();

		if (score > currentScore)
		{
			bestScoreText.text = "Best Score : " + score.ToString();
		}
		else
		{
			bestdistanceScoreText.text = " Your Distance Score : " + currentScore.ToString();
		}

		questionscoreText.text = score.ToString();

		if (score > currentScore)
		{
			bestScoreText.text = "Best Score : " + score.ToString();
		}
		else
		{
			bestquestionScoreText.text = " question Score : " + score.ToString();
		}
	}

	private void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "PlayerTrigger") {
			Destroy (trigger.gameObject);
		}

		if (other.gameObject.tag == "Coin") {
			Destroy (other.gameObject, 0.5f);
			score += 5f;
		}

		if (other.gameObject.tag == "Boost") {
			Destroy (other.gameObject);
			StartCoroutine (BoostController ());
		}

		if (other.gameObject.tag == "DeathPoint") {
			death = true;
			if (score > lastScore) {
				PlayerPrefs.SetFloat ("MyScore", score);
			}
			distancescoreText.text = "0";
		}
	}

	IEnumerator BoostController(){
		boost = true;
		yield return new WaitForSeconds (3);
		boost = false;
	}

	IEnumerator JumpController(){
		jump = true;
		yield return new WaitForSeconds (0.2f);
		jump = false;
	}

	IEnumerator SlideController(){
		slide = true;
		yield return new WaitForSeconds (1f);
		slide = false;
	}


		
	public void GoToMenu(){
		SceneManager.LoadScene (0);
	}

	public void PlayAgain(){
		SceneManager.LoadScene (1);
	}
}
