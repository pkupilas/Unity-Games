using UnityEngine;
using _Characters.SpecialAbilities;

[CreateAssetMenu(menuName = "RPG/SpecialAbility/SelfHeal")]
public class SelfHealConfig : AbilityConfig
{
    [Header("Self Heal Specific")]
    [SerializeField] private float _healAmount;

    public float HealAmount => _healAmount;

    public override void AttachComponentTo(GameObject gameObject)
    {
        var behaviourComponent = gameObject.AddComponent<SelfHealBehaviour>();
        behaviourComponent.SetConfig(this);
        behaviour = behaviourComponent;
    }
}
