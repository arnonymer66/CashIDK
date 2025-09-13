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

        // Flag, um den Fokus auf das Textfeld zu setzen
        private bool isFocused = false;

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Zeige das Eingabefeld, wenn das Spiel läuft
                if (InGame.instance != null)
                {
                    showInput = true;
                    inputText = ""; // Eingabefeld zurücksetzen
                    isFocused = true; // Setze den Fokus
                }
                else
                {
                    MelonLogger.Warning("Du bist nicht im Spiel! (You are not in-game!)");
                }
            }
        }

        public override void OnGUI()
        {
            if (showInput)
            {
                GUI.Box(new Rect(10, 10, 220, 90), "Geld eingeben");

                // Verwende GUILayout.TextField anstelle von GUI.TextField
                GUILayout.BeginArea(new Rect(10, 10, 220, 90));
                inputText = GUILayout.TextField(inputText, 25);

                if (GUILayout.Button("OK"))
                {
                    if (int.TryParse(inputText, out int moneyAmount))
                    {
                        if (moneyAmount > 0)
                        {
                            if (InGame.instance != null && InGame.instance.bridge != null)
                            {
                                // Geld hinzufügen
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

                if (GUILayout.Button("Abbrechen"))
                {
                    showInput = false;
                }
                GUILayout.EndArea();
            }
        }
    }
}
