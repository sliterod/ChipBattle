using UnityEngine;
using System.Collections;

public class StraightShot : Projectile {

    private static float START_POINT_SPEED = 100;
    private static float END_POINT_SPEED = 60;
    private static float SCALE_SPEED = 1.2f;

    private float endPointDelay = 0.1f;
    private float lineScale = 0.25f;

    int direcction = 1; //Tells if the projectile if going form right to left or the otherwise

    private LineRenderer lineRenderer;

    Vector3 endPoint;


    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, this.transform.position);
        lineRenderer.SetPosition(1, endPoint);
        damage = 20;
    }
	
	// Update is called once per frame
	void Update () {
        //If the projectile was launched
        if (isActive)
        {
            /*
            //Head of the projectile and starting point of the line
            float step = START_POINT_SPEED * Time.deltaTime * direcction;
            lineRenderer.SetPosition(0, transform.position);


            //Tail of the projectile and final point of the line
            if(endPointDelay <= 0)
            {
                step = END_POINT_SPEED * Time.deltaTime * direcction;
                endPoint.x += step;
                lineRenderer.SetPosition(1, endPoint);
                if (Vector3.Distance(endPoint, transform.position) < step)
                {
                    KillSelf();
                }
            }
            else
            {
                endPointDelay -= Time.deltaTime;
            }
            */
            lineScale = Mathf.Clamp01(lineScale - (Time.deltaTime * SCALE_SPEED));
            lineRenderer.SetWidth(lineScale, lineScale);
            if(lineScale == 0)
            {
                KillSelf();
            }            
            
            
        }
    }

    /// <summary>
    /// Tells the projectile to start moving at the default speed
    /// </summary>
    /// <param name="side">Indicates if the projectile is shot from the Blue o Red side of the stage</param>
    public void Launch(StageSide side)
    {
        //First we check in wich side of the stage is the projectile
        if (side == StageSide.red)
        {
            //We chance the speed so it moves form right to left
            direcction *= -1;
        }
        //then we raise the Active flag so it can move
        isActive = true;
        endPoint = this.transform.position;
        CheckHit(side);
    }

    void OnBecameInvisible()
    {
        //When the projectile left the visible space we destroy it
        KillSelf();
    }

    /// <summary>
    /// This is a super quick projectile so the hit is determined by a raycast and not by hitboxes
    /// this method cast said raycast and checks if it hit something
    /// </summary>
    void CheckHit(StageSide side)
    {
        bool raycastHit;
        RaycastHit hitInfo = new RaycastHit();
        Ray ray;
        if(side == StageSide.blue)
        {
            ray = new Ray(this.transform.position, Vector3.right);
            //Cast a ray that only hit objects on the red side
            raycastHit = Physics.Raycast(ray, out hitInfo, 2000, 1 << 9);
        }
        else
        {
            ray = new Ray(this.transform.position, Vector3.left);
            //Cast a ray that only hit objects on the blue side
            raycastHit = Physics.Raycast(ray, out hitInfo, 2000, 1 << 8);
        }
        if (raycastHit)
        {
            Debug.Log("Hit Something");
            endPoint = hitInfo.point;
            hitInfo.collider.gameObject.SendMessage("OnHit", damage);
        }
        else
        {
            Debug.Log("Hit Nothing =(");
            if(side == StageSide.blue)
            {
                endPoint =new Vector3(80, this.transform.position.y, this.transform.position.z);
            }else
            {
                endPoint = new Vector3(-80, this.transform.position.y, this.transform.position.z);
            }
        }

    }

    /// <summary>
    /// Destroy the object
    /// </summary>
    void KillSelf()
    {
        Destroy(gameObject);
    }
}
