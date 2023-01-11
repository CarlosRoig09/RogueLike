using UnityEngine;
public enum Life
{
    Alive,
    Death
}
public class CharData : ScriptableObject
{
    public string Name;
    public float speed;
    public float life;
    public float maxlife;
    public Life State;
    public float invulerability;
    public Invulnerability Damagable;
}
