using Consts;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public List<Ingredients> ingredients = new List<Ingredients>();
    public List<Ingredient> selectedIngredients = new List<Ingredient>();
    public List<Ingredient> cuttableIngredients = new List<Ingredient>();
    public List<Ingredients> selectedIngredients_enum = new List<Ingredients>();
    public Recipes nameRecipe;

    public Recipe(Characters character, bool isHappy, bool isSweet) 
    {
        WhatRecipeIs(character, isHappy, isSweet);
        AddIngredients(nameRecipe);
    }

    public void WhatRecipeIs(Characters character, bool isHappy, bool isSweet)
    {
        if (isHappy)
        {
            if (isSweet)
            {
                switch (character)
                {
                    case Characters.MANGO:
                        nameRecipe = Recipes.TORTA_MELE;
                        break;
                    case Characters.MARGHERITA:
                        nameRecipe = Recipes.CHEESECAKE_FRUTTI_BOSCO;
                        break;
                    case Characters.BOB:
                        nameRecipe = Recipes.BISCOTTI_MANDORLE;
                        break;
                    case Characters.ALEX:
                        nameRecipe = Recipes.ROTOLO_ARANCIA;
                        break;
                }
            }
            else
            {
                switch (character)
                {
                    case Characters.MANGO:
                        nameRecipe = Recipes.PASTA_PESTO;
                        break;
                    case Characters.MARGHERITA:
                        nameRecipe = Recipes.FRITTATA_ZUCCHINE;
                        break;
                    case Characters.BOB:
                        nameRecipe = Recipes.COUS_COUS_ZUCCHINE;
                        break;
                    case Characters.ALEX:
                        nameRecipe = Recipes.TAGLIATA;
                        break;
                }
            }
        }
        else
        {
            if (isSweet)
            {
                switch (character)
                {
                    case Characters.MANGO:
                        nameRecipe = Recipes.TORTA_CIOCCOLATO;
                        break;
                    case Characters.MARGHERITA:
                        nameRecipe = Recipes.MUFFIN_CIOCCOLATO;
                        break;
                    case Characters.BOB:
                        nameRecipe = Recipes.PANCAKE_BANANA;
                        break;
                    case Characters.ALEX:
                        nameRecipe = Recipes.BROWNIE;
                        break;
                }
            }
            else
            {
                switch (character)
                {
                    case Characters.MANGO:
                        nameRecipe = Recipes.PATATE_FORNO;
                        break;
                    case Characters.MARGHERITA:
                        nameRecipe = Recipes.PIZZA_MARGHERITA;
                        break;
                    case Characters.BOB:
                        nameRecipe = Recipes.SFORMATO_ZUCCA_PATATE;
                        break;
                    case Characters.ALEX:
                        nameRecipe = Recipes.PASTA_FUNGHI_SALSICCIA;
                        break;
                }
            }
        }
    }


    public void AddIngredients(Recipes recipe)
    {
        switch(recipe)
        {
            case Recipes.TORTA_CIOCCOLATO:
                ingredients.Add(Ingredients.FARINA);
                ingredients.Add(Ingredients.CIOCCOLATO);
                ingredients.Add(Ingredients.MANDORLA);
                ingredients.Add(Ingredients.ACQUA);
                ingredients.Add(Ingredients.OLIO);
                ingredients.Add(Ingredients.ZUCCHERO);
                ingredients.Add(Ingredients.LIEVITO);
                break;
            case Recipes.COUS_COUS_ZUCCHINE:
                ingredients.Add(Ingredients.COUS_COUS);
                ingredients.Add(Ingredients.ZUCCHINA);
                ingredients.Add(Ingredients.CIPOLLA);
                ingredients.Add(Ingredients.BURRO);
                ingredients.Add(Ingredients.SALE);
                ingredients.Add(Ingredients.BASILICO);
                ingredients.Add(Ingredients.OLIO);
                ingredients.Add(Ingredients.ACQUA);
                break;
            case Recipes.BROWNIE:
                ingredients.Add(Ingredients.CIOCCOLATO);
                ingredients.Add(Ingredients.UOVO);
                ingredients.Add(Ingredients.NOCCIOLA);
                ingredients.Add(Ingredients.BURRO);
                ingredients.Add(Ingredients.SALE);
                ingredients.Add(Ingredients.ZUCCHERO);
                ingredients.Add(Ingredients.FARINA);
                break;
            case Recipes.PIZZA_MARGHERITA:
                ingredients.Add(Ingredients.FARINA);
                ingredients.Add(Ingredients.POMODORO);
                ingredients.Add(Ingredients.BASILICO);
                ingredients.Add(Ingredients.MOZZARELLA);
                ingredients.Add(Ingredients.SALE);
                ingredients.Add(Ingredients.LIEVITO);
                ingredients.Add(Ingredients.OLIO);
                ingredients.Add(Ingredients.ACQUA);
                break;
            case Recipes.PANCAKE_BANANA:
                ingredients.Add(Ingredients.BANANA);
                ingredients.Add(Ingredients.CANNELLA);
                ingredients.Add(Ingredients.LATTE);
                ingredients.Add(Ingredients.FARINA);
                ingredients.Add(Ingredients.SALE);
                ingredients.Add(Ingredients.LIEVITO);
                ingredients.Add(Ingredients.OLIO);
                break;
            case Recipes.PASTA_PESTO:
                ingredients.Add(Ingredients.PASTA);
                ingredients.Add(Ingredients.PINOLO);
                ingredients.Add(Ingredients.AGLIO);
                ingredients.Add(Ingredients.FORMAGGIO);
                ingredients.Add(Ingredients.SALE);
                ingredients.Add(Ingredients.BASILICO);
                ingredients.Add(Ingredients.OLIO);
                break;
            case Recipes.ROTOLO_ARANCIA:
                ingredients.Add(Ingredients.ARANCIA);
                ingredients.Add(Ingredients.ZUCCHERO);
                ingredients.Add(Ingredients.FARINA);
                ingredients.Add(Ingredients.UOVO);
                ingredients.Add(Ingredients.LIEVITO);
                ingredients.Add(Ingredients.SALE);
                ingredients.Add(Ingredients.BURRO);
                ingredients.Add(Ingredients.ACQUA);
                break;
            case Recipes.PASTA_FUNGHI_SALSICCIA:
                ingredients.Add(Ingredients.PASTA);
                ingredients.Add(Ingredients.SALSICCIA);
                ingredients.Add(Ingredients.FUNGO);
                ingredients.Add(Ingredients.PEPE);
                ingredients.Add(Ingredients.SALE);
                ingredients.Add(Ingredients.VINO_BIANCO);
                ingredients.Add(Ingredients.OLIO);
                ingredients.Add(Ingredients.PANNA);
                ingredients.Add(Ingredients.AGLIO);
                ingredients.Add(Ingredients.ERBE);
                break;
            case Recipes.TORTA_MELE:
                ingredients.Add(Ingredients.MELA);
                ingredients.Add(Ingredients.LIEVITO);
                ingredients.Add(Ingredients.ZUCCHERO);
                ingredients.Add(Ingredients.FARINA);
                ingredients.Add(Ingredients.CANNELLA);
                ingredients.Add(Ingredients.OLIO);
                ingredients.Add(Ingredients.ACQUA);
                break;
            case Recipes.SFORMATO_ZUCCA_PATATE:
                ingredients.Add(Ingredients.ZUCCA);
                ingredients.Add(Ingredients.PATATA);
                ingredients.Add(Ingredients.ERBE);
                ingredients.Add(Ingredients.AGLIO);
                ingredients.Add(Ingredients.SALE);
                ingredients.Add(Ingredients.LATTE);
                ingredients.Add(Ingredients.FARINA);
                ingredients.Add(Ingredients.OLIO);
                break;
            case Recipes.CHEESECAKE_FRUTTI_BOSCO:
                ingredients.Add(Ingredients.FRUTTI_DI_BOSCO);
                ingredients.Add(Ingredients.FORMAGGIO_SPALMABILE);
                ingredients.Add(Ingredients.BISCOTTO);
                ingredients.Add(Ingredients.BURRO);
                ingredients.Add(Ingredients.PANNA);
                ingredients.Add(Ingredients.SALE);
                break;
            case Recipes.TAGLIATA:
                ingredients.Add(Ingredients.CARNE);
                ingredients.Add(Ingredients.POMODORO);
                ingredients.Add(Ingredients.RUCOLA);
                ingredients.Add(Ingredients.PEPE);
                ingredients.Add(Ingredients.SALE);
                ingredients.Add(Ingredients.FORMAGGIO);
                ingredients.Add(Ingredients.OLIO);
                break;
            case Recipes.BISCOTTI_MANDORLE:
                ingredients.Add(Ingredients.MANDORLA);
                ingredients.Add(Ingredients.ZUCCHERO);
                ingredients.Add(Ingredients.FARINA);
                ingredients.Add(Ingredients.UOVO);
                ingredients.Add(Ingredients.BURRO);
                break;
            case Recipes.FRITTATA_ZUCCHINE:
                ingredients.Add(Ingredients.UOVO);
                ingredients.Add(Ingredients.ZUCCHINA);
                ingredients.Add(Ingredients.CIPOLLA);
                ingredients.Add(Ingredients.ERBE);
                ingredients.Add(Ingredients.SALE);
                ingredients.Add(Ingredients.OLIO);
                break;
            case Recipes.MUFFIN_CIOCCOLATO:
                ingredients.Add(Ingredients.CIOCCOLATO);
                ingredients.Add(Ingredients.ZUCCHERO);
                ingredients.Add(Ingredients.FARINA);
                ingredients.Add(Ingredients.UOVO);
                ingredients.Add(Ingredients.BURRO);
                ingredients.Add(Ingredients.LIEVITO);
                break;
            case Recipes.PATATE_FORNO:
                ingredients.Add(Ingredients.PATATA);
                ingredients.Add(Ingredients.ERBE);
                ingredients.Add(Ingredients.CIPOLLA);
                ingredients.Add(Ingredients.SALE);
                ingredients.Add(Ingredients.OLIO);
                break;
        }
    }

    public void PrintRecipe()
    {
        Debug.Log(nameRecipe);
        
        foreach( Ingredients ingredient in ingredients)
        {
            Debug.Log(ingredient);
        }
    }

    public bool AllIngredientsTaken()
    {
        
        foreach(Ingredients i in ingredients)
        {
            if(!selectedIngredients_enum.Contains(i))
                return false;
        }

        return true;
    }

    public bool AllIngredientsCut() 
    {
        foreach(Ingredient i in cuttableIngredients)
        {
            if(!i.hasBeenCut)
            {
                Debug.Log(i.GetName() + " non è stato tagliato");
                return false;
            }
        }

        return true;
    }
}
