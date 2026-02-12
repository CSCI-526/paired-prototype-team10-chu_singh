using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;

    public int score = 0;
    public int scoreToWin = 3;

    private bool isInvincible = false;
    public float invincibilityTimer = 0f;

    public TextMeshProUGUI coinText;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (invincibilityTimer > 0)
        {
            isInvincible = true;
            invincibilityTimer -= Time.deltaTime;
            player.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            isInvincible = false;
            player.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f);
        }

    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);

        if (coinText != null)
        {
            coinText.text = "Coins: " + score;
        }

        if (score >= scoreToWin)
        {
            Debug.Log("You Win!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void HitObstacle()
    {
        if (isInvincible) return;
        Debug.Log("Hit an obstacle! Game Over.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GrantInvincibility(float duration)
    {
        invincibilityTimer = duration;
    }
}
