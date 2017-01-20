using UnityEngine;
using System.Collections;

public class Meteor : Chip {

    Transform projectilePoint;
    AudioSource shotSound;
	int numberOfProjectils=4;
	private GameObject target;
    /// <summary>
    /// Class constructor
    /// </summary>
    public Meteor()
    {
        _chipName = "#Meteor";
        _chipPrefabName = "Meteor";
        _animation = (int)ChipAnimations.SingleShot;
    }

    // Use this for initialization
    void Start () {
        shotSound=GameObject.Find("ChipsSounds/shot").GetComponent<AudioSource>();

		if (transform.root.name == "Player1") {
			target=GameObject.Find("Player2");
		} else {
			target=GameObject.Find("Player1");
		}

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Override of the Chip's Activate function
    /// </summary>
    public override void Activate()
    {
        projectilePoint = transform.root.Find(RIGHT_HAND_PATH);
        if (!isActive)
        {
            base.Activate();
            shotSound.PlayDelayed(0);
        }        
    }

    void OnHitFrame()
    {
        if (isActive)
        {

            StartCoroutine(shotMeteors(.2f));
                
        } 
    }
    IEnumerator shotMeteors(float waitTime)
    {
        
        GameObject projectile = Instantiate(Resources.Load("Projectiles/Meteor", typeof(GameObject))) as GameObject;
        
        //We take the projectile form the resources
        Vector3 meteorPosition = new Vector3(projectilePoint.position.x, 20, projectilePoint.position.z);
        projectile.transform.position = meteorPosition; //Put it into position
                                                                                                                               //blue side
        int maxDistanceX = 20;
        Vector3 meteorDestinetion;
        if (transform.root.gameObject.layer == 8)
        {
            meteorDestinetion = new Vector3(Random.Range(0, maxDistanceX), 1, Random.Range(-7, 7));
        }
        else {
            maxDistanceX *= -1;
            meteorDestinetion = new Vector3(Random.Range(maxDistanceX, 0), 1, Random.Range(-7, 7));
            
        }

        GameObject projectileTarget = Instantiate(Resources.Load("Micelaneos/Target/target", typeof(GameObject)),meteorDestinetion,Quaternion.identity) as GameObject;
        
        projectile.transform.GetChild(0).GetComponent<MeteorController>().targetMark=projectileTarget;

        if (transform.root.gameObject.layer == 8)
        {
            projectile.transform.GetChild(0).GetComponent<MeteorController>().Launch(StageSide.blue, meteorDestinetion); //And we shoot it
        }
        else
        {
            projectile.transform.GetChild(0).GetComponent<MeteorController>().Launch(StageSide.red, meteorDestinetion); //And we shoot it
        }
        numberOfProjectils--;
        if (numberOfProjectils>0) {
            StartCoroutine(shotMeteors(.2f));
        }
        yield return new WaitForSeconds(waitTime);
    }
}