using UnityEngine;
using System.Collections;

public class ClothCollision : MonoBehaviour {
    // Use this for initialization
    public GameObject goal;
    private SphereCollider[] colliders;
    private ClothSphereColliderPair[] sphereColliders;
    private Cloth cloth;
    void Start () {
        cloth = GetComponent<Cloth>();
        colliders = goal.GetComponentsInChildren<SphereCollider>();
        sphereColliders = new ClothSphereColliderPair[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
        {
            
            sphereColliders[i] = new ClothSphereColliderPair();
            sphereColliders[i].first = colliders[i];
        }
        cloth.sphereColliders = sphereColliders;
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
