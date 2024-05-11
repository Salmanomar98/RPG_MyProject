using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;


        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;


        Health targetObject;
        float timeSinceLastAttack;

        private void Start()
        {
            SpawnWeapon(defaultWeapon);
        }

        private void Update()
        {
            timeBetweenAttacks += Time.deltaTime;
            if (targetObject == null)
            {
                return;
            }


            if (targetObject.IsDead() == true)
            {
                GetComponent<Animator>().ResetTrigger("attack");
                Cancel();

            }


            if (GetIsInRange() == false)
            {
                GetComponent<Mover>().MoveTo(targetObject.transform.position);
            }
            else
            {
                AttackMethod();
                GetComponent<Mover>().Cancel();
            }
        }

        public void SpawnWeapon(Weapon weapon)
        {

            defaultWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            defaultWeapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        private void AttackMethod()
        {
            transform.LookAt(targetObject.transform);
            if (timeSinceLastAttack < timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
                //
                targetObject.TakeDamage(defaultWeapon.GetDamage());
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }
            Health healthToTest = GetComponent<Health>();
            return healthToTest != null && !healthToTest.IsDead();
        }
        public void Attack(GameObject target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            targetObject = target.GetComponent<Health>();
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }


        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, targetObject.transform.position) < defaultWeapon.GetRange();
        }


        public void Cancel()
        {
            StopAttack();
            targetObject = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }

}
