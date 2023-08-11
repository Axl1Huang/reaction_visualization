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
        Debug.Log("Collision Detected with: " + collision.gameObject.name);  // 添加这行
        if (collision.gameObject.GetComponent<MainReaction>())
        {
            shouldMove = true;
            Color newColor = mainReaction.GetRandomColorAndApply();
            spriteRenderer.color = newColor;
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

