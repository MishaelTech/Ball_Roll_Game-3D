using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;

    Rigidbody rb;

    float xInput;
    float yInput;

    int score = 0;
    public int winScore;
    public int highScore;
    public int playerScore;

    public GameObject winText;
    public TextMeshProUGUI highText;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -5f)
        {
            SceneManager.LoadScene("Game");
        }

        if(playerScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
        }   
        highText.text = $"HighScore: {PlayerPrefs.GetInt("HighScore", 0)}";
    }

    private void FixedUpdate()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        rb.AddForce(xInput * speed, 0, yInput * speed);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);

            score++;

            if(score >= winScore)
            {
                //gamewin
                winText.SetActive(true);
                Invoke(nameof(RestartGame), 5f);
            }
        }
    }
}
