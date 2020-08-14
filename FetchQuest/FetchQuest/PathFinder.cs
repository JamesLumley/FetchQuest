using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FetchQuest
{
    //CLASS CREATED BY CHRIS
    static class PathFinder
    {
        #region Declarations
        private enum NodeStatus { Open, Closed };
        private static Dictionary<Vector2, NodeStatus> nodeStatus = new Dictionary<Vector2, NodeStatus>();

        private const int CostStraight = 10;
        private const int CostDiagonal = 15;

        private static List<PathNode> openList = new List<PathNode>();
        private static Dictionary<Vector2, float> nodeCosts = new Dictionary<Vector2, float>();
        #endregion

        #region Helper Methods

        static private void addNodeToOpenList(PathNode node)//Adds open nodes the to node list
        {
            int index = 0;
            float cost = node.TotalCost;

            while ((openList.Count() > index) && (cost < openList[index].TotalCost))
            {
                index++;
            }
            openList.Insert(index, node);
            nodeCosts[node.GridLocation] = node.TotalCost;
            nodeStatus[node.GridLocation] = NodeStatus.Open;
        }


        static private List<PathNode> findAdjacentNodes(//Finds all adjacent nodes for the pathfinding
    PathNode currentNode,
    PathNode endNode)
        {
            List<PathNode> adjacentNodes = new List<PathNode>();

            int X = currentNode.GridX;
            int Y = currentNode.GridY;

            int NumTilesX = TileMap.MapWidth;
            int NumTilesY = TileMap.MapHeight;

            bool upLeft = true;
            bool upRight = true;
            bool downLeft = true;
            bool downRight = true;

            if ((X > 0) && (TileMap.IsFloorTile(X - 1, Y)))
            {
                adjacentNodes.Add(new PathNode(
                        currentNode,
                        endNode,
                        new Vector2(X - 1, Y),
                        CostStraight + currentNode.DirectCost));
            }
            else
            {
                upLeft = false;
                downLeft = false;
            }

            if ((X < NumTilesX - 1) && (TileMap.IsFloorTile(X + 1, Y)))
            {
                adjacentNodes.Add(new PathNode(
                        currentNode,
                        endNode,
                        new Vector2(X + 1, Y),
                        CostStraight + currentNode.DirectCost));
            }
            else
            {
                upRight = false;
                downRight = false;
            }


            if ((Y > 0) && (TileMap.IsFloorTile(X, Y - 1)))
            {
                adjacentNodes.Add(new PathNode(
                    currentNode,
                    endNode,
                    new Vector2(X, Y - 1),
                    CostStraight + currentNode.DirectCost));
            }
            else
            {
                upLeft = false;
                upRight = false;
            }

            if ((Y < NumTilesY - 1) && (TileMap.IsFloorTile(X, Y + 1)))
            {
                adjacentNodes.Add(new PathNode(
                    currentNode,
                    endNode,
                    new Vector2(X, Y + 1),
                    CostStraight + currentNode.DirectCost));
            }
            else
            {
                downLeft = false;
                downRight = false;
            }


            if ((upLeft) && (TileMap.IsFloorTile(X - 1, Y - 1)))
            {
                adjacentNodes.Add(new PathNode(
                    currentNode,
                    endNode,
                    new Vector2(X - 1, Y - 1),
                    CostDiagonal + currentNode.DirectCost));
            }

            if ((upRight) && (TileMap.IsFloorTile(X + 1, Y - 1)))
            {
                adjacentNodes.Add(new PathNode(
                    currentNode,
                    endNode,
                    new Vector2(X + 1, Y - 1),
                    CostDiagonal + currentNode.DirectCost));
            }

            if ((downLeft) && (TileMap.IsFloorTile(X - 1, Y + 1)))
            {
                adjacentNodes.Add(new PathNode(
                    currentNode,
                    endNode,
                    new Vector2(X - 1, Y + 1),
                    CostDiagonal + currentNode.DirectCost));
            }

            if ((downRight) && (TileMap.IsFloorTile(X + 1, Y + 1)))
            {
                adjacentNodes.Add(new PathNode(
                    currentNode,
                    endNode,
                    new Vector2(X + 1, Y + 1),
                    CostDiagonal + currentNode.DirectCost));
            }

            return adjacentNodes;
        }


        #endregion

        #region Public Methods

        static public List<Vector2> FindPath(//Finds the path for object to take
            Vector2 startTile, Vector2 endTile)
        {
            if ((!(TileMap.IsFloorTile(endTile))) || (!(TileMap.IsFloorTile(startTile))))
            { return null; }

            openList.Clear();
            nodeCosts.Clear();
            nodeStatus.Clear();
            PathNode startNode;
            PathNode endNode;

            endNode = new PathNode(null, null, endTile, 0);
            startNode = new PathNode(null, endNode, startTile, 0);
            addNodeToOpenList(startNode);

            while (openList.Count > 0)
            {
                PathNode currentNode = openList[openList.Count - 1];
                if (currentNode.IsEqualToNode(endNode))
                {
                    List<Vector2> bestPath = new List<Vector2>();
                    while (currentNode != null)
                    {
                        bestPath.Insert(0, currentNode.GridLocation);
                        currentNode = currentNode.ParentNode;
                    }
                    return bestPath;
                }
                openList.Remove(currentNode);
                nodeCosts.Remove(currentNode.GridLocation);

                foreach (PathNode possibleNode in findAdjacentNodes(currentNode, endNode))
                {
                    if (nodeStatus.ContainsKey(possibleNode.GridLocation))
                    {
                        if (nodeStatus[possibleNode.GridLocation] == NodeStatus.Closed)
                        { continue; }
                        if (nodeStatus[possibleNode.GridLocation] == NodeStatus.Open)
                        {
                            if (possibleNode.TotalCost >= nodeCosts[possibleNode.GridLocation])
                            { continue; }
                        }
                    }
                    addNodeToOpenList(possibleNode);
                }
                nodeStatus[currentNode.GridLocation] = NodeStatus.Closed;
            }
            return null;
        }
        #endregion
    }
}
