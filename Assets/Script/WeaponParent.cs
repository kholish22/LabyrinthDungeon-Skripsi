using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public enum AttackDirection
    {
        left, right
    }

    Vector2 rightAttackOffset;
    Collider2D swordCollider;
    Coroutine attackCoroutine;

    public AttackDirection attackDirection;
    public Animator animator;
    public float delay = 0.3f;
    private bool attackBlocked;

    // Start is called before the first frame update
    private void Start()
    {
        swordCollider = GetComponentInChildren<Collider2D>();
        rightAttackOffset = transform.position;
    }

    public void Attack()
    {
        switch (attackDirection)
        {
            case AttackDirection.left:
                AttackLeft();
                break;
            case AttackDirection.right:
                AttackRight();
                break;
        }

        if (attackBlocked)
            return;

        if (gameObject.activeSelf)
        {
            animator.SetTrigger("swordAttack");
            attackBlocked = true;
            attackCoroutine = StartCoroutine(DelayAttack());
        }
        else
        {
            if (GameObject.Find("WeaponParent Gold").activeSelf)
            {
                WeaponParent weaponGold = GameObject.Find("WeaponParent Gold").GetComponent<WeaponParent>();
                weaponGold.Attack();
                if (attackDirection == AttackDirection.left)
                {
                    weaponGold.AttackLeft();
                }
                else if (attackDirection == AttackDirection.right)
                {
                    weaponGold.AttackRight();
                }
            }
            else
            {
                Debug.Log("WeaponParent is inactive");
            }
        }
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    private void AttackRight()
    {
        swordCollider.enabled = true;
        FlipRight();
    }

    private void AttackLeft()
    {
        swordCollider.enabled = true;
        FlipLeft();
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    void FlipLeft()
    {
        Vector2 currentRotation = transform.eulerAngles;
        currentRotation = new Vector3(0, 180, 0);
        transform.localEulerAngles = currentRotation;
    }

    void FlipRight()
    {
        Vector2 currentRotation = transform.eulerAngles;
        currentRotation = new Vector3(0, 0, 0);
        transform.localEulerAngles = currentRotation;
    }

    // Coroutine handler to keep the current attack coroutine running after weapon switch
    private IEnumerator HandleCoroutineSwitch(Coroutine currentCoroutine, WeaponParent newWeapon)
    {
        // Wait for the current attack to finish before switching weapons
        yield return currentCoroutine;

        // Start a new attack coroutine with the new weapon
        newWeapon.Attack();
    }

    // Method to switch weapons
    public void SwitchWeapon(WeaponParent newWeapon)
    {
        // Stop the current attack and the delay coroutine
        StopAttack();

        // Start a coroutine to handle the weapon switch
        StartCoroutine(HandleCoroutineSwitch(attackCoroutine, newWeapon));
    }
}
