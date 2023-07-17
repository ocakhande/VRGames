using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(menuName ="Gun")]
public class Gun : ScriptableObject
{
    public float Damage;
    public float fireSpeed;
    public int Cost;
    public GameObject gun;
    public int Id;
}
