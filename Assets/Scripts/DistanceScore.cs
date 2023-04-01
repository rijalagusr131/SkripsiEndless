using UnityEngine;
using UnityEngine.UI;

public class DistanceScore : MonoBehaviour
{

	public Transform player;
    public Text scoreText;
    int currentScore;


    // Update is called once per frame
    void Update()
    {
        int x = 44;
        scoreText.text = (player.position.z + x).ToString("0");
        PlayerPrefs.SetInt("CurrentDistance", (int)(player.position.z + x));
        currentScore = PlayerPrefs.GetInt("CurrentDistance");
        scoreText.text = currentScore.ToString();
    }
}