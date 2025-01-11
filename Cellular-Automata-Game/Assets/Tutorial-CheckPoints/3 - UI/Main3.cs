using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA
{
    public class Main3 : MonoBehaviour
    {
        public GameObject cellPrefab;
        public int columns = 100;
        public int maxRows = 100;

        private List<GameObject> cells;

        // Rule 110
        private bool[] rule = new bool[8] { false, true, true, true, false, true, true, false };

        private void Start()
        {
            Create();
        }

        public void ReCreate()
        {
            StartCoroutine(ReCreateI());
        }
        public IEnumerator ReCreateI()
        {
            int count = 0;
            foreach (var cell in cells)
            {
                Destroy(cell);
                if (count % 50 == 0)
                {
                    yield return new WaitForEndOfFrame();
                }

                count++;
            }

            Create();
        }

        public void SetRule(int ruleNumber)
        {
            if (ruleNumber < 0 || ruleNumber > 255)
            {
                throw new ArgumentException("Rule number must be between 0 and 255");
            }

            for (int i = 0; i < 8; i++)
            {
                rule[i] = (ruleNumber & (1 << i)) != 0;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space key was pressed.");
                StartCoroutine(ReCreateI());
            }

            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                SetRule(110);
                StartCoroutine(ReCreateI());
            }

            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                SetRule(30);
                StartCoroutine(ReCreateI());
            }
        }

        private void Create()
        {
            cells = new List<GameObject>();


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
                        cells.Add(block);
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