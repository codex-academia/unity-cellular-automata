using CA;
using UnityEngine;

public class CellCounter : MonoBehaviour
{
    public Main3 Main3; // Since it is public we can reference it in the inspector
    public TMPro.TextMeshProUGUI cellCounterText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Main3 = GetComponent<Main3>();
        // Main3 = FindObjectOfType<Main3>();
    }

    // Update is called once per frame
    void Update()
    {
        cellCounterText.text = Main3.CellCount.ToString();
    }
}
