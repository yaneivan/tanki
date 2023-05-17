using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanki
{
    struct Node
    {                              //как сделать нормально шоб возвращало значения
        int x;
        int y;

        int Gcost; //distance from a starting node
        int Hcost; //distance to an end node
        int Fcost; //full cost
        public Node(int x, int y, int Gcost, int Hcost)
        {
            this.x = x;
            this.y = y;
            this.Gcost = Gcost;
            this.Hcost = Hcost;
            this.Fcost = Gcost + Hcost;
        }
        
        public int GetX() { return x; }
        public int GetY() { return y; }
        public int GetGcost() { return Gcost; }
        public int GetHcost() { return Hcost; }
        public int GetFcost() { return Fcost; } 
    }

    
    internal class AStarPathFinding
    {
        List<Node> openNodes = new List<Node>();
        List<Node> closedNodes = new List<Node>();

        public AStarPathFinding() { }

        public void FindPath(Node startNode, Node endNode)
        {
            openNodes.Add(startNode);
            SortOpenNodes();
            CloseNode(openNodes[0]);
        }

        void SortOpenNodes()
        {

        }

        void CloseNode(Node node)
        {
            //remove from oepnNodes
        }

        int FindManhattanDistance(Node startNode, Node endNode)
        {
            return Abs(startNode.GetX() - endNode.GetX()) + Abs(startNode.GetY() - endNode.GetY());
        }

        int Abs(int value)
        {
            if (value >= 0) return value;
            else return -value;
        }
    }
}
