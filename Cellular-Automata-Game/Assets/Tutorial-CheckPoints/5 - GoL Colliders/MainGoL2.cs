using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace CA
{
    public class MainGoL2 : MonoBehaviour
    {
        public GameOfLife2 gameOfLife;
        public List<GameObject> gameObjects;
        public int Cells => gameObjects.Count;
        public GameObject cellPrefab;
        private Camera main;
        public bool AutoUpdate = true;

        private void Awake()
        {
            gameOfLife = new GameOfLife2(100, 100);
            gameObjects = new List<GameObject>();
            main = Camera.main;
        }


        private void Update()
        {
            if (AutoUpdate && Time.frameCount % 10 == 0)
            {
                UpdateGame();
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
            else if (Input.GetMouseButtonUp(2))
            {
                var ray = main.ScreenPointToRay(Input.mousePosition);
                var hits = Physics.SphereCastAll(ray, 10);
                Debug.Log($"Found {hits.Length} hits.");

                foreach (var hit in hits)
                {
                    var cellController = hit.collider.GetComponent<CellController>();
                    if (cellController != null)
                    {
                        gameObjects.Remove(cellController.gameObject);
                        cellController.Destroy();
                    }
                }
            }
        }

        public void UpdateGame()
        {
            gameOfLife.Update();
            gameOfLife.Draw();
            DrawCells(gameOfLife.Cells);
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
                block.GetComponent<CellController>().Bind(gameOfLife);
                block.transform.position = cell;
                gameObjects.Add(block);
            }
        }

        public void SpawnRandomGlider()
        {
            gameOfLife.DrawGlider(UnityEngine.Random.Range(10, 90), UnityEngine.Random.Range(10, 90));
        }

        public void SpawnRandomGun()
        {
            gameOfLife.DrawGliderGun(UnityEngine.Random.Range(10, 50), UnityEngine.Random.Range(10, 50));
        }
    }
}