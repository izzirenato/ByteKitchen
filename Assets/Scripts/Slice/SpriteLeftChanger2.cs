using UnityEngine;

public class SpriteLeftChanger2 : MonoBehaviour
{
    private string referenceName;

    public void SetReferenceName(string name)
    {
        referenceName = name;
        LoadSprite();
    }

    private void LoadSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        if (referenceName == null)
        {
            Debug.Log("reference name null");
            sr.sprite = null;
            return;
        }

        Sprite[] slices = Resources.LoadAll<Sprite>("Sprites/IngredientsCut/" + referenceName);
        if (slices == null || slices.Length == 0) return;

        Sprite slice = System.Array.Find(slices, s => s.name == referenceName + "_0");
        if (slice != null)
            sr.sprite = slice;
        else
            Debug.LogWarning($"Slice sinistro \"{referenceName}_0\" non trovato in Resources/Sprites/IngredientsCut/");
    }
}

