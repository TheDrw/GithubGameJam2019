using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Items
{
    [CreateAssetMenu(menuName = "Item/Consumable")]
    public class Consumable : Item, IConsumable
    {
        [SerializeField] ConsumableType consumableType;
    }
}