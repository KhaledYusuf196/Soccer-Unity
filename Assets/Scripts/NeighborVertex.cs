using UnityEngine;
using System.Collections;

public class NeighborVertex {

    public ClothVertex neighbor;
    public float distance;
    public NeighborVertex(ClothVertex v, float d)
    {
        neighbor = v;
        distance = d;
    }

}
