using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "new player config", menuName = "Configuration/Player config")]
public class PlayerConfig : ScriptableObject{
    [Tooltip("The force can be generated per energy")]
    public float energyForce;
    [Tooltip("The minimum energy needed per jump")]
    public float minEnergyNeeded;
    [Tooltip("Energy needed per unit distance player drags on screen")]
    public float energyNeedMul;
    [Tooltip("The amount of energy burned off when not jumping")]
    public float energyBurnOff;
    [Tooltip("The amount of energy burned off when slo-mo")]
    public float energyBurnOffSlowMo;
    [Tooltip("The initial amount of energy can be gained per fuel")]
    public float initialFuelPower;
    [Tooltip("The initial amount of maxmimum energy")]
    public float initialMaxEnergy;
    [Tooltip("The maxmimum explode force can be generated when collide with fuel or bomb")]
    public float maxExplodeForce;
    [Tooltip("The max distance between the player and lava")]
    public float maxLavaDistance;
    [Tooltip("The speed lava rise")]
    public float lavaSpeed;
    [Tooltip("Max energy increase per level of max energy")]
    public float maxEnergyIncreasePerLv;
    [Tooltip("Fuel power increase per level of fuel power")]
    public float fuelPowerIncreasePerLv;
    [Tooltip("Energy durability increase per level of energy durability")]
    public float energyDuribilityIncreasePerLv;
    [Tooltip("How many times slower when slo mo")]
    public float timeSlowAmount;
    [Tooltip("time to fade in slo mo effect")]
    public float sloMoEffectFadeInTime;

}
