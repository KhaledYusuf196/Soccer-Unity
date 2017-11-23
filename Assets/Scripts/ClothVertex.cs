using UnityEngine;
using System.Collections;

public class ClothVertex {
    private int index;
    public Vector3 vertex;
    public Vector3 velocity;
    private Vector3 acceleration;
    private ArrayList neighbors;

    public ClothVertex(int index, Vector3 vertex)
    {
        this.index = index;
        this.vertex = vertex;
        acceleration = Vector3.zero;
        neighbors = new ArrayList();
    }

    public void addNeighbors(ClothVertex[] vertices, int[] triangles)
    {
        neighbors = new ArrayList();
        for(int i = 0; i < triangles.Length; i += 3)
        {
            if(triangles[i] == index)
            {
                neighbors.Add(new NeighborVertex(vertices[triangles[i + 1]], (vertices[triangles[i + 1]].vertex-vertex).magnitude));
                neighbors.Add(new NeighborVertex(vertices[triangles[i + 2]], (vertices[triangles[i + 2]].vertex - vertex).magnitude));
            }
            if (triangles[i+1] == index)
            {
                neighbors.Add(new NeighborVertex(vertices[triangles[i]], (vertices[triangles[i]].vertex - vertex).magnitude));
                neighbors.Add(new NeighborVertex(vertices[triangles[i + 2]], (vertices[triangles[i + 2]].vertex - vertex).magnitude));
            }
            if (triangles[i+2] == index)
            {
                neighbors.Add(new NeighborVertex(vertices[triangles[i + 1]], (vertices[triangles[i + 1]].vertex-vertex).magnitude));
                neighbors.Add(new NeighborVertex(vertices[triangles[i]], (vertices[triangles[i]].vertex - vertex).magnitude));
            }
        }
    }
    public void simulateClothVertex(float force, float drag, float mass, float downForce, Vector3 windForce, SphereCollider[] spheres)
    {
        acceleration = (mass * (downForce * Vector3.down)) + (-drag * velocity * velocity.magnitude) + windForce;
        for (int i = 0; i < neighbors.Count; i++)
        {
            NeighborVertex tmp = (NeighborVertex)neighbors[i];
            float magnitude = ((tmp.neighbor.vertex - vertex).magnitude - tmp.distance) * force;
            Vector3 direction = (tmp.neighbor.vertex - vertex).normalized;
            Vector3 distanceVector = direction * magnitude;
            acceleration += distanceVector;
        }
        acceleration /= mass;
        velocity += acceleration * Time.deltaTime;
        if (spheres != null)
        {
            for (int i = 0; i < spheres.Length ; i++)
            {
                Vector3 distance = vertex - spheres[i].transform.position;
                float radius = spheres[i].radius * Mathf.Max(spheres[i].transform.localScale.x, spheres[i].transform.localScale.y, spheres[i].transform.localScale.z);
                if (radius > distance.magnitude)
                {
                    distance = distance.normalized;
                    vertex = spheres[i].transform.position + distance * radius;
                    Rigidbody ball = spheres[i].GetComponent<Rigidbody>();
                    if(ball == null)
                    {
                        velocity = Vector3.zero;
                    }
                    else
                    {
                        float ballVelocityMagnitude = Vector3.Dot(distance, ball.velocity);
                        float velocityMagnitude = Vector3.Dot(distance, velocity);
                        Vector3 totalVelocity = (ballVelocityMagnitude * ball.mass + velocityMagnitude * mass) * distance;
                        ball.velocity -= ballVelocityMagnitude * distance;
                        velocity -= velocityMagnitude * distance;
                        ball.velocity += totalVelocity / 1.2f;
                        velocity += totalVelocity / 1.2f;
                    }
                }
            }
        }
            
        if (velocity.magnitude > 200.0f)
                velocity = (velocity.normalized) * 200.0f;
    }
}
