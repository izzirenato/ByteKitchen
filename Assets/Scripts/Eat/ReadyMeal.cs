
using System.Collections.Generic;
using UnityEngine;

public class ReadyMeal : MonoBehaviour
{
    private SpriteRenderer _sr;

    void Awake()
    {
        List<Sprite> _sprites = new List<Sprite>()
        {
            Resources.Load<Sprite>("Sprites/ReadyMeals/tortacioccolato"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/couscous_zucchine"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/brownie"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/pizza"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/torta_di_mele"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/sformato"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/cheesecake"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/tagliata"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/pancake"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/pasta_pesto"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/rotoloarancia"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/pasta_funghi_salsiccia"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/biscotti_mandorle"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/frittata_zucchine"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/muffin_cioccolato"),
            Resources.Load<Sprite>("Sprites/ReadyMeals/patate_forno")
        };

        _sr = GetComponent<SpriteRenderer>();
        if (_sr == null)
        {
            Debug.LogError("SpriteRenderer component not found on ReadyMeal object.");
            _sr.sprite = _sprites[1];
            return;
        }
        else
        {
            _sr.sprite = _sprites[(int)Consts.GameData.recipe.nameRecipe];
            Debug.Log($"ReadyMeal sprite set to: {_sprites[(int)Consts.GameData.recipe.nameRecipe].name}");
            Debug.Log((int)Consts.GameData.recipe.nameRecipe + " " + Consts.GameData.recipe.nameRecipe);
        }
    }
}
