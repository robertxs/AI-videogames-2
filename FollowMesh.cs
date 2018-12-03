using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMesh : MonoBehaviour {

    //-------------------------------------- used in A*
    public struct NodeRecord
    {
        public Node node;
        public Connection connection;
        public float costSoFar;
        public float estimatedTotalCost;
    }

    NodeRecord startRecord = new NodeRecord();

    //List<Node> nodes;

    //List<List<Connection>> graphconnections = new List<List<Connection>>();

    public List<Vector3> followPath = new List<Vector3>();

    public GameObject RedDragon;
    public GameObject DarkDragon;
    public GameObject SmallDragon;
    //--------------------------------------



    Vector3[] vertices = null;
    public Graph graph = new Graph();


    
    void Awake () {

        followPath.Add(new Vector3(999,999,999));

        graph.nodes = new List<Node>();
        graph.connections = new List<List<Connection>>();

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        vertices = mesh.vertices;
        

        int y = 0;
        for (int i = 0; i < vertices.Length; i++) {

            if ((i + 1) % 11 != 0 && i + 11 < vertices.Length) {

                Node node = new Node();
                node.name = y;

                Vector3 point1 = vertices[i];
                Vector3 point2 = vertices[i + 11];
                Vector3 point3 = vertices[i + 12];

                Vector3 midpoint = new Vector3((point1.x + point2.x + point3.x) / 3, 0, (point1.z + point2.z + point3.z) / 3);

                node.position = midpoint * 8 + transform.position;
                //if (y != 80 && y != 81 && y != 82 && y != 100 && y != 101 && y != 102) { 
                    graph.nodes.Add(node);
                //}
                y = y + 1;

                Node tnode = new Node();
                tnode.name = y;

                Vector3 tpoint1 = vertices[i];
                Vector3 tpoint2 = vertices[i + 1];
                Vector3 tpoint3 = vertices[i + 12];

                Vector3 tmidpoint = new Vector3((tpoint1.x + tpoint2.x + tpoint3.x) / 3, 0, (tpoint1.z + tpoint2.z + tpoint3.z) / 3);

                tnode.position = tmidpoint * 8 + transform.position;
                //if (y != 80 && y != 81 && y != 82 && y != 100 && y != 101 && y != 102){
                    graph.nodes.Add(tnode);
                //}
                y = y + 1;
            }
        }


        for (int i = 0; i < graph.nodes.Count; i++)
        {
            graph.connections.Add(new List<Connection>());
        }
        
        

        for (int i = 0; i < graph.nodes.Count; i++)
        {
            graph.connections.Add(new List<Connection>());


            for (int j = 0; j < graph.nodes.Count; j++)
            {
                if (i != j && (graph.nodes[j].position - graph.nodes[i].position).magnitude <= 5.97f) { 
                    Connection c1 = new Connection();
                   
                    c1.set(graph.nodes[i], graph.nodes[j], (graph.nodes[j].position - graph.nodes[i].position).magnitude);
                   
                    Debug.DrawLine(graph.nodes[i].position, graph.nodes[j].position, Color.red, 50f);
               
                    graph.connections[i].Add(c1);
                
                }
            }
        }
    }


   // void OnDrawGizmos() {

     //   foreach (Node node in graph.nodes)
     //   {
      //      Gizmos.color = Color.blue;
     //       Gizmos.DrawWireSphere(node.position, 1f);
      //  }

      //  return;
   // }

    public int end;
    public GameObject dragon;

    public void Go(GameObject drago)
    {
        dragon = drago;
        int start = getDragonNode(drago);
        

        //nodes = graph.nodes;

        startRecord.node = graph.nodes[start];
        startRecord.connection = null;
        startRecord.costSoFar = 0;
        startRecord.estimatedTotalCost = Heuristic(graph.nodes[start].position, graph.nodes[end].position);

        List<NodeRecord> open = new List<NodeRecord>();
        open.Add(startRecord);
        List<NodeRecord> closed = new List<NodeRecord>();

        List<Connection> connections = new List<Connection>();

        //graphconnections = graph.connections;


        for (int i = 0; i < graph.nodes.Count; i++)
        {
            for (int j = 0; j < graph.connections[i].Count; j++)
            {
                if (graph.connections[i][j].cost != 0)
                {
                    connections.Add(graph.connections[i][j]);
                }
            }
        }

        NodeRecord current = new NodeRecord();

        int[] predecessor = new int[graph.nodes.Count];
        List<int> path = new List<int>();


        while (open.Count > 0)
        {
            current = open[0];
            for (int i = 0; i < open.Count; i++)
            {
                if (open[i].estimatedTotalCost < current.estimatedTotalCost)
                {
                    current = open[i];
                }
            }

            if (current.node.name == end)
            {
                break;
            }

            List<Connection> actualConnections = new List<Connection>();
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].from == current.node)
                {
                    actualConnections.Add(connections[i]);
                }
            }

            foreach (Connection connect in actualConnections)
            {
                float endNodeCost = current.costSoFar + connect.cost;
                float endNodeHeuristic;

                bool closedcontains = false;
                NodeRecord endNodeRecord = new NodeRecord();

                for (int i = 0; i < closed.Count; i++)
                {
                    if (closed[i].node == connect.to)
                    {
                        closedcontains = true;
                        endNodeRecord = closed[i];
                    }
                }

                bool opencontains = false;

                for (int i = 0; i < open.Count; i++)
                {
                    if (open[i].node == connect.to)
                    {
                        opencontains = true;
                        endNodeRecord = open[i];
                    }
                }

                if (closedcontains == true)
                {
                    if (endNodeRecord.costSoFar <= endNodeCost)
                    {
                        continue;
                    }
                    else if (endNodeRecord.costSoFar > endNodeCost)
                    {
                        closed.Remove(endNodeRecord);
                    }

                    endNodeHeuristic = Heuristic(connect.to.position, graph.nodes[end].position);

                }
                else if (opencontains == true)
                {
                    if (endNodeRecord.costSoFar <= endNodeCost)
                    {
                        continue;
                    }

                    endNodeHeuristic = Heuristic(connect.to.position, graph.nodes[end].position);

                }
                else
                {
                    endNodeRecord = new NodeRecord();
                    endNodeRecord.node = connect.to;
                    endNodeHeuristic = Heuristic(connect.to.position, graph.nodes[end].position);
                }

                endNodeRecord.costSoFar = endNodeCost;
                endNodeRecord.connection = connect;
                endNodeRecord.estimatedTotalCost = endNodeCost + endNodeHeuristic;

                predecessor[connect.to.name] = connect.from.name;

                if (opencontains == false)
                {
                    open.Add(endNodeRecord);
                }

            }

            open.Remove(current);
            closed.Add(current);
        }


        path.Add(graph.nodes[end].name);
        int node = predecessor[graph.nodes[end].name];
        path.Add(node);

        while (node != start)
        {
            node = predecessor[node];
            path.Add(node);
        }

        path.Reverse();

        followPath = new List<Vector3>();
        for (int i = 0; i < path.Count; i++)
        {
            followPath.Add(graph.nodes[path[i]].position);
        }

    }

    float Heuristic(Vector3 position1, Vector3 position2)
    {
        return (position2 - position1).magnitude;
    }

    public int currentPathNode = 0;
    public bool canbeupdated = true;

    public void Update()
    {
        //------------------------------------------------------------------------------

        Vector3 target;

        if (followPath[0] != new Vector3(999,999,999))
        {
            canbeupdated = false;
           
            target = followPath[currentPathNode];
          
            dragon.transform.rotation = Quaternion.Slerp(dragon.transform.rotation, Quaternion.LookRotation(target - dragon.transform.position), 0.03F);
            float speed = 8f;
            if (currentPathNode == followPath.Count - 1)
                dragon.transform.Translate((target - dragon.transform.position).normalized * Time.deltaTime * speed / 2, Space.World);
            else
            {
                dragon.transform.Translate((target - dragon.transform.position).normalized * Time.deltaTime * speed, Space.World);
            }
            if ((target - dragon.transform.position).magnitude <= 0.2f)
            {
                currentPathNode += 1;

                if (currentPathNode >= followPath.Count)
                {
                    followPath = null;
                    followPath = new List<Vector3>();
                    followPath.Add(new Vector3(999, 999, 999));
                    currentPathNode = 0;
                    canbeupdated = true;
                }
            }
        }
    }

    public int getDragonNode(GameObject dragon) {

        int actualNode = 0;
        float minDistance = (graph.nodes[0].position - dragon.transform.position).magnitude;

        for (int i = 1; i < graph.nodes.Count; i++) {
            float distance = (graph.nodes[i].position - dragon.transform.position).magnitude;
            if (distance < minDistance) {
                minDistance = distance;
                actualNode = i;
            }
        }

        return actualNode;
    }

}




