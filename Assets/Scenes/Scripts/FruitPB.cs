using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPB : MonoBehaviour
{
    [SerializeField] private Fruit fruit;
    [SerializeField] private Transform[] pieces;
    public Fruit Fruit { get => fruit; set => fruit = value; }
    public Transform[] Pieces { get => pieces; set => pieces = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Knife knife))
        {
            transform.SetParent(collision.transform);
        }
    }
}
