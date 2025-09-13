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
        private bool isFocused = false;

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.E)) // "E" zum Ein-/Ausblenden des Eingabefelds
            {
                if (showInput)
                {
                    showInput = false; // Verstecke das Eingabefeld, wenn es schon offen ist
                }
                else
                {
                    if (InGame.instance != null)
                    {
                        showInput = true; // Zeige das Eingabefeld
                        inputText = ""; // Setze das Eingabefeld zurück
                        isFocused = true; // Setze den Fokus
                    }
                    else
                    {
                        MelonLogger.Warning("Du bist nicht im Spiel! (You are not in-game!)");
                    }
                }
            }

            // Erlaube nur Zahlen von 0-9 und Verarbeite die Eingabe mit Enter
            if (showInput)
            {
                if (Input.anyKeyDown)
                {
                    foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        if (key.ToString().StartsWith("Alpha") && Input.GetKeyDown(key))
                        {
                            // Zahlen (0-9) einfügen
                            inputText += key.ToString().Replace("Alpha", "");
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Return)) // Wenn "Enter" gedrückt wird
                    {
                        if (int.TryParse(inputText, out int moneyAmount))
                        {
                            if (moneyAmount > 0)
                            {
                                if (InGame.instance != null && InGame.instance.bridge != null)
                                {
                                    // FIX: Verwende den CashSource-Wert für Modding
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

                        showInput = false; // Verstecke das Eingabefeld nach Drücken von Enter
                    }
                }
            }
        }

        public override void OnGUI()
        {
            if (showInput)
            {
                GUI.Box(new Rect(10, 10, 220, 90), "Geld eingeben");

                // Anzeigen des Textfelds
                GUILayout.BeginArea(new Rect(10, 10, 220, 90));
                inputText = GUILayout.TextField(inputText, 25); // Eingabefeld für Zahlen

                // OK Button (geht auch durch Enter)
                if (GUILayout.Button("OK") || Input.GetKeyDown(KeyCode.Return))
                {
                    if (int.TryParse(inputText, out int moneyAmount))
                    {
                        if (moneyAmount > 0)
                        {
                            if (InGame.instance != null && InGame.instance.bridge != null)
                            {
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

                    showInput = false; // Verstecke das Eingabefeld nach Drücken von OK oder Enter
                }

                // Abbrechen Button
                if (GUILayout.Button("Abbrechen"))
                {
                    showInput = false; // Verstecke das Eingabefeld, wenn abgebrochen wird
                }

                GUILayout.EndArea();
            }
        }
    }
}

