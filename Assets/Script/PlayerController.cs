using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;

    Rigidbody rb;

    float xInput;
    float yInput;
    float timer = 0f;

    bool gameStarted = false;

    int score = 0;
    public int winScore;
    public int highScore;
    public int playerScore;

    public GameObject winText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
        UpdateScoreText();
        StartGame();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        UpdateScoreText();
        /*playerScore = playerScore + scoreToAdd;
        scoreText.text = $"Score: {playerScore}";*/
    }

    void UpdateScoreText()
    {
        // Avoid division by zero if highScore is zero
        scoreText.text = $"Score: {playerScore} / {highScore}";
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -0f)
        {
            SceneManager.LoadScene("Game");
        }

        if (gameStarted)
        {
            timer += Time.deltaTime;
            timerText.text = $"Time: {timer:F2}"; // Display the timer in seconds with 2 decimal places

            if (timer >= 30f)
            {
                RestartGame();
            }
        }

        /* if (playerScore > highScore)
         {
             highScore = playerScore;
             PlayerPrefs.SetInt("HighScore", highScore);
             PlayerPrefs.Save(); // Ensure changes are saved immediately
         }

         highText.text = $"HighScore: {highScore}";*/

        /*if(playerScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
        }   
        highText.text = $"HighScore: {PlayerPrefs.GetInt("HighScore", 0)}";*/
    }

    private void FixedUpdate()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        rb.AddForce(xInput * speed, 0, yInput * speed);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);

            score++;
            addScore(1);

            if(score >= winScore)
            {
                //gamewin
                winText.SetActive(true);
                gameStarted = false; 
                Invoke(nameof(RestartGame), 5f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            transform.position = new Vector3(0, 2.17f, 0);
        }
    }

    // Call this method to start the timer when the game begins
    public void StartGame()
    {
        timer = 0f;
        gameStarted = true;
    }
}
