using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
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
            // Check if "E" is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                showInput = true;
                inputText = "";
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
                        InGame.instance.AddCash(moneyAmount);
                        MelonLogger.Msg($"Spieler bekam {moneyAmount} Geld.");
                    }
                    else
                    {
                        MelonLogger.Warning("Ungültige Zahl eingegeben.");
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
