using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawLineThroughCircle : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        DrawLine();
    }

    void DrawLine()
    {
        Vector3 leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2f, Camera.main.nearClipPlane));
        Vector3 rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2f, Camera.main.nearClipPlane));

        lineRenderer.positionCount = 2; // 设置为2，因为我们只需要两个点
        lineRenderer.SetPosition(0, leftEdge);
        lineRenderer.SetPosition(1, rightEdge);
    }
}

