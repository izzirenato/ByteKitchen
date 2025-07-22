using UnityEngine;

public class SpriteGroupChanger2 : MonoBehaviour
{
    private string referenceName;

    [Header("Riferimenti ai tre SpriteChanger figli")]
    [SerializeField] private SpriteLeftChanger2 leftChanger;
    [SerializeField] private SpriteMidChanger2 midChanger;
    [SerializeField] private SpriteRightChanger2 rightChanger;

    public void SetSpriteIngredient()
    { 
        referenceName = IngredienteInfo.referenceName;

        if (string.IsNullOrEmpty(referenceName))
        {
            Debug.LogWarning("referenceName non assegnato nell'IngredienteInfo.");
            leftChanger.SetReferenceName(referenceName);
            midChanger.SetReferenceName(referenceName);
            rightChanger.SetReferenceName(referenceName);
            return;
        }

        if (leftChanger != null)
            leftChanger.SetReferenceName(referenceName);

        if (midChanger != null)
            midChanger.SetReferenceName(referenceName);

        if (rightChanger != null)
            rightChanger.SetReferenceName(referenceName);
    }
}
