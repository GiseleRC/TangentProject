using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arrow Stats", menuName = "ScriptableObjects/GameCore/ArrowStats")]
public class ArrowStats : ScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    public int Damage { get => _damage; }

    public float Speed { get => _speed; }
}
