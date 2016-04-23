using UnityEngine;
using System.Collections;

public class CharacterFxController : MonoBehaviour {

    public ParticleSystem DamageFX;
    public GameObject DeathFX;

    //Flags
    bool damageNeedReset;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnHit(int damage)
    {
        DamageFX.Clear();
        DamageFX.Simulate(0.0f, true, true);
        if(damage <= 50)
        {
            DamageFX.Emit(10); //Low damage attack
        }
        else if(damage <= 100)
        {
            DamageFX.Emit(15); //Medium damage attack
        }
        else
        {
            DamageFX.Emit(20); //Heavy damage Attack
        }

    }

   void OnDeath()
    {
        transform.FindChild("Mesh").gameObject.SetActive(false);
        DeathFX.SetActive(true);
    }
}
