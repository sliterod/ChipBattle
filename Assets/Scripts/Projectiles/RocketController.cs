using UnityEngine;
using System.Collections;

public class RocketController : Projectile {
    
	float topSpeed = 15;
	float speed = 0;
	float curve = 4;
	bool iArrived = false;

	static float MAX_COLLIDER_RADIUS = 3.5f;
	static float COLLIDER_GROW_SPEED = 3.5f;
	public GameObject explosion;
	AudioSource explosionSound;
	float colliderRadius;
	bool explosionActivated = false;


	// Use this for initialization
	Transform target;
	Vector3 lastTargetPosition;
	void Start () {
		colliderRadius = transform.localScale.x;
        damage = 40;
		explosionSound = GameObject.Find("ChipsSounds/rocket").GetComponent<AudioSource>();
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


			if (speed < topSpeed) {
				speed += 0.3f;
				if (speed > topSpeed/2) {
					speed += 0.4f;
				}
			}
            float step = speed * Time.deltaTime;

			if(speed < 0)
			{
				//We chance the speed so it moves form right to left
				curve *= -1;
			}

			if( (lastTargetPosition.x - this.transform.position.x) > curve ){
				this.transform.Translate(Vector3.right * step);
			}else{
				if (iArrived) {
					this.transform.Translate (Vector3.right * step/2);
					this.transform.Translate (Vector3.down * step);
				} else {
					this.transform.position = Vector3.MoveTowards (this.transform.position,lastTargetPosition,step);
					if ((lastTargetPosition.x - this.transform.position.x) > (curve/4)) {
						this.transform.Translate (Vector3.right * step/1.5f);
					}	
				}
				if (this.transform.position.Equals (lastTargetPosition)) {
					iArrived=true;
				}
			}

			if((this.target.position.x - this.transform.position.x) > curve/1.5f){
				lastTargetPosition = this.target.position;
			}
        }
	}

    /// <summary>
    /// Tells the projectile to start moving at the default speed
    /// </summary>
    /// <param name="side">Indicates if the projectile is shot from the Blue o Red side of the stage</param>
	public void Launch(StageSide side,Transform target)
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
			//GetComponent<MeshRenderer>().enabled = false;
			explosionSound.PlayDelayed(0);

		}
		if (other.name == this.target.transform.name)
		{
			explosionActivated = true;
			explosion.SetActive(true);
			explosion.transform.position = this.transform.position;
			//GetComponent<MeshRenderer>().enabled = false;
			explosionSound.PlayDelayed(0);
		}
        if(other.gameObject.layer == layerOfEffect)
        {
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
