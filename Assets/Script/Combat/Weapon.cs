using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Make New Weapon", order = 0)]

    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController animationOverride = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 1f;
        [SerializeField] bool isRightHand = true;



        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (weaponPrefab != null)
            {
                Transform handTransform;
                if (isRightHand)
                {
                    handTransform = rightHand;
                }
                else
                {
                    handTransform = leftHand;
                }
                Instantiate(weaponPrefab, handTransform);
            }
            if (animationOverride != null)
            {
                animator.runtimeAnimatorController = animationOverride;
            }
        }

        public float GetDamage()
        {
            return weaponDamage;
        }
        public float GetRange()
        {
            return weaponDamage;
        }

    }
}
