using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;

    Rigidbody rb;

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

    void Update()
    {
        if (transform.position.y < -0f)
        {
            SceneManager.LoadScene("Game");
        }

        if (gameStarted)
        {
            timer += Time.deltaTime;
            timerText.text = $"Time: {30 - timer:F2}";

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
        /*xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        rb.AddForce(xInput * speed, 0, yInput * speed);*/
        HandleInput();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);

            score++;
            addScore(1);

            if (score >= winScore)
            {
                //gamewin
                winText.SetActive(true);
                gameStarted = false;
                Invoke(nameof(RestartGame), 5f);
            }
        }
    }

    private void HandleInput()
    {
        float xInput = 0;
        float yInput = 0;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;

            // Calculate the normalized touch position
            float normalizedX = (touchPosition.x / Screen.width) * 2 - 1;
            float normalizedY = (touchPosition.y / Screen.height) * 2 - 1;

            // Use the normalized touch position as input
            xInput = normalizedX;
            yInput = normalizedY;
        }
        else
        {
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");
        }

        rb.AddForce(new Vector3(xInput, 0, yInput) * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            transform.position = new Vector3(0, 2.17f, 0);
        }
    }

    public void StartGame()
    {
        timer = 0f;
        gameStarted = true;
    }
}
