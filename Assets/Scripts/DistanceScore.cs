using UnityEngine;
using UnityEngine.UI;

public class DistanceScore : MonoBehaviour
{

	public Transform player;
    public Text scoreText;


    // Update is called once per frame
    void Update()
    {
        int x = 44;
        scoreText.text = (player.position.z + x).ToString("0");
    }
}