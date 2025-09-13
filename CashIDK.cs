using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Simulation;
using UnityEngine;

[assembly: MelonInfo(typeof(MoneyInputMod.Main), "Money Input Mod", "1.0.0", "DeinName")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace MoneyInputMod
{
    public class Main : BloonsTD6Mod
    {
        private bool showInput = false;
        private string inputText = "";

        public override void OnUpdate()
        {
            // Debugging: Überprüfen, ob E wirklich gedrückt wird
            if (Input.GetKeyDown(KeyCode.E))
            {
                MelonLogger.Msg("E gedrückt!"); // Zeigt an, wenn E gedrückt wurde
                // Show the input box only if the game is running
                if (InGame.instance != null)
                {
                    MelonLogger.Msg("Spiel läuft. Eingabefeld wird angezeigt.");
                    showInput = true;
                    inputText = "";
                }
                else
                {
                    MelonLogger.Warning("Du bist nicht im Spiel! (You are not in-game!)");
                }
            }
        }

        public override void OnGUI()
        {
            // Debugging: Überprüfen, ob showInput korrekt gesetzt wird
            if (showInput)
            {
                GUI.Box(new Rect(10, 10, 220, 90), "Geld eingeben");

                inputText = GUI.TextField(new Rect(20, 40, 200, 20), inputText, 25);

                if (GUI.Button(new Rect(20, 65, 90, 20), "OK"))
                {
                    if (int.TryParse(inputText, out int moneyAmount))
                    {
                        if (moneyAmount > 0)
                        {
                            if (InGame.instance != null && InGame.instance.bridge != null)
                            {
                                // FIX: Use correct cash source type (assuming 3 is Mod, use your game's enum value for Mod if different)
                                InGame.instance.bridge.AddCash(moneyAmount, (Il2CppAssets.Scripts.Simulation.Simulation.CashSource)3);
                                MelonLogger.Msg($"Spieler bekam {moneyAmount} Geld.");
                            }
                            else
                            {
                                MelonLogger.Warning("InGame bridge nicht verfügbar! (InGame bridge not available!)");
                            }
                        }
                        else
                        {
                            MelonLogger.Warning("Bitte eine positive Zahl eingeben. (Please enter a positive number.)");
                        }
                    }
                    else
                    {
                        MelonLogger.Warning("Ungültige Zahl eingegeben. (Invalid number entered.)");
                    }

                    showInput = false;
                }

                if (GUI.Button(new Rect(130, 65, 90, 20), "Abbrechen"))
                {
                    showInput = false;
                }
            }
        }
    }
}

