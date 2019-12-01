using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Items
{
    public abstract class Item : ScriptableObject
    {
        [Header("Item info")]
        [SerializeField] int weight = 5;

        [TextArea(4,10)]
        [SerializeField] string description = "";
        [SerializeField] GameObject itemPrefab = null;
        [SerializeField] Sprite itemSprite = null;

        public int Weight => weight;
        public string Description => description;
        public GameObject ItemPrefab => itemPrefab;
        public Sprite ItemSprite => itemSprite;
    }
}