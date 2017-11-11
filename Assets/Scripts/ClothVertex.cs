using UnityEngine;
using System.Collections;

public class ClothVertex {
    private int index;
    public Vector3 vertex;
    public Vector3 velocity;
    public Vector3 oldVelocity;
    private Vector3 acceleration;
    private ArrayList neighbors;
    private float drag;
    private float force;
    private float downForce;
    private float mass;

    public ClothVertex(int index, Vector3 vertex, float force, float downForce, float drag, float mass)
    {
        this.index = index;
        this.vertex = vertex;
        this.force = force;
        this.downForce = downForce;
        this.drag = drag;
        this.mass = mass;
        acceleration = Vector3.zero;
        oldVelocity = velocity = Vector3.zero;
        neighbors = new ArrayList();
    }

    public void addNeighbors(ClothVertex[] vertices, int[] triangles)
    {
        neighbors = new ArrayList();
        for(int i = 0; i < triangles.Length; i += 3)
        {
            if(triangles[i] == index)
            {
                neighbors.Add(new NeighborVertex(vertices[triangles[i + 1]], (vertices[triangles[i + 1]].vertex-vertex).sqrMagnitude));
                neighbors.Add(new NeighborVertex(vertices[triangles[i + 2]], (vertices[triangles[i + 2]].vertex - vertex).sqrMagnitude));
            }
            if (triangles[i+1] == index)
            {
                neighbors.Add(new NeighborVertex(vertices[triangles[i]], (vertices[triangles[i]].vertex - vertex).sqrMagnitude));
                neighbors.Add(new NeighborVertex(vertices[triangles[i + 2]], (vertices[triangles[i + 2]].vertex - vertex).sqrMagnitude));
            }
            if (triangles[i+2] == index)
            {
                neighbors.Add(new NeighborVertex(vertices[triangles[i + 1]], (vertices[triangles[i + 1]].vertex-vertex).sqrMagnitude));
                neighbors.Add(new NeighborVertex(vertices[triangles[i]], (vertices[triangles[i]].vertex - vertex).sqrMagnitude));
            }
        }
    }
    public void simulateClothVertex()
    {
        acceleration = (mass * (downForce*Vector3.down)) + (-drag * velocity * velocity.magnitude);
        for(int i = 0; i < neighbors.Count; i++)
        {
            NeighborVertex tmp = (NeighborVertex)neighbors[i];
            float magnitude = ((tmp.neighbor.vertex - vertex).magnitude - Mathf.Sqrt(tmp.distance))*force;
            Vector3 direction = (tmp.neighbor.vertex - vertex).normalized;
            Vector3 distanceVector = direction*magnitude;
            acceleration += distanceVector;
        }
        acceleration /= mass;
        oldVelocity = new Vector3(velocity.x, velocity.y, velocity.z);
        velocity += acceleration * Time.deltaTime;
        
        if (velocity.magnitude > 200.0f)
                velocity = (velocity.normalized) * 200.0f;
    }
}
