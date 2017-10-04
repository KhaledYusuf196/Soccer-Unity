using UnityEngine;
using System.Collections;

public class ClothSimulation : MonoBehaviour {
    public GameObject ls;
    public GameObject us;
    public GameObject rs;
    public GameObject ds;
    public float force;
    public float constraint;
    Vector3 dist;
    //GameObject[] go;
    //LineRenderer[] lr;
    // Use this for initialization
    void Start () {
        if (ls != null)
        {
            //go = new GameObject[3];
            //lr = new LineRenderer[3];
            if(ls != null)
            {
                //go[0] = new GameObject();
                /*lr[0] = go[0].AddComponent<LineRenderer>();
                lr[0].SetWidth(0.1f, 0.1f);*/
            }
            if(us != null)
            {
                //go[1] = new GameObject();
                /*lr[1] = go[1].AddComponent<LineRenderer>();
                lr[1].SetWidth(0.1f, 0.1f);*/
            }
            if(rs != null)
            {
                //go[2] = new GameObject();
                /*lr[2] = go[2].AddComponent<LineRenderer>();
                lr[2].SetWidth(0.1f, 0.1f);*/
            }
            

        }
        }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        if (ls != null)
        {
            /*lr[0].SetPosition(0, transform.position);
            lr[0].SetPosition(1, ls.transform.position);*/
            //Gizmos.DrawLine(transform.position, ls.transform.position);
            Debug.DrawLine(transform.position, ls.transform.position);
            if((ls.transform.position - transform.position).magnitude > constraint)
            {
                dist = (ls.transform.position - transform.position).normalized * constraint;
                GetComponent<Rigidbody>().velocity += (ls.transform.position - transform.position - dist) * Time.deltaTime * 32 *force;
            }
            
        }
        
        if (us != null)
        {
            /*lr[1].SetPosition(0, transform.position);
            lr[1].SetPosition(1, us.transform.position);*/
            Debug.DrawLine(transform.position, us.transform.position);
            if ((us.transform.position - transform.position).magnitude > constraint)
            {
                dist = (us.transform.position - transform.position).normalized * constraint;
                GetComponent<Rigidbody>().velocity += (us.transform.position - transform.position - dist) * Time.deltaTime * 32*force;
            }
            
        }

        if (rs != null)
        {
            /*lr[2].SetPosition(0, transform.position);
            lr[2].SetPosition(1, rs.transform.position);*/
            Debug.DrawLine(transform.position, rs.transform.position);
            if ((rs.transform.position - transform.position).magnitude > constraint)
            {
                dist = (rs.transform.position - transform.position).normalized * constraint;
                GetComponent<Rigidbody>().velocity += (rs.transform.position - transform.position - dist) * Time.deltaTime * 32*force;
            }
            
        }

        if (ds != null)
        {
            Debug.DrawLine(transform.position, ds.transform.position);
            if ((ds.transform.position - transform.position).magnitude > constraint)
            {
                dist = (ds.transform.position - transform.position).normalized * constraint;
                GetComponent<Rigidbody>().velocity += (ds.transform.position - transform.position - dist) * Time.deltaTime * 32*force;
            }
        }

        GetComponent<Rigidbody>().velocity -= (GetComponent<Rigidbody>().velocity *Time.deltaTime);
    }

}

