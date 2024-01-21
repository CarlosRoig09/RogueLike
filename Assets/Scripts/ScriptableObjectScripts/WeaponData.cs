using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "WeaponSO")]
public class WeaponData : ItemData
{
    public ShootSO shootData;
    public MeleeSO meleeData;
    public GameObject Weapon;
    public LayerMask WeaponHolder;
    public IWeaponControler ActualScript;
    public WeaponState WA;
    private Vector3 _initialLocalPosition;
    private Quaternion _initialLocalRotation;
    private Vector3 _initialPosition;

    public void CallInStart()
    {
        WA = WeaponState.Normal;
        shootData.rangeAttack = true;
        meleeData.meleeAttack = true;
    }
    public void PrepareToThrowWeapon(GameObject parent, GameObject weapon)
    {
        if ( shootData.rangeAttack&& WA == WeaponState.Normal)
        {
            _initialLocalPosition = weapon.transform.localPosition;
            _initialLocalRotation = weapon.transform.localRotation;
            _initialPosition = weapon.transform.position;
            weapon.transform.SetPositionAndRotation(parent.transform.position, parent.transform.rotation);
            ParentAndChildrenMethods.UnParentAnSpecificChildren(parent, weapon);
            WA = WeaponState.DistanceAttack;
        }
    }
    public void ThrowWeapon(GameObject weapon, float Speed)
    {
        if(shootData.rangeAttack)
        weapon.GetComponent<Rigidbody2D>().velocity = Speed * Time.fixedDeltaTime * weapon.transform.right;
    }

    public void MaxDistance(GameObject weapon, WeaponState newState)
    {
        if (WA == WeaponState.DistanceAttack)
            if ((weapon.transform.position - _initialPosition).magnitude >= shootData.maxDistance)
            {
                weapon.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
                WA = newState;
            }
    }

    public IEnumerator WeaponSpin(float time, GameObject weapon, WeaponState WS)
    {
        weapon.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        weapon.transform.Rotate(shootData.ProyectileSpeed * Time.deltaTime * Vector3.forward, Space.Self);
        yield return new WaitForSeconds(time);
        WA = WS;
    }

    public void ComeBack(GameObject parent, GameObject weapon,float speedToComeBack, float Speed)
    {
        if (WA == WeaponState.ComeBack)
        {
            var vectorComeBack = parent.transform.position - weapon.transform.position;
            weapon.transform.rotation = Quaternion.Euler(weapon.transform.rotation.x, weapon.transform.rotation.y, Mathf.Atan2(vectorComeBack.y, vectorComeBack.x) * Mathf.Rad2Deg);
            ThrowWeapon(weapon,Speed);
            if (Physics2D.Raycast(weapon.transform.position, parent.transform.position - weapon.transform.position, 0.65f, WeaponHolder.value))
            {
                ParentAndChildrenMethods.ParentAChildren(parent, weapon);
                weapon.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
                weapon.transform.localPosition = _initialLocalPosition;
                weapon.transform.localRotation = _initialLocalRotation;
                WA = WeaponState.Normal;
                shootData.rangeAttack = false;
            }
        }
    }

    //Slash, Double Slash and stab attacks.
    public void AttackByAnimator(GameObject weapon, WeaponState WS)
    {
            weapon.GetComponent<Animator>().SetBool("Attack", true);
            WA = WS;
    }

    public void RotateWeapon(GameObject grandParent, Quaternion endPoint, ref bool noRepitePosition)
    {
        var rotationScript = grandParent.GetComponent<MoveAroundThePlayerByMousePController>();
        rotationScript.HS = HandState.MoveByRotateAnim;
        rotationScript.RoationSpeed = 850;
        Debug.Log(noRepitePosition);
        if (endPoint.eulerAngles.magnitude <= grandParent.transform.rotation.eulerAngles.magnitude)
        {
            if (noRepitePosition)
            {
                rotationScript.HS = HandState.MoveByMouse;
                WA = WeaponState.EndMeleeAttack;
            }
        }
        else
            noRepitePosition = true;
    }

    public void AttackAnimationIsFinished(GameObject weapon,ref bool typeOfAttack)
    {
        weapon.GetComponent<Animator>().SetBool("Attack", false);
        AttackFinished(ref typeOfAttack);
    }

    public void AttackFinished(ref bool typeOfAttack)
    {
        typeOfAttack = false;
        WA = WeaponState.Normal;
    }

    public IEnumerator ResetMeleeCount()
    {
        yield return new WaitForSeconds(meleeData.CadenceTime);
        meleeData.meleeAttack = true;
        MultipleAttack();
    }

    private IEnumerator MultipleAttack()
    {
        meleeData.CurrentAttack += 1;
        if (meleeData.CurrentAttack < meleeData.WeaponAttacks.Count)
        {
            yield return new WaitForSeconds(meleeData.WeaponAttacks[meleeData.CurrentAttack-1].CountDown + meleeData.CadenceTime);
            meleeData.CurrentAttack -= 1;
        }
        else
        {
            meleeData.CurrentAttack = 0;
        }
    }

    public IEnumerator ResetProyectileCount()
    {
        yield return new WaitForSeconds(shootData.CadenceTime);
        shootData.rangeAttack = true;
    }

}


