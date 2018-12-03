using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node{
    public int name;
    public Vector3 position;
}

public class Connection{
    public Node from;
    public Node to;
    public float cost;

    public void set(Node from, Node to, float cost) {
        this.from = from;
        this.to = to;
        this.cost = cost;
    }
}

public class Graph
{
    public List<Node> nodes;
    public List<List<Connection>> connections;
}


