using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Simulation;
using UnityEngine;

[assembly: MelonInfo(typeof(MoneyInputMod.Main), "Money Input Mod", "1.1.0", "arnonymer66")]
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
                        else if (key == KeyCode.Backspace && inputText.Length > 0) // Backspace zum Löschen
                        {
                            inputText = inputText.Substring(0, inputText.Length - 1);
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Return)) // Wenn "Enter" gedrückt wird
                    {
                        if (inputText.Length == 1 && int.TryParse(inputText, out int number) && number >= 0 && number <= 9)
                        {
                            // Wenn eine Zahl zwischen 0 und 9 eingegeben wurde, gib 100.000 Geld
                            if (InGame.instance != null && InGame.instance.bridge != null)
                            {
                                InGame.instance.bridge.AddCash(100000, (Il2CppAssets.Scripts.Simulation.Simulation.CashSource)3);
                                MelonLogger.Msg($"Spieler bekam 100.000 Geld.");
                            }
                            else
                            {
                                MelonLogger.Warning("InGame bridge nicht verfügbar! (InGame bridge not available!)");
                            }
                        }
                        else
                        {
                            MelonLogger.Warning("Bitte eine Zahl zwischen 0 und 9 eingeben. (Please enter a number between 0 and 9.)");
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

                // Anzeigen des Textfelds mit der eingegebenen Zahl
                GUILayout.BeginArea(new Rect(10, 10, 220, 90));
                inputText = GUILayout.TextField(inputText, 25); // Eingabefeld für Zahlen

                // OK Button (geht auch durch Enter)
                if (GUILayout.Button("OK") || Input.GetKeyDown(KeyCode.Return))
                {
                    if (inputText.Length == 1 && int.TryParse(inputText, out int number) && number >= 0 && number <= 9)
                    {
                        // Wenn eine Zahl zwischen 0 und 9 eingegeben wurde, gib 100.000 Geld
                        if (InGame.instance != null && InGame.instance.bridge != null)
                        {
                            InGame.instance.bridge.AddCash(100000, (Il2CppAssets.Scripts.Simulation.Simulation.CashSource)3);
                            MelonLogger.Msg($"Spieler bekam 100.000 Geld.");
                        }
                        else
                        {
                            MelonLogger.Warning("InGame bridge nicht verfügbar! (InGame bridge not available!)");
                        }
                    }
                    else
                    {
                        MelonLogger.Warning("Bitte eine Zahl zwischen 0 und 9 eingeben. (Please enter a number between 0 and 9.)");
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

