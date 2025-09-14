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
        private bool showMenu = false;

        // Variablen für die Funktionen
        private bool godModeEnabled = false;
        private float towerSpeed = 1.0f;
        private int monkeyXP = 0;
        private int monkeyMoney = 0;
        private int trophies = 0;
        private float gameSpeed = 1.0f; // Spielgeschwindigkeit
        private float towerDamageMultiplier = 1.0f; // Affenschaden Multiplier

        // Menü-Position und Größe
        private Rect menuRect = new Rect(10, 10, 250, 700);
        private bool showGodModeSection = true;
        private bool showMonkeyMoneySection = true;
        private bool showMonkeyXPSection = true;
        private bool showTowerSpeedSection = true;
        private bool showTrophiesSection = true;
        private bool showTowerDamageSection = true;
        private bool showGameSpeedSection = true;

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.E)) // "E" zum Ein-/Ausblenden des Menüs
            {
                showMenu = !showMenu;
            }
        }

        public override void OnGUI()
        {
            if (!showMenu) return;

            // Beginne mit dem Menü
            GUILayout.BeginArea(menuRect);

            GUILayout.Label("Monkey Mod Menu", GUILayout.Height(30));

            // God Mode - eigener Bereich
            if (showGodModeSection)
            {
                GUILayout.BeginVertical("box");
                GUILayout.Label("God Mode");
                godModeEnabled = GUILayout.Toggle(godModeEnabled, "Aktivieren: " + (godModeEnabled ? "ON" : "OFF"));
                if (godModeEnabled)
                {
                    foreach (var tower in InGame.instance.bridge.GetAllTowers())
                    {
                        tower.life = 9999; // Unendlich Leben für Türme
                    }
                }
                GUILayout.EndVertical();
            }

            // Monkey Money - eigener Bereich
            if (showMonkeyMoneySection)
            {
                GUILayout.BeginVertical("box");
                GUILayout.Label("Monkey Money");
                monkeyMoney = (int)GUILayout.HorizontalSlider(monkeyMoney, 0, 10000);
                GUILayout.Label("Wert: " + monkeyMoney);
                if (GUILayout.Button("Add Monkey Money"))
                {
                    if (InGame.instance != null && InGame.instance.bridge != null)
                    {
                        InGame.instance.bridge.AddCash(monkeyMoney, (Il2CppAssets.Scripts.Simulation.Simulation.CashSource)3);
                        MelonLogger.Msg($"Spieler bekam {monkeyMoney} Monkey Money.");
                    }
                }
                GUILayout.EndVertical();
            }

            // Affen XP - eigener Bereich
            if (showMonkeyXPSection)
            {
                GUILayout.BeginVertical("box");
                GUILayout.Label("Affen XP");
                monkeyXP = (int)GUILayout.HorizontalSlider(monkeyXP, 0, 10000);
                GUILayout.Label("Wert: " + monkeyXP);
                if (GUILayout.Button("Add Monkey XP"))
                {
                    var player = InGame.instance.bridge.GetPlayer();
                    player.AddXP(monkeyXP);
                    MelonLogger.Msg($"Spieler bekam {monkeyXP} XP.");
                }
                GUILayout.EndVertical();
            }

            // Türmenschussgeschwindigkeit - eigener Bereich
            if (showTowerSpeedSection)
            {
                GUILayout.BeginVertical("box");
                GUILayout.Label("Türmenschussgeschwindigkeit");
                towerSpeed = GUILayout.HorizontalSlider(towerSpeed, 0.1f, 5.0f);
                GUILayout.Label("Wert: " + towerSpeed.ToString("F2"));
                if (GUILayout.Button("Set Tower Speed"))
                {
                    foreach (var tower in InGame.instance.bridge.GetAllTowers())
                    {
                        tower.projectileSpeed = towerSpeed;
                    }
                    MelonLogger.Msg($"Schussgeschwindigkeit der Türme auf {towerSpeed} gesetzt.");
                }
                GUILayout.EndVertical();
            }

            // Trophäen - eigener Bereich
            if (showTrophiesSection)
            {
                GUILayout.BeginVertical("box");
                GUILayout.Label("Trophäen: " + trophies);
                if (GUILayout.Button("Add Trophy"))
                {
                    trophies++;
                    MelonLogger.Msg($"Trophäen erhöht: {trophies}");
                }
                GUILayout.EndVertical();
            }

            // Affenschaden - eigener Bereich
            if (showTowerDamageSection)
            {
                GUILayout.BeginVertical("box");
                GUILayout.Label("Affenschaden Multiplier");
                towerDamageMultiplier = GUILayout.HorizontalSlider(towerDamageMultiplier, 0.1f, 10.0f);
                GUILayout.Label("Wert: " + towerDamageMultiplier.ToString("F2"));
                if (GUILayout.Button("Set Tower Damage"))
                {
                    foreach (var tower in InGame.instance.bridge.GetAllTowers())
                    {
                        // Beispiel: Erhöhe den Schaden des Turms
                        tower.attack *= towerDamageMultiplier;
                    }
                    MelonLogger.Msg($"Affenschaden der Türme auf {towerDamageMultiplier} gesetzt.");
                }
                GUILayout.EndVertical();
            }

            // Spielgeschwindigkeit - eigener Bereich
            if (showGameSpeedSection)
            {
                GUILayout.BeginVertical("box");
                GUILayout.Label("Spielgeschwindigkeit");
                gameSpeed = GUILayout.HorizontalSlider(gameSpeed, 0.1f, 5.0f);
                GUILayout.Label("Wert: " + gameSpeed.ToString("F2"));
                if (GUILayout.Button("Set Game Speed"))
                {
                    Time.timeScale = gameSpeed;
                    MelonLogger.Msg($"Spielgeschwindigkeit auf {gameSpeed} gesetzt.");
                }
                GUILayout.EndVertical();
            }

            GUILayout.EndArea();
        }
    }
}
