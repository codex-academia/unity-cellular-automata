using System.Collections.Generic;
using UnityEngine;

namespace CA
{
    public class Main2 : MonoBehaviour
    {
        public GameObject cellPrefab;
        private void Start()
        {
            List<Transform> cells = new List<Transform>();
            // Rule 110
            bool[] rule = new bool[8] { false, true, true, true, false, true, true, false };

            int columns = 100;

            int maxRows = 100;

            // Set the Initial State last is alive
            bool[] currentState = new bool[columns];
            bool[] nextState = new bool[columns];
            for (int i = 0; i < columns; i++)
            {
                currentState[i] = false;
            }
            currentState[columns - 1] = true;

            // Iterate over the rows
            for (int row = 0; row < maxRows; row++)
            {
                // Find the next State
                for (int i = 0; i < columns; i++)
                {
                    int a = (i - 1 + columns) % columns;
                    int b = i;
                    int c = (i + 1 + columns) % columns;

                    int num = 0;

                    // Convert the binary number to decimal
                    if (currentState[a])
                    {
                        num += 4; // 2^2
                    }
                    if (currentState[b])
                    {
                        num += 2; // 2^1
                    }
                    if (currentState[c])
                    {
                        num += 1; // 2^0
                    }

                    // Apply the rule
                    nextState[b] = rule[num];
                }

                // Draw the current state
                for (int i = 0; i < columns; i++)
                {
                    if (currentState[i])
                    {
                        var block = Instantiate(cellPrefab);
                        block.name = $"Cell_{i}_{row}";
                        Transform cell = block.transform;
                        cell.position = new Vector3(i, 0, -row);
                        cells.Add(cell);
                    }
                }

                // Update the current state
                for (int i = 0; i < columns; i++)
                {
                    currentState[i] = nextState[i];
                }

            }



        }
    }
}
