using UnityEngine;

public class MovingPoint : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 leftEdge;
    private Vector3 rightEdge;

    private bool shouldMove = true;
    private SpriteRenderer spriteRenderer;
    private MainReaction mainReaction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2f, Camera.main.nearClipPlane));
        rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2f, Camera.main.nearClipPlane));

        transform.position = leftEdge;

        mainReaction = FindObjectOfType<MainReaction>();
    }

    private void Update()
    {
        if (shouldMove)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (transform.position.x > rightEdge.x)
            {
                ResetPointAndMainCircle();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MainReaction>())
        {
            shouldMove = true;
            mainReaction.UpdateColorBasedOnLatestScore();  // 首先更新大圆的颜色
            spriteRenderer.color = mainReaction.GetCurrentColor();  // 然后，将小圆的颜色设置为与大圆相同的颜色
        }
    }

    private void ResetPointAndMainCircle()
    {
        transform.position = leftEdge;
        spriteRenderer.color = Color.white;
        shouldMove = true;
        mainReaction.ResetColor();
    }
}



