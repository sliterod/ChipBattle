using UnityEngine;
using System.Collections;

public class arrowPointerController : MonoBehaviour {

	// Use this for initialization
    public GameObject Cone;
    public GameObject Cylinder;
    private Vector3 coneStartPosition;
    private Vector3 cylinderStartPosition;
    private bool bajando;
    private bool termine;
    private Vector3 coneTopPosition;
    private Vector3 coneDownPosition;
    private Vector3 cylinderTopPosition;
    private Vector3 cylinderDownPosition;
    float step;
    float distancia;
	void Start () {
        
        bajando = false;
        distancia = 0.25f;
        coneStartPosition = Cone.transform.position;
        cylinderStartPosition = Cylinder.transform.position;

        coneTopPosition = new Vector3(coneStartPosition.x,coneStartPosition.y+distancia,coneStartPosition.z);
        coneDownPosition = new Vector3(coneStartPosition.x,coneStartPosition.y-distancia,coneStartPosition.z);
        cylinderTopPosition = new Vector3(cylinderStartPosition.x,cylinderStartPosition.y+distancia,cylinderStartPosition.z);
        cylinderDownPosition = new Vector3(cylinderStartPosition.x,cylinderStartPosition.y-distancia,cylinderStartPosition.z);
	}
	
    void Update() 
    {
        step = Time.deltaTime/10;
        if (bajando==true)
        {
            Cone.transform.position = Vector3.MoveTowards(Cone.transform.position, coneTopPosition, step);
            Cylinder.transform.position = Vector3.MoveTowards(Cylinder.transform.position, cylinderTopPosition, step);
        }
        else
        {
            Cone.transform.position = Vector3.MoveTowards(Cone.transform.position, coneDownPosition, step);
            Cylinder.transform.position = Vector3.MoveTowards(Cylinder.transform.position, cylinderDownPosition, step);
        }

        if (Cone.transform.position.y > 0.5)
        {
            bajando = false;
        }else if(Cone.transform.position.y < 0.4f){
            bajando = true;
        }

        transform.Rotate(Vector3.down * Time.deltaTime*100);


    }
    void OnCollisionEnter (Collision col)
    {
        Debug.Log(col.gameObject.name);
        if(col.gameObject.name == "player1")
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                r.enabled = false;
            }

            transform.position = new Vector3(0, 1, 0);
        }
    }
}
