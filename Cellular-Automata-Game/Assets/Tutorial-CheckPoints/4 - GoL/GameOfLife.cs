﻿using System.Collections.Generic;
using UnityEngine;

namespace CA
{
    public class GameOfLife
    {
        bool[][] currentState;
        bool[][] nextState;
        public List<Vector3> Cells;
        private readonly int gridX;
        private readonly int gridY;

        public GameOfLife(int gridX, int gridY)
        {
            this.gridX = gridX;
            this.gridY = gridY;
            currentState = new bool[gridX][];
            nextState = new bool[gridX][];
            Cells = new List<Vector3>();

            // initialize the arrays
            for (int i = 0; i < gridX; i++)
            {
                currentState[i] = new bool[gridY];
                nextState[i] = new bool[gridY];
            }

            DrawGlider(10, 10);
        }

        public void Update()
        {
            for (int i = 0; i < gridX; i++)
            {
                for (int j = 0; j < gridY; j++)
                {
                    // count neighbours
                    int neighbours = 0;
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x == 0 && y == 0)
                            {
                                continue;
                            }

                            int neighbourX = (i + x + gridX) % gridX;
                            int neighbourY = (j + y + gridY) % gridY;

                            if (currentState[neighbourX][neighbourY])
                            {
                                neighbours++;
                            }
                        }
                    }

                    // Determine next state
                    if (currentState[i][j])
                    {
                        if (neighbours < 2 || neighbours > 3)
                        {
                            nextState[i][j] = false;
                        }
                        else
                        {
                            nextState[i][j] = true;
                        }
                    }
                    else
                    {
                        if (neighbours == 3)
                        {
                            nextState[i][j] = true;
                        }
                    }
                }
            }

            // Update the current state
            for (int i = 0; i < gridX; i++)
            {
                for (int j = 0; j < gridY; j++)
                {
                    currentState[i][j] = nextState[i][j];
                }
            }
        }

        public void Draw()
        {
            Cells.Clear();
            for (int i = 0; i < gridX; i++)
            {
                for (int j = 0; j < gridY; j++)
                {
                    if (currentState[i][j])
                    {
                        Cells.Add(new Vector3(i, 0, j));
                    }
                }
            }
        }

        public void DrawGlider(int i, int j)
        {
            SetAlive(i, j);
            SetAlive(i + 1, j + 1);
            SetAlive(i + 2, j - 1);
            SetAlive(i + 2, j);
            SetAlive(i + 2, j + 1);
        }

        public void DrawBlinker(int i, int j)
        {
            SetAlive(i, j);
            SetAlive(i, j + 1);
            SetAlive(i, j + 2);
        }

        public void SetAlive(int i, int j)
        {
            currentState[i][j] = true;
        }
    }
}