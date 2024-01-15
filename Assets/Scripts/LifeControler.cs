using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeControler : MonoBehaviour
{
    public void ModifyLife(float modificator, ref float life, float maxLife)
    {
        life += modificator;
        LifeInLimits(ref life,maxLife);
        KillACharacter(life);
    }
    private void LifeInLimits(ref float life, float maxLife)
    {
        if (life < 0)
            life = 0;
        if (life > maxLife)
            life = maxLife;
    }
    private void KillACharacter(float life)
    {
        if (life <= 0)
            gameObject.GetComponent<Character>().GetComponent<Animator>().SetBool("Dead",true);
    }
}
