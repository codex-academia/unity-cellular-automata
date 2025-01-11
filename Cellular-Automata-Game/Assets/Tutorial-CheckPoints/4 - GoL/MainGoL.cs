using System;
using System.Collections.Generic;
using UnityEngine;

namespace CA
{
    public partial class MainGoL : MonoBehaviour
    {
        public GameOfLife gameOfLife;
        private List<GameObject> gameObjects;
        public GameObject cellPrefab;
        private Camera main;

        private void Start()
        {
            gameOfLife = new GameOfLife(100, 100);
            gameObjects = new List<GameObject>();
            main = Camera.main;
        }

        private void Update()
        {
            if (Time.frameCount % 10 == 0)
            {
                gameOfLife.Update();
                gameOfLife.Draw();
                DrawCells(gameOfLife.Cells);
            }

            if (Input.GetMouseButtonUp(0))
            {
                var ray = main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    var point = hit.point;
                    gameOfLife.DrawBlinker((int)point.x, (int)point.z);
                    // Debug.Log($"Cell at {cell.x}, {cell.z} is alive.");
                    // Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 10);
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                var ray = main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    var point = hit.point;
                    gameOfLife.DrawGlider((int)point.x, (int)point.z);
                }
            }
        }

        private void DrawCells(List<Vector3> cells)
        {
            foreach (var go in gameObjects)
            {
                Destroy(go);
            }

            gameObjects.Clear();

            foreach (var cell in cells)
            {
                var block = Instantiate(cellPrefab);
                block.transform.position = cell;
                gameObjects.Add(block);
            }
        }
    }
}