using UnityEngine;
using System.Collections;

public class MegaCannonBallThree : Projectile {
    
    float speed = 18;
    float angle = 1;
    float radius = 1;
    float right = 1;
    // Use this for initialization
    void Start () {
        damage = 80;
	}
	
	// Update is called once per frame
	void Update () {
        //If the projectile was launched
        if (isActive)
        {
            float step = speed * Time.deltaTime;
            this.transform.Translate(Vector3.right * step);
            


        }
	}

    /// <summary>
    /// Tells the projectile to start moving at the default speed
    /// </summary>
    /// <param name="side">Indicates if the projectile is shot from the Blue o Red side of the stage</param>
    public void Launch(StageSide side)
    {
        SetLayerOfEffect(side);

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
        if(other.gameObject.layer == layerOfEffect)
        {
            other.SendMessage("OnHit", damage);
        }
        Destroy(gameObject);
    }
}
