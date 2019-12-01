using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Items
{
    [CreateAssetMenu(menuName = "Item/Equipment")]
    public class Equipment : Item
    {
        [Header("Equip stats")]
        [SerializeField] EquipmentType equipType = EquipmentType.BodyArmor;
        [SerializeField] int attack = 10;
        [SerializeField] int defense = 10;

        public EquipmentType EquipType => equipType;
        public int Attack => attack;
        public int Defense => defense;
    }
}