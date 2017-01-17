using UnityEngine;
using System.Collections;

public class MeteorController : Projectile {
    
	float speed = 40;
	bool iArrived = false;

	static float MAX_COLLIDER_RADIUS = 3.5f;
	static float COLLIDER_GROW_SPEED = 3.5f;
	public GameObject explosion;
	AudioSource explosionSound;
	float colliderRadius;
	bool explosionActivated = false;
	private int numberOfMeteor=3;

	// Use this for initialization
	Vector3 target;
	Vector3 lastTargetPosition;
	void Start () {
		colliderRadius = transform.localScale.x;
        damage = 50;
		explosionSound = GameObject.Find("ChipsSounds/meteor").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        //If the projectile was launched
        if (isActive)
        {
			if (explosionActivated)
			{
				colliderRadius = Mathf.Clamp(colliderRadius + (COLLIDER_GROW_SPEED * Time.deltaTime), 0.0f, MAX_COLLIDER_RADIUS);
				//transform.localScale = Vector3.one * colliderRadius;
				if (colliderRadius == MAX_COLLIDER_RADIUS && explosion.GetComponent<ParticleSystem>().particleCount == 0)
				{
					KillSelf();
				}
			}

            float step = speed * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards (this.transform.position,this.target,step);
        }
	}

    /// <summary>
    /// Tells the projectile to start moving at the default speed
    /// </summary>
    /// <param name="side">Indicates if the projectile is shot from the Blue o Red side of the stage</param>
	public void Launch(StageSide side,Vector3 target)
    {
        SetLayerOfEffect(side);
		this.target = target;
        //First we check in wich side of the stage is the projectile
        if(side == StageSide.red)
        {
            //We chance the speed so it moves form right to left
            speed *= -1;
        }
        //then we raise the Active flag so it can move
        isActive = true;
    }

    void OnBecameInvisible()
    {
        //When the projectile left the visible space we destroy it
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Stage")
		{
			explosionActivated = true;
			explosion.SetActive(true);
			explosion.transform.position = this.transform.position;
			GetComponent<MeshRenderer>().enabled = false;
			explosionSound.PlayDelayed(0);

		}
        if(other.gameObject.layer == layerOfEffect)
        {
            explosionActivated = true;
            explosion.SetActive(true);
            explosion.transform.position = this.transform.position;
            GetComponent<MeshRenderer>().enabled = false;
            explosionSound.PlayDelayed(0);
            other.SendMessage("OnHit", damage);
        }
        //Destroy(gameObject);
    }
	void KillSelf()
	{
		Destroy(explosion.gameObject);
		Destroy(gameObject);
	}
}
