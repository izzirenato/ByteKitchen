using System.Collections.Generic;
using UnityEngine;

namespace Consts
{
    public class SceneNames

    {
        public const string MainMenu = "MainMenu";
        public const string Intro = "Introduction";
        public const string Character = "CharacterSelection";
        public const string Preferences = "PreferencesSelection";

        public const string Kitchen = "Kitchen";
        public const string Living = "LivingRoom";

        public const string Fridge = "Fridge";
        public const string Cupboard = "Cupboard";

        public const string Cut = "Cut";
        public const string Mix = "Mix";

        public const string miniGame_1 = "MiniGame #1";
        public const string miniGame_2 = "MiniGame #2";
        public const string turnTable = "TurnTable";
        public const string bookshelf = "Bookshelf";
        public const string photo = "Photo";

        public const string eat = "Eat";
        public const string End = "EndScene";
    }

    //non so se può servire
    public class SpritesName
    {
        public const string CharacterFront = "Square";
        public const string CharacterRight = "Triangle";
        public const string CharacterLeft = "Circle";
        public const string CharacterBack = "Capsule";
        

        public const string zucchina = "zucchina";
        public const string acqua = "acqua";
        public const string aglio = "aglio";
        public const string arancia = "arancia";
        public const string banana = "banana";
        public const string basilico = "basilico";
        public const string biscotto = "biscotto";
        public const string burro = "burro";
        public const string cannella = "cannella";
        public const string carne = "carne";
        public const string cioccolato = "cioccolato";
        public const string cipolla = "cipolla";
        public const string couscous = "cousCous";
        public const string erbe = "erbe";
        public const string farina = "farina";
        public const string formaggio = "formaggio";
        public const string formaggioSpalmabile = "formaggioSpalmabile";
        public const string fruttiDiBosco = "fruttiDiBosco";
        public const string fungo = "fungo";
        public const string latte = "latte";
        public const string lievito = "lievito";
        public const string mandorla = "mandorla";
        public const string mela = "mela";
        public const string mozzarella = "mozzarella";
        public const string noce = "noce";
        public const string olio = "oil";
        public const string panna = "panna";
        public const string pasta = "pasta";
        public const string patata = "patata";
        public const string pepe = "pepe";
        public const string pinolo = "pinolo";
        public const string pomodoro = "pomodoro";
        public const string rucola = "rucola";
        public const string sale = "sale";
        public const string salsiccia = "salsiccia";
        public const string uovo = "uovo";
        public const string vino = "vino";
        public const string zucca = "zucca";
        public const string zucchero = "zucchero";

    }

    public enum Ingredients
    {
        //DENTRO IL FRIGO
        ZUCCHINA,
        CIPOLLA,
        BURRO,
        BASILICO,
        UOVO,
        POMODORO,
        MOZZARELLA,
        LATTE,
        FORMAGGIO,
        ARANCIA,
        FUNGO,
        SALSICCIA,
        PANNA,
        MELA,
        ZUCCA,
        FRUTTI_DI_BOSCO,
        FORMAGGIO_SPALMABILE,
        ACQUA,
        CARNE,
        RUCOLA,


        //FUORI DAL FRIGO
        BANANA,
        SALE,
        NOCCIOLA,
        CANNELLA,
        PASTA,
        PINOLO,
        AGLIO,
        PEPE,
        VINO_BIANCO,
        PATATA,
        ERBE,
        BISCOTTO,
        FARINA,
        CIOCCOLATO,
        MANDORLA,
        OLIO,
        ZUCCHERO,
        LIEVITO,
        COUS_COUS
    }

    public enum Recipes
    {
        TORTA_CIOCCOLATO,
        COUS_COUS_ZUCCHINE,
        BROWNIE,
        PIZZA_MARGHERITA,
        TORTA_MELE,
        SFORMATO_ZUCCA_PATATE,
        CHEESECAKE_FRUTTI_BOSCO,
        TAGLIATA,
        PANCAKE_BANANA,
        PASTA_PESTO,
        ROTOLO_ARANCIA,
        PASTA_FUNGHI_SALSICCIA,
        BISCOTTI_MANDORLE,
        FRITTATA_ZUCCHINE,
        MUFFIN_CIOCCOLATO,
        PATATE_FORNO
    }

    public enum Characters
    {
        MANGO,
        MARGHERITA,
        ALEX,
        BOB
    }

    public static class GameData
    {
        //in double?
        public static float SFXvolume = 1.0f;
        public static float musicVolume = 1.0f;

        public static Characters characterSelected;
        public static bool isHappy;
        public static bool isSweet;

        public static Vector2 playerPosition = new(0,0);

        public static Recipe recipe;
        public static bool isMealReady = false;

        public static bool robotIsCooking = false;

        public static int interactionCount = 0; //conta quante volte il giocatore ha interagito con gli oggetti
        public static HashSet<string> interactedObjects = new HashSet<string>(); //per tenere traccia degli oggetti con cui il giocatore ha interagito

        public static AudioClip currentLivingRoomMusic;
        public static bool isVinylMusicPlaying = false;
        public static bool isPhotoClean = false;
        public static bool isLampOn = true; // stato della lampada, inizialmente accesa

        //funzione che controlla che tutte le variabili sono inizializzate (?)

        public static void PrintGameData()
        {
            Debug.Log(characterSelected);
            Debug.Log(isHappy);
            Debug.Log(isSweet);
        }

        public static void Reset()
        {
            characterSelected = Characters.MANGO;
            isHappy = true;
            isSweet = true;
            playerPosition = new Vector2(0, 0);
            recipe = null;
            isMealReady = false;
            robotIsCooking = false;
            interactionCount = 0;
            interactedObjects.Clear();
            currentLivingRoomMusic = null;
            isVinylMusicPlaying = false;
            isPhotoClean = false;
            isLampOn = true; // resetta lo stato della lampada
        }
    }
}

