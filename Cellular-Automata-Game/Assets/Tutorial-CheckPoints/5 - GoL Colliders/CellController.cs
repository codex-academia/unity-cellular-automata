using CA;
using UnityEngine;

public class CellController : MonoBehaviour
{
    private GameOfLife2 gameOfLife;

    public void Bind(GameOfLife2 gameOfLife)
    {
        this.gameOfLife = gameOfLife;
    }

    public void Destroy()
    {
        var pos = transform.position;
        gameOfLife.DestroyCell((int)pos.x, (int)pos.z);
        Destroy(gameObject);
    }
}