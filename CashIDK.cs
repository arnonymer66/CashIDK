using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.MainMenu;
using Il2CppAssets.Scripts.Simulation;
using UnityEngine;
using Il2CppAssets.Scripts.Simulation.Simulation;

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
        
        // Referenzen zu den verschiedenen UI-Menüs
        private GameObject modMenuUI = null;
        private static ModSettings menuSettings;

        public override void OnSettingsUI()
        {
            // Hauptmenü-Button - wird im Hauptmenü angezeigt
            if (GUILayout.Button("Mod Menü öffnen"))
            {
                showMenu = !showMenu;
            }

            base.OnSettingsUI();
        }

        public override void OnUpdate()
        {
            // Menüsteuerung
            if (showMenu)
            {
                UpdateMenuOptions();
            }
        }

        private void UpdateMenuOptions()
        {
            // Wenn der GodMode aktiviert ist, setze unendliches Leben für alle Türme
            if (godModeEnabled)
            {
                foreach (var tower in InGame.instance.bridge.GetAllTowers())
                {
                    tower.life = 9999; // Maximale Leben
                }
            }

            // Spielgeschwindigkeit einstellen
            Time.timeScale = gameSpeed;
        }

        public override void OnGUI()
        {
            if (!showMenu) return;

            // Beginne mit dem Menü
            GUILayout.BeginArea(menuRect);

            GUILayout.Label("Monkey Mod Menu", GUILayout.Height(30));

            // God Mode - eigener Bereich
            GUILayout.BeginVertical("box");
            GUILayout.Label("God Mode");
            godModeEnabled = GUILayout.Toggle(godModeEnabled, "Aktivieren: " + (godModeEnabled ? "ON" : "OFF"));
            GUILayout.EndVertical();

            // Monkey Money - eigener Bereich
            GUILayout.BeginVertical("box");
            GUILayout.Label("Monkey Money");
            monkeyMoney = (int)GUILayout.HorizontalSlider(monkeyMoney, 0, 10000);
            GUILayout.Label("Wert: " + monkeyMoney);
            if (GUILayout.Button("Add Monkey Money"))
            {
                if (InGame.instance != null && InGame.instance.bridge != null)
                {
                    InGame.instance.bridge.AddCash(monkeyMoney, (CashSource)3);
                    MelonLogger.Msg($"Spieler bekam {monkeyMoney} Monkey Money.");
                }
            }
            GUILayout.EndVertical();

            // Monkey XP - eigener Bereich
            GUILayout.BeginVertical("box");
            GUILayout.Label("Affen XP");
            monkeyXP = (int)GUILayout.HorizontalSlider(monkeyXP, 0, 10000);
            GUILayout.Label("Wert: " + monkeyXP);
            if (GUILayout.Button("Add Monkey XP"))
            {
                var player = InGame.instance.bridge.GetPlayer();
                if (player != null)
                {
                    player.AddXP(monkeyXP);
                    MelonLogger.Msg($"Spieler bekam {monkeyXP} XP.");
                }
            }
            GUILayout.EndVertical();

            // Spielgeschwindigkeit - eigener Bereich
            GUILayout.BeginVertical("box");
            GUILayout.Label("Spielgeschwindigkeit");
            gameSpeed = GUILayout.HorizontalSlider(gameSpeed, 0.1f, 5.0f);
            GUILayout.Label("Wert: " + gameSpeed.ToString("F2"));
            GUILayout.EndVertical();

            // Affenschaden - eigener Bereich
            GUILayout.BeginVertical("box");
            GUILayout.Label("Affenschaden Multiplier");
            towerDamageMultiplier = GUILayout.HorizontalSlider(towerDamageMultiplier, 0.1f, 10.0f);
            GUILayout.Label("Wert: " + towerDamageMultiplier.ToString("F2"));
            GUILayout.EndVertical();

            GUILayout.EndArea();
        }

        // Mod-Menü im Startmenü anzeigen
        public override void OnMainMenuUI()
        {
            // Button im Profilbereich oder Einstellungen hinzufügen
            if (GUILayout.Button("Mod Menü öffnen"))
            {
                showMenu = !showMenu;
            }
        }

        // Pause-Menü-Modifikation
        public override void OnInGameUI()
        {
            // Button im Pause-Menü hinzufügen
            if (GUILayout.Button("Mod Menü öffnen"))
            {
                showMenu = !showMenu;
            }
        }
    }
}
