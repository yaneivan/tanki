using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tanki
{
    internal class Map
    {
        int size;
        Cell[,] cells;
        FastNoise noiseGenerator;

        public int GetMapSize() { return size; }

        public void MarkCell(int x, int y)
        {
            if (x < 0 || x > size - 1) return;
            if (y < 0 || y > size - 1) return;
            cells[y, x].GetMarked();
        }

        public void UnmarkAllCells()
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    cells[y, x].GetUnmarked();
                }
            }
        }
        public void GenerateMap(int Size)
        {
            size = Size;

            Random rnd = new Random();

            noiseGenerator = new FastNoise();
            noiseGenerator.SetSeed(rnd.Next());
            noiseGenerator.SetFrequency(0.01f);
            noiseGenerator.SetNoiseType(FastNoise.NoiseType.Perlin);
            
            cells = new Cell[size, size];
            for (int k = 0; k < size; k++)
            {
                for (int i = 0; i < size; i++)
                {
                    float perlinTmp = noiseGenerator.GetNoise(k*20, i*20);
                    System.Diagnostics.Debug.Write(string.Format("created new cell at {0}, {1} with type {2} \n", i, k, perlinTmp));
                    if (perlinTmp < -0.25f)
                    {
                        cells[k, i] = new Cell(i, k, CellType.Sand);
                    }
                    else if (perlinTmp >= -0.25f && perlinTmp <= 0.0f)
                    {
                        cells[k, i] = new Cell(i, k, CellType.Grass);
                    }
                    else if (perlinTmp > 0.0f && perlinTmp <= 0.25f)
                    {
                        cells[k, i] = new Cell(i, k, CellType.Bushes);
                    }
                    else if (perlinTmp > 0.25f)
                    {
                        cells[k, i] = new Cell(i, k, CellType.Wall);
                    }
                    //System.Diagnostics.Debug.Write(perlinMap);
                    System.Diagnostics.Debug.Write("\n");
                }
                //System.Diagnostics.Debug.Write("\n");
            }
        }

        public bool ParkingAllowed(int x, int y)
        {
            if (cells[y, x].GetCellType() != CellType.Wall) return true;
            else return false; 
        }

        public void Draw(Graphics g)
        {
            for (int k = 0; k < size; k++)
            {
                for (int i = 0; i < size; i++)
                {
                    cells[k, i].Draw(g);
                }
            }
        }

        public Position FindPositionForTank(int startXY, int stepXY)
        {
            for (int k = startXY; k < size; k += stepXY)
            {
                for (int i = k; i < size; i += stepXY)
                {
                    if (cells[k, i].GetCellType() == CellType.Grass)
                    {
                        return new Position(i, k);
                    }
                    if (cells[k, i].GetCellType() == CellType.Grass)
                    {
                        return new Position(i, k);
                    }
                }
            }
            return new Position(0, 0);
        }
        
        
    }

    struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Position a, Position b) => (a.x == b.x && a.y == b.y);
        public static bool operator !=(Position a, Position b) => (a.x != b.x || a.y != b.y);
    }

    enum CellType { Grass, Sand, Bushes, Wall }

    internal class Cell
    {
        int x, y;
        CellType type;
        bool isMarked;

        public CellType GetCellType() { return type; }

        public Cell(int x, int y, CellType type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }

        public void GetMarked() { isMarked = true; }
        public void GetUnmarked() { isMarked = false; }

        public void Draw(Graphics g)
        {
            switch ((int)type)
            {
                case 0:
                    g.FillRectangle(new SolidBrush(Color.ForestGreen), x*50, y*50, 50, 50);
                    break;
                case 1:
                    g.FillRectangle(new SolidBrush(Color.Gold), x * 50, y*50, 50, 50);
                    break;
                case 2:
                    g.FillRectangle(new SolidBrush(Color.DarkGreen), x * 50, y * 50, 50, 50);
                    break;
                case 3:
                    g.FillRectangle(new SolidBrush(Color.Crimson), x * 50, y * 50, 50, 50);
                    break;
            }

            if (isMarked) g.DrawRectangle(new Pen(Color.Orange), x * 50 + 5, y * 50 + 5, 40, 40);
        }
    }
}
