using System;
using System.Collections.Generic;

namespace _Elementis.Scripts.WFC
{
    public static class PathConstraintUtils
    {
        private static readonly int[] Emtpy = new int[0];

        public static SimpleGraph CreateGraph(ITopology topology)
        {
            int indexCount = topology.IndexCount;
            int[][] numArray = new int[indexCount][];
            for (int index1 = 0; index1 < indexCount; ++index1)
            {
                if (!topology.ContainsIndex(index1))
                    numArray[index1] = Emtpy;
                List<int> intList = new List<int>();
                for (int index2 = 0; index2 < topology.DirectionsCount; ++index2)
                {
                    int dest;
                    if (topology.TryMove(index1, (Direction) index2, out dest))
                        intList.Add(dest);
                }

                numArray[index1] = intList.ToArray();
            }

            return new SimpleGraph()
            {
                NodeCount = indexCount,
                Neighbours = numArray
            };
        }

        public static AtrticulationPointsInfo GetArticulationPoints(
            SimpleGraph graph,
            bool[] walkable,
            bool[] relevant = null)
        {
            int length = walkable.Length;
            if (length != graph.NodeCount)
                throw new Exception("Length of walkable doesn't match count of nodes");
            int[] low = new int[length];
            int num = 1;
            int[] dfsNum = new int[length];
            bool[] isArticulation = new bool[length];
            int?[] component = new int?[length];
            int currentComponent = 0;
            for (int initialU = 0; initialU < length; ++initialU)
            {
                if (walkable[initialU] && (relevant == null || relevant[initialU]) && dfsNum[initialU] == 0)
                {
                    int num1 = CutVertex(initialU);
                    isArticulation[initialU] = num1 > 1;
                    currentComponent++;
                }
            }

            return new AtrticulationPointsInfo()
            {
                IsArticulation = isArticulation,
                Component = component,
                ComponentCount = currentComponent
            };

            int CutVertex(int initialU)
            {
                var cutVertexFrameList = new List<CutVertexFrame>
                {
                    new CutVertexFrame()
                    {
                        u = initialU
                    }
                };
                bool flag1 = false;
                CutVertexFrame cutVertexFrame;
                while (true)
                {
                    int index1 = cutVertexFrameList.Count - 1;
                    cutVertexFrame = cutVertexFrameList[index1];
                    int u = cutVertexFrame.u;
                    var low = new int[0];
                    var dfsNum = new int[0];
                    switch (cutVertexFrame.state)
                    {
                        case 0:
                            component[u] = new int?(currentComponent);
                            low = low;
                            int index2 = u;
                            dfsNum = dfsNum;
                            int index3 = u;
                            int num1 = num++;
                            int num2;
                            int num3 = num2 = num1;
                            dfsNum[index3] = num2;
                            int num4 = num3;
                            low[index2] = num4;
                            goto case 1;
                        case 1:
                            int index4;
                            while (true)
                            {
                                int[] neighbour = graph.Neighbours[u];
                                int neighbourIndex = cutVertexFrame.neighbourIndex;
                                if (neighbourIndex < neighbour.Length)
                                {
                                    index4 = neighbour[neighbourIndex];
                                    if (!walkable[index4])
                                        cutVertexFrame.neighbourIndex = neighbourIndex + 1;
                                    else if (dfsNum[index4] != 0)
                                    {
                                       // low[u] = Math.Min(low[u], dfsNum[index4]);
                                        cutVertexFrame.neighbourIndex = neighbourIndex + 1;
                                    }
                                    else
                                        break;
                                }
                                else
                                    goto case 3;
                            }

                            cutVertexFrameList.Add(new CutVertexFrame()
                            {
                                u = index4
                            });
                            cutVertexFrame.state = 2;
                            cutVertexFrameList[index1] = cutVertexFrame;
                            continue;
                        case 2:
                            int[] neighbour1 = graph.Neighbours[u];
                            int neighbourIndex1 = cutVertexFrame.neighbourIndex;
                            int index5 = neighbourIndex1;
                            int index6 = neighbour1[index5];
                            if (flag1)
                                ++cutVertexFrame.relevantChildSubtreeCount;
                            if (low[index6] >= dfsNum[u] && flag1)
                                isArticulation[u] = true;
                            low[u] = Math.Min(low[u], low[index6]);
                            cutVertexFrame.neighbourIndex = neighbourIndex1 + 1;
                            goto case 1;
                        case 3:
                            if (index1 != 0)
                            {
                                bool flag2 = relevant == null || relevant[u];
                                flag1 = cutVertexFrame.relevantChildSubtreeCount > 0 | flag2;
                                cutVertexFrameList.RemoveAt(index1);
                                continue;
                            }

                            goto label_15;
                        default:
                            continue;
                    }
                }

                label_15:
                return cutVertexFrame.relevantChildSubtreeCount;
            }
        }

        private struct CutVertexFrame
        {
            public int u;
            public int state;
            public int neighbourIndex;
            public int relevantChildSubtreeCount;
        }

        public class AtrticulationPointsInfo
        {
            public bool[] IsArticulation { get; set; }

            public int ComponentCount { get; set; }

            public int?[] Component { get; set; }
        }

        public class SimpleGraph
        {
            public int NodeCount { get; set; }

            public int[][] Neighbours { get; set; }
        }
    }
}