using UnityEngine;
using System.Collections;

public class ClothCollisionFormation : MonoBehaviour {
    // Use this for initialization
    //private GameObject[] vertices;
    private Mesh colliderMesh;
    private Mesh objectMesh;
    private MeshCollider meshObject;
    void Start () {
        meshObject = GetComponentInChildren<MeshCollider>();
        objectMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        colliderMesh = new Mesh();
        colliderMesh.vertices = objectMesh.vertices;
        colliderMesh.triangles = objectMesh.triangles;
        //GetComponent<MeshCollider>().sharedMesh.vertices = GetComponent<Cloth>().vertices;
        meshObject.sharedMesh = colliderMesh;
        /*vertices = new GameObject[mesh.vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new GameObject();
            vertices[i].AddComponent<SphereCollider>();
            vertices[i].GetComponent<SphereCollider>().radius = 0.1f;
            vertices[i].transform.position = mesh.vertices[i];
        }*/
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //mesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        colliderMesh.vertices = GetComponent<Cloth>().vertices;
        meshObject.sharedMesh = colliderMesh;
        meshObject.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
        /*for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].transform.position = mesh.vertices[i];
        }*/
    }
}
