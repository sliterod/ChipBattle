using UnityEngine;
using System.Collections;

public class StunGrenadeProjectil : Projectile {

    static float MAX_COLLIDER_RADIUS = 3.5f;
    static float COLLIDER_GROW_SPEED = 3.5f;

    private float yForce = 25f;
    private float xForce = 19f;
    private float speedModifire = -3f;
    public GameObject explosion;

    float colliderRadius;

    
    bool explosionActivated = false;


    // Use this for initialization
    void Start () {
        colliderRadius = transform.localScale.x;
        damage = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (explosionActivated)
        {
            colliderRadius = Mathf.Clamp(colliderRadius + (COLLIDER_GROW_SPEED * Time.deltaTime), 0.0f, MAX_COLLIDER_RADIUS);
            transform.localScale = Vector3.one * colliderRadius;
            if (colliderRadius == MAX_COLLIDER_RADIUS && explosion.GetComponent<ParticleSystem>().particleCount == 0)
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
        SetLayerOfEffect(side);
        if(side == StageSide.red)
        {
            xForce *= -1;
        }
        Vector3 force = new Vector3(xForce, yForce, 0);
        GetComponent<Rigidbody>().AddForce(force,ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!explosionActivated) {
            if (other.tag == "Stage")
            {
                explosionActivated = true;
                explosion.SetActive(true);
                explosion.transform.position = this.transform.position;
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<MeshRenderer>().enabled = false;
            }
            if (other.tag == "Player")
            {
                explosionActivated = true;
                explosion.SetActive(true);
                explosion.transform.position = this.transform.position;
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else
        {
            if (other.tag == "Player" && other.gameObject.layer == layerOfEffect)
            {
                other.SendMessage("setSpeedModifier", speedModifire);

                StartCoroutine(clearSpeed(4.0F, other, "setSpeedModifier", speedModifire));

                //System.Threading.Timer timer = null;
                //timer = new System.Threading.Timer((obj) =>
                //{
                //    other.SendMessage("setSpeedModifier", 0);
                //    timer.Dispose();
                //},
                //            null, 4000, System.Threading.Timeout.Infinite);
            }
        }
        
    }
    IEnumerator clearSpeed(float waitTime, Collider other,string functionName,float parameter)
    {
        yield return new WaitForSeconds(waitTime);
        other.SendMessage(functionName, 0);
    }
    /// <summary>
    /// Destroy the object
    /// </summary>
    void KillSelf()
    {
        Destroy(explosion.gameObject);
        Destroy(transform.root.gameObject);
    }
}
