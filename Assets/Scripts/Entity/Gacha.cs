using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Model
{
    [Serializable]
    public class Gacha
    {
        public string Id;
        public string Name;

        public Node Nodes;

        public Gacha()
        {
            Nodes = new Node();
        }

        [Serializable]
        public class Node
        {
            public float Prob;
            public List<Node> NextNode = new List<Node>();
            public List<ItemNode> Nodes = new List<ItemNode>();
        }

        [Serializable]
        public class ItemNode
        {
            public ItemSO Item;
            public float Prob;
        }

        public string ToJson()
        {
            var json = JsonUtility.ToJson(Nodes, true);
            Debug.Log(json);
            return json;
        }

        public float GetFloat()
        {
            System.Random rd = new System.Random();
            return (float)rd.NextDouble() * 100f;
        }

        public ItemSO GetItemSO()
        {
            float rd = GetFloat();
            float index = 0f;

            foreach (Node n in Nodes.NextNode)
            {
                index += n.Prob;
                if (rd <= index)
                {
                    Debug.Log($"{rd}");

                    rd = GetFloat();
                    index = 0f;

                    foreach (ItemNode m in n.Nodes)
                    {
                        index += m.Prob;
                        if (rd <= index)
                        {
                            return m.Item;
                        }
                    }
                }
            }

            return null;
        }

    }
}
