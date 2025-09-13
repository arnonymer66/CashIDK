using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Simulation; // <--- This line is important
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Show the input box only if the game is running
                if (InGame.instance != null)
                {
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
                                // FIX: CashSource.Mod is available because of the using statement above
                                InGame.instance.bridge.AddCash(moneyAmount, CashSource.Mod);
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
