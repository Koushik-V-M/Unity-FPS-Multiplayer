using UnityEngine;

[System.Serializable]
public class playerWeapon
{
    public string name = "gun1";
    public int damage = 10;
    public float range = 100f;
    public int fireRateWeapon = 15;
    public ParticleSystem gunFlare;
    public GameObject hitEffect;
}
