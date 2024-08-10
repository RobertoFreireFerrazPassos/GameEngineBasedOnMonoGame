using GameEngine.Nodes.Properties;
using System.Collections.Generic;

namespace GameEngine.Nodes;

internal class Node
{
    public Dictionary<string, Node> Nodes { get; set; } = new Dictionary<string, Node>();
   
    public Visibility Visibility { get; set; }

    public Order Ordering { get; set; }
}
