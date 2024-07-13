using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    public WeaponAnimations[] weaponAnimations;

    private Dictionary<Equipment, AnimationClip[]> _weaponAnimationsDict;

    protected override void Start()
    {
        base.Start();
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged; 

        _weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]>();

        foreach (WeaponAnimations a in weaponAnimations)
        {
            _weaponAnimationsDict.Add(a.weapon, a.clips);
        }
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            _animator.SetLayerWeight(1, 1);
            if (_weaponAnimationsDict.ContainsKey(newItem))
            {
                _currentAttackAnimSet = _weaponAnimationsDict[newItem];
            }
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon)
        {
            _animator.SetLayerWeight(1, 0);
            _currentAttackAnimSet = defaultAnimation;
        }

        if (newItem != null && newItem.equipSlot == EquipmentSlot.Shield)
        {
            _animator.SetLayerWeight(2, 1);
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Shield)
        {
            _animator.SetLayerWeight(2, 0);
        }
    }

    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}
