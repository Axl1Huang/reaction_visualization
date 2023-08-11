using UnityEngine;

public class MainReaction : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
        Resize();
        // GetRandomColorAndApply();
    }

    private void Resize()
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Vector2 scale = new Vector2(worldScreenWidth / spriteSize.x, worldScreenHeight / spriteSize.y);
        float minScale = Mathf.Min(scale.x, scale.y) * 0.9f;  // 添加0.9乘法因子

        transform.localScale = new Vector2(minScale, minScale);
    }

    public Color GetRandomColorAndApply()
    {
        int randomNumber = Random.Range(0, 2); // 生成0或1
        if (randomNumber == 0)
        {
            spriteRenderer.color = Color.green;
            return Color.green;
        }
        else
        {
            spriteRenderer.color = Color.red;
            return Color.red;
        }
    }

    public void ResetColor()
    {
        spriteRenderer.color = Color.white;
    }
}



