using UnityEngine;

public class BallJoint : MonoBehaviour

 {
    private TargetJoint2D jointt;
    private Camera cam;

    void Start()
    {
        jointt = GetComponent<TargetJoint2D>();
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        jointt.enabled = true;
        jointt.target = GetMousePos();
    }

    void OnMouseDrag()
    {
        jointt.target = GetMousePos();
    }

    void OnMouseUp()
    {
        jointt.enabled = false;
    }

    Vector2 GetMousePos()
    {
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(pos.x, pos.y);
    }
}
