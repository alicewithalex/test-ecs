using UnityEngine;

[CreateAssetMenu(menuName = "Create/EcsCustom/Template Settings", fileName = "TemplateSettings")]
sealed class TemplateSettings : ScriptableObject
{
    public string NamespacePrefix = "Modules.MR";
    [Min(0)] public int SkipDeepness = 3;
}
