using UnityEngine;

[CreateAssetMenu(fileName = "SFXGroup", menuName = "Audio/SFX Group")]
public class SFXGroup : ScriptableObject
{
    public SFXType sfxType; // Örneğin: "Metal", "Wood"
    public AudioClip[] clips; // Bu gruba ait ses efektleri
}
