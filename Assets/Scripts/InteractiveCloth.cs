using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]//Mesh have to be added to mesh filter
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class InteractiveCloth : MonoBehaviour {
    private Mesh originalMesh;
    private Vector3[] clothVertices;
    private ArrayList constrainedVertices;
    private ClothVertex[] cloth;
    public float clothVertexForce = 1.0f;
    public float downForce = 1.0f;
    public float drag = 1.0f;
    public float mass = 1.0f;
	// Use this for initialization
	void Start () {
        constrainedVertices = new ArrayList();
        originalMesh = GetComponent<MeshFilter>().mesh;
        clothVertices = originalMesh.vertices;
        cloth = new ClothVertex[clothVertices.Length];
        for (int i = 0; i < clothVertices.Length; i++)
        {
            if (!checkconstraints(clothVertices[i]))
                constrainedVertices.Add(i);
            cloth[i] = new ClothVertex(i, transform.localToWorldMatrix.MultiplyPoint(clothVertices[i]), clothVertexForce, downForce, drag, mass);
        }
        for (int i = 0; i < cloth.Length; i++)
        {
            cloth[i].addNeighbors(cloth, originalMesh.triangles);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        for (int i = 0; i < clothVertices.Length; i++)
        {
            if(constrainedVertices.Contains(i))
                cloth[i].vertex = transform.localToWorldMatrix.MultiplyPoint(clothVertices[i]);
        }
        GetComponent<MeshFilter>().mesh = GetComponent<MeshCollider>().sharedMesh = generateMesh(simulateCloth(), originalMesh);
    }

    private void OnCollisionEnter(Collision collision)
    {
            int nearestVertexIndex = 0;
            for(int i = 1; i < clothVertices.Length; i++)
            {
                if(Vector3.Distance(collision.transform.position, cloth[i].vertex) < Vector3.Distance(cloth[nearestVertexIndex].vertex, collision.transform.position))
                {
                    nearestVertexIndex = i;
                }
            }
            if (!constrainedVertices.Contains(nearestVertexIndex))
            {
                cloth[nearestVertexIndex].velocity = -collision.rigidbody.velocity;
            }
    }

    private Vector3[] simulateCloth()
    {
        filterVertices();
        for (int i = 0; i < clothVertices.Length; i++)
        {
            if(!constrainedVertices.Contains(i))
                clothVertices[i] = transform.worldToLocalMatrix.MultiplyPoint(cloth[i].vertex);
        }
        return clothVertices;
    }

    private Mesh generateMesh(Vector3[] vertices, Mesh originalMesh)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = originalMesh.triangles;
        mesh.uv = originalMesh.uv;
        mesh.uv2 = originalMesh.uv2;
        mesh.uv3 = originalMesh.uv3;
        mesh.uv4 = originalMesh.uv4;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        return mesh;
    }

    private void filterVertices()
    {
        for(int i = 0; i < cloth.Length; i++)
        {
            
            if (!constrainedVertices.Contains(i))
            {
                cloth[i].simulateClothVertex();
            }
        }
        for (int i = 0; i < cloth.Length; i++)
        {
            if (!constrainedVertices.Contains(i))
            {
                cloth[i].vertex += cloth[i].velocity * Time.deltaTime;
            }
        }
    }
    private bool checkconstraints(Vector3 vertex)//cloth constraints added here
    {
        return vertex.z > -5.0f&& vertex.z < 5.0f && vertex.x > -5.0f && vertex.x < 5.0f;
    }
}
