using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerScripts
{
    public class Combat : MonoBehaviour
    {
        [SerializeField] private long value = 100000;
        private List<string> words = new List<string>{"погром", "гром"};
        private List<string> Dic = new List<string> {"гром", "ром",  "рог",  "Морг",  "огр",  "моР",  "порог"};

        static int[][] gridA =
        {
            new[] {1, 3, 1, 0, 1, 1},
            new[] {2, 1, 1, 0, 1, 1},
            new[] {1, 1, 1, 0, 0, 0},
            new[] {1, 1, 1, 1, 1, 1},
            new[] {1, 1, 1, 1, 1, 1},
            new[] {1, 1, 1, 1, 1, 1},
        };

       

        private List<int> values = new List<int> {2, 9, 3, 5, 5, 2, 3, 4, 6, 1, 6};
        char c = ' ';
        private char f = 'K';

        private void Awake()
        {
            print(FindPath(gridA, 0, 0, 5, 2, 30));

        }

        private static List<int> GetWordSubWords(List<string> words, List<string> wordDictionary)
        {
            List<int> result = new List<int>();
            for (int x = 0; x < words.Count; x++)
            {
                int coincidence = 0;

                for (int y = 0; y < wordDictionary.Count; y++)
                {
                    var currentWords = words[x].ToLower();
                    var currentDic = wordDictionary[y].ToLower();
                    int currentCoincidence = 0;
                    for (int i = 0; i < currentDic.Length; i++)
                    {
                        for (int j = 0; j < currentWords.Length; j++)
                        {
                            if (currentDic[i] == currentWords[j])
                            {
                                currentCoincidence++;
                                currentWords = currentWords.Remove(j, 1);
                                break;
                            }
                        }
                    }

                    if (currentCoincidence == currentDic.Length)
                    {
                        coincidence++;
                    }
                }

                result.Add(coincidence);
            }


            return result;
        } // gotovo

        public static int FindPath(int[][] gridMap, int sX, int sY, int eX, int eY, int energyAmount)
        {
            List<Nod> CheckedNodes = new List<Nod>();
            List<Nod> WaitingNodes = new List<Nod>();
            int path = 0;
            var pathToTarget = new List<Vector2>();
            if (sX == eX && sY == eY)
                return 0;
            Nod startNode = new Nod(0, sX,sY,eX,eY, null);
            CheckedNodes.Add(startNode);
            WaitingNodes.AddRange(GetNeighboursNodes(startNode));
            while (WaitingNodes.Count>0)
            {
                Nod nodetocheck = WaitingNodes.FirstOrDefault(x => x.F == WaitingNodes.Min(y => y.F)); 
                if (nodetocheck.Xposition == eX && nodetocheck.Yposition== eY)
                    return path = CalculatePath(nodetocheck);
                // var walkable = !Physics2D.OverlapCircle(new Vector2(nodetocheck.Position.x + 0.5f, nodetocheck.Position.y + 0.5f),0.05f, _layerMask);
                // if (!walkable)
                // {
                //     WaitingNodes.Remove(nodetocheck);
                //     CheckedNodes.Add(nodetocheck);
                // }
                // else
                // {
                //     WaitingNodes.Remove(nodetocheck);
                //     if (CheckedNodes.All(x => x.Position != nodetocheck.Position))
                //     {
                //         CheckedNodes.Add(nodetocheck);
                //         WaitingNodes.AddRange(GetNeighboursNodes(nodetocheck));
                //     }
                //
                // }
            }
          
                
            return path;
        }
//         foreach(int[] row in numbers)
//         {
//             foreach(int number in row)
//             {
//                 Console.Write($"{number} \t");
//             }
//             Console.WriteLine();
//         }
//  
// // перебор с помощью цикла for
//     for (int i = 0; i<numbers.Length;i++)
//     {
//         for (int j =0; j<numbers[i].Length; j++)
//         {
//             Console.Write($"{numbers[i][j]} \t");
//         }
//         Console.WriteLine();
//     }


     
        public static string  FormatPrettyCoins(long value, char separator)
        {
            
            string separ = new string(separator,1);
            var test = value.ToString();
            if(value<1||test.Length>10)
            {
                throw new Exception("Недопустимые значения");
            }

           
            if (test.Length >= 3 && test.Length <= 6)
            {
                test = test.Insert(test.Length - 3, separ);
                
            }

            else if (test.Length > 6 && test.Length < 8)
            {
                test = test.Remove(test.Length - 3, 3);
                test = test.Insert(test.Length - 3, separ);
                test= string.Concat(test,"K");
            }
            else if (test.Length >= 8 && test.Length < 11)
            {
                test = test.Remove(test.Length - 6, 6);
                test = test.Insert(test.Length - 3, separ);
                test= string.Concat(test,"M");
            }

            return test;
        } // gotovo


        public static int FindMaxRect(List<int> heights)
        {
            var sort = heights.ToArray();
            Array.Sort(sort);
            if (heights.Count > 10000 || heights.Count < 1 || sort[0] < 1 || sort[sort.Length - 1] > 10000)
            {
                throw new Exception("Недопустимые значения или длина");
            }

            int[] square = new int[heights.Count];
            for (int i = 0; i <= heights.Count-1; i++) 
            {
                var shirina = 1;
                for (int j = i; j < heights.Count-1; j++) 
                {
                    if (heights[j + 1] >= heights[i])
                        shirina += 1;
                    else
                        break;
                }

                for (int j = i; j >0; j--)
                {
                    if (heights[j - 1] >= heights[i])
                        shirina += 1;
                    else
                        break;
                }

                square[i] = heights[i] * shirina;

            }
            Array.Sort(square);

            return square[square.Length-1];
        } // gotovo

        private static List<Nod> GetNeighboursNodes(Nod nod)
        {
            List<Nod> Neighbors = new List<Nod>();
            if (nod.Xposition - 1 >= 0&&gridA[nod.Xposition-1][nod.Yposition]!=0)
                Neighbors.Add(new Nod(nod.G+1,nod.Xposition-1,nod.Yposition,nod.XtargetPosition,nod.YtargetPositin,nod ));
            if (nod.Xposition+1<gridA.Length&&gridA[nod.Xposition+1][nod.Yposition]!=0)
                Neighbors.Add(new Nod(nod.G+1,nod.Xposition+1,nod.Yposition,nod.XtargetPosition,nod.YtargetPositin,nod));
            if(nod.Yposition+1<gridA.Length&&gridA[nod.Xposition][nod.Yposition+1]!=0)
                Neighbors.Add(new Nod(nod.G+1,nod.Xposition,nod.Yposition+1,nod.XtargetPosition,nod.YtargetPositin,nod));
            if(nod.Yposition-1>0&&gridA[nod.Xposition][nod.Yposition-1]!=0)
                Neighbors.Add(new Nod(nod.G+1,nod.Xposition,nod.Yposition-1,nod.XtargetPosition,nod.YtargetPositin,nod));
           

            return Neighbors;
        }
        private static int CalculatePath(Nod node)
        {
            int step = 0;
            var path = new List<int>();
            Nod currentNode = node;
            while (currentNode.PreviosNode !=null)
            {
                // path.Add(new Vector2(currentNode.Position.x+0.5f,currentNode.Position.y+0.5f));
                // currentNode = currentNode.PreviosNode;
                step++;
            }
        
        
            return step;
        }
  
    }
    public class Nod
    {
        public int Xposition;
        public int Yposition;
        public int XtargetPosition;
        public int YtargetPositin;
        public Nod PreviosNode;
        public int F;
        public int G;
        public int H;
        
        public Nod(int g, int _XnodePosition,int _YnodePosition, int _XtargetPosition,int _YtargetPosition, Nod previousNode)
        {
            Xposition = _XnodePosition;
            Yposition = _YnodePosition;
            XtargetPosition = _XtargetPosition;
            YtargetPositin = _YtargetPosition;
            PreviosNode = previousNode;
            G = g;
            H =  Math.Abs(_XtargetPosition - _XnodePosition) +  Math.Abs(_YtargetPosition - _YnodePosition);
            F = G + H;

        }
    }
}