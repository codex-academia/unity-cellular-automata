using UnityEngine;

public class EraserController : MonoBehaviour
{
    public GameObject eraser;
    private Camera main;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Align GameObject with the mouse position
        var ray = main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            eraser.transform.position = hit.point;
        }

        // also orientation
        eraser.transform.rotation = Quaternion.Euler(0, 0, 0);
        var rayDirection = ray.direction;
        Vector3 direction = hit.point - main.transform.position;
        var rotation = Quaternion.FromToRotation(Vector3.up, direction);
        eraser.transform.rotation = rotation;
    }
}