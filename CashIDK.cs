using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Simulation;
using UnityEngine;
using System.Collections.Generic;

// Assembly attributes go here, after using statements!
[assembly: MelonInfo(typeof(MoneyInputMod.Main), "Advanced Monkey Mod", "1.0.0", "DeinName")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace MoneyInputMod
{
    public class Main : BloonsTD6Mod
    {
        private bool showMenu = false;
        private float gameSpeed = 1.0f; 
        private bool godModeEnabled = false;
        private bool infiniteXP = false;
        private bool infiniteMoney = false;
        private bool unlockAllTrophies = false;
        private bool towerDamageBoost = false;
        private static ModSettings menuSettings;

        public override void OnSettingsUI()
        {
            GUILayout.Label("Mod Einstellungen");
            if (GUILayout.Button("Mod Menü öffnen"))
            {
                showMenu = !showMenu;
            }
            base.OnSettingsUI();
        }

        public override void OnUpdate()
        {
            Time.timeScale = gameSpeed;
            if (godModeEnabled) EnableGodMode();
            if (infiniteXP) GrantInfiniteXP();
            if (infiniteMoney) GrantInfiniteMoney();
            if (unlockAllTrophies) UnlockAllTrophies();
            if (towerDamageBoost) BoostTowerDamage();
        }

        public override void OnGUI()
        {
            if (!showMenu) return;

            GUILayout.BeginArea(new Rect(10, 10, 250, 500));
            GUILayout.Label("Monkey Mod Menü", GUILayout.Height(30));

            GUILayout.BeginVertical("box");
            GUILayout.Label("God Mode");
            godModeEnabled = GUILayout.Toggle(godModeEnabled, "Aktivieren: " + (godModeEnabled ? "ON" : "OFF"));
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUILayout.Label("Unendliche XP");
            infiniteXP = GUILayout.Toggle(infiniteXP, "Aktivieren: " + (infiniteXP ? "ON" : "OFF"));
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUILayout.Label("Unendlich Geld");
            infiniteMoney = GUILayout.Toggle(infiniteMoney, "Aktivieren: " + (infiniteMoney ? "ON" : "OFF"));
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUILayout.Label("Alle Trophäen Freischalten");
            unlockAllTrophies = GUILayout.Toggle(unlockAllTrophies, "Aktivieren: " + (unlockAllTrophies ? "ON" : "OFF"));
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUILayout.Label("Tower Schaden Boost");
            towerDamageBoost = GUILayout.Toggle(towerDamageBoost, "Aktivieren: " + (towerDamageBoost ? "ON" : "OFF"));
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            GUILayout.Label("Spielgeschwindigkeit");
            gameSpeed = GUILayout.HorizontalSlider(gameSpeed, 0.1f, 5.0f);
            GUILayout.Label("Wert: " + gameSpeed.ToString("F2"));
            GUILayout.EndVertical();

            GUILayout.EndArea();
        }

        private void EnableGodMode()
        {
            foreach (var tower in InGame.instance.bridge.GetAllTowers())
            {
                tower.life = 9999;
            }
        }

        private void GrantInfiniteXP()
        {
            // Implement XP logic here
        }

        private void GrantInfiniteMoney()
        {
            foreach (var player in InGame.instance.bridge.GetAllPlayers())
            {
                player.money = 9999999;
            }
        }

        private void UnlockAllTrophies()
        {
            // Implement trophy unlock logic here
        }

        private void BoostTowerDamage()
        {
            foreach (var tower in InGame.instance.bridge.GetAllTowers())
            {
                tower.damage *= 10;
            }
        }

        public override void OnInGameUI()
        {
            if (GUILayout.Button("Mod Menü öffnen"))
            {
                showMenu = !showMenu;
            }
        }

        public override void OnMainMenuUI()
        {
            if (GUILayout.Button("Mod Menü öffnen"))
            {
                showMenu = !showMenu;
            }
        }
    }
}
