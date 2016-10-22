using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    /// <summary>
    /// Tells if the projectile is flying
    /// </summary>
    protected bool isActive = false;

    /// <summary>
    /// Damage that will be applied to the target on impact
    /// </summary>
    protected int damage = 40;

    /// <summary>
    /// The number of the layer of the enemy side
    /// </summary>
    protected int layerOfEffect;

    /// <summary>
    /// Sets the damage of the proyectile
    /// </summary>
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

   /// <summary>
   /// Sets the layer of effect based on the side of the field where the projectile is being launched
   /// </summary>
   /// <param name="side"></param>
    public void SetLayerOfEffect(StageSide side)
    {
        if(side == StageSide.blue)
        {
            layerOfEffect = 9; //The red side
        }
        else
        {
            layerOfEffect = 8; //The blue side
        }
        
    }
}
