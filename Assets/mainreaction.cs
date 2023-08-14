using UnityEngine;
using brainflow;
using Plugins.Restfulness;

public class MainReaction : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public BoardIds boardId;
    private Predictor _predictor;
    private double latestScore;

    private int redCount = 0;
    private int yellowCount = 0;
    private int greenCount = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
        Resize();

        _predictor = new Predictor(boardId);
        _predictor.OnRestfulnessScoreUpdated += OnRestfulnessScoreUpdated;
        _predictor.StartSession();

        Invoke("EndSession", 60f);  // 结束会话的计时器
    }

    private void Resize()
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        Vector2 scale = new Vector2(worldScreenWidth / spriteSize.x, worldScreenHeight / spriteSize.y);
        float minScale = Mathf.Min(scale.x, scale.y) * 0.9f;

        transform.localScale = new Vector2(minScale, minScale);
    }

    public Color GetCurrentColor()
    {
        return spriteRenderer.color;
    }

    public void ResetColor()
    {
        spriteRenderer.color = Color.white;
    }

    private void OnRestfulnessScoreUpdated(double score)
    {
        Debug.Log("Score updated: " + score);
        latestScore = score;
        UpdateColorBasedOnLatestScore();
    }

    public void UpdateColorBasedOnLatestScore()
    {
        if (latestScore < 0.33)
        {
            spriteRenderer.color = Color.red;
            redCount++;
        }
        else if (latestScore >= 0.33 && latestScore < 0.66)
        {
            spriteRenderer.color = Color.yellow;
            yellowCount++;
        }
        else if (latestScore >= 0.66)
        {
            spriteRenderer.color = Color.green;
            greenCount++;
        }
    }

    private void EndSession()
    {
        _predictor.StopSession();
        Debug.Log("Time end!");
        DisplayResultsInConsole();
    }

    private void DisplayResultsInConsole()
    {
        float totalCount = redCount + yellowCount + greenCount;
        float redPercentage = (redCount / totalCount) * 100;
        float yellowPercentage = (yellowCount / totalCount) * 100;
        float greenPercentage = (greenCount / totalCount) * 100;

        string results = $"Red: {redCount} ({redPercentage:0.0}%)\nYellow: {yellowCount} ({yellowPercentage:0.0}%)\nGreen: {greenCount} ({greenPercentage:0.0}%)";
        Debug.Log(results);
    }
}









