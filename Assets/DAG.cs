using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Node {
    public int id;
    public Vector3 position;
    public List<int> edges;
}
public class DAG : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();
    public int nodeCount;
    public int maxBranchPerNode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateOrigin();
        GenerateInitTree();
        AddBranches();
        DebugLRBetweenNodes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateInitTree() {
        for (int i = 1; i < nodeCount; i++) {
            Node n = GenerateRandomNode(i, nodes[i-1].position);
            nodes.Add(n);
            nodes[i-1].edges.Add(n.id);
        }
    }

    void GenerateOrigin() {
        Node n = new Node();
        n.id = 0;
        n.position = new Vector3(0.0f, 0.0f, 0.0f);
        n.edges = new List<int>();
        nodes.Add(n);
    }

    Node GenerateRandomNode(int i, Vector3 parentPos) {
        float x = Random.Range(parentPos.x - 1.0f, parentPos.x + 1.0f);
        float y = Random.Range(parentPos.y, parentPos.y + 10.0f);
        float z = Random.Range(parentPos.z - 1.0f, parentPos.z + 1.0f);

        Node n = new Node();
        n.id = i;
        n.position = new Vector3(x, y, z);
        n.edges = new List<int>();
        return n;
    }

    void AddBranches(){
        int currentNodeCount = nodes.Count;
        for (int i = 1; i < currentNodeCount; i++) {
            int branchCount = Random.Range(0, maxBranchPerNode + 1);
            Vector3 parentPos = nodes[i].position;
            for (int j = 0; j < branchCount; j++) {
                float x = Random.Range(parentPos.x - 5.0f, parentPos.x + 5.0f);
                float y = Random.Range(parentPos.y - 1.0f, parentPos.y + 1.0f);
                float z = Random.Range(parentPos.z - 5.0f, parentPos.z + 5.0f);

                Node n = new Node();
                n.id = nodes.Count;
                n.position = new Vector3(x, y, z);
                n.edges = new List<int>();
                nodes.Add(n);
                nodes[i].edges.Add(n.id);
            }
        }
    }

    void DebugLRBetweenNodes(){        
        for (int i = 0; i < nodes.Count; i++) {
            Vector3 n = nodes[i].position;
            foreach (int e in nodes[i].edges) {
                Debug.DrawLine(n, nodes[e].position, Color.green, 100.0f);
                Debug.Log(n + nodes[e].position);
            }
        }
    }
}
