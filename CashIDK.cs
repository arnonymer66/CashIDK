using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Simulation;
using UnityEngine;
using System.Collections.Generic;

// Assembly attributes should be AFTER all using statements
[assembly: MelonInfo(typeof(MoneyInputMod.Main), "Advanced Monkey Mod", "1.0.0", "DeinName")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace MoneyInputMod
{
    public class Main : BloonsTD6Mod
    {
        // Menü anzeigen Flag
        private bool showMenu = false;

        // Game Speed und andere Modi
        private float gameSpeed = 1.0f; 
        private bool godModeEnabled = false;
        private bool infiniteXP = false;
        private bool infiniteMoney = false;
        private bool unlockAllTrophies = false;
        private bool towerDamageBoost = false;

        // ModSettings-Instanz
        private static ModSettings menuSettings;

        // Diese Methode überschreibt die UI-Einstellungen
        public override void OnSettingsUI()
        {
            GUILayout.Label("Mod Einstellungen");

            // Schalter für das Mod-Menü
            if (GUILayout.Button("Mod Menü öffnen"))
            {
                showMenu = !showMenu;
            }

            base.OnSettingsUI();
        }

        // Diese Methode wird bei jedem Update des Spiels aufgerufen
        public override void OnUpdate()
        {
            // Spielgeschwindigkeit anpassen
            Time.timeScale = gameSpeed;

            if (godModeEnabled)
            {
                EnableGodMode();
            }

            if (infiniteXP)
            {
                GrantInfiniteXP();
            }

            if (infiniteMoney)
            {
                GrantInfiniteMoney();
            }

            if (unlockAllTrophies)
            {
                UnlockAllTrophies();
            }

            if (towerDamageBoost)
            {
                BoostTowerDamage();
            }
        }

        // Diese Methode rendert das Mod-Menü im Spiel
        public override void OnGUI()
        {
            if (!showMenu) return;

            // Mod-Menü anzeigen
            GUILayout.BeginArea(new Rect(10, 10, 250, 500));
            GUILayout.Label("Monkey Mod Menü", GUILayout.Height(30));

            // God Mode
            GUILayout.BeginVertical("box");
            GUILayout.Label("God Mode");
            godModeEnabled = GUILayout.Toggle(godModeEnabled, "Aktivieren: " + (godModeEnabled ? "ON" : "OFF"));
            GUILayout.EndVertical();

            // XP
            GUILayout.BeginVertical("box");
            GUILayout.Label("Unendliche XP");
            infiniteXP = GUILayout.Toggle(infiniteXP, "Aktivieren: " + (infiniteXP ? "ON" : "OFF"));
            GUILayout.EndVertical();

            // Money
            GUILayout.BeginVertical("box");
            GUILayout.Label("Unendlich Geld");
            infiniteMoney = GUILayout.Toggle(infiniteMoney, "Aktivieren: " + (infiniteMoney ? "ON" : "OFF"));
            GUILayout.EndVertical();

            // Trophäen
            GUILayout.BeginVertical("box");
            GUILayout.Label("Alle Trophäen Freischalten");
            unlockAllTrophies = GUILayout.Toggle(unlockAllTrophies, "Aktivieren: " + (unlockAllTrophies ? "ON" : "OFF"));
            GUILayout.EndVertical();

            // Tower Schaden
            GUILayout.BeginVertical("box");
            GUILayout.Label("Tower Schaden Boost");
            towerDamageBoost = GUILayout.Toggle(towerDamageBoost, "Aktivieren: " + (towerDamageBoost ? "ON" : "OFF"));
            GUILayout.EndVertical();

            // Spielgeschwindigkeit
            GUILayout.BeginVertical("box");
            GUILayout.Label("Spielgeschwindigkeit");
            gameSpeed = GUILayout.HorizontalSlider(gameSpeed, 0.1f, 5.0f);
            GUILayout.Label("Wert: " + gameSpeed.ToString("F2"));
            GUILayout.EndVertical();

            GUILayout.EndArea();
        }

        private void EnableGodMode()
        {
            // Setze die Lebenspunkte aller Türme auf ein sehr hohes Level
            foreach (var tower in InGame.instance.bridge.GetAllTowers())
            {
                tower.life = 9999;
            }
        }

        private void GrantInfiniteXP()
        {
            // XP-Logik (Beispiel)
            // Hier könntest du die XP eines Spielers unendlich machen. Es kann von der internen API von BloonsTD6 abhängen, wie du auf XP zugreifen und es ändern kannst.
        }

        private void GrantInfiniteMoney()
        {
            // Unendlich Geld
            foreach (var player in InGame.instance.bridge.GetAllPlayers())
            {
                player.money = 9999999; // Setze Geld auf einen sehr hohen Wert
            }
        }

        private void UnlockAllTrophies()
        {
            // Trophäen freischalten
            // Beispiel, wie man alle Trophäen freischaltet (wird die API von BloonsTD6 benötigt)
            // Du kannst mit der Trophy-System-API von BTD6 arbeiten, um alle Trophäen zu aktivieren
        }

        private void BoostTowerDamage()
        {
            // Tower Schaden boost
            foreach (var tower in InGame.instance.bridge.GetAllTowers())
            {
                tower.damage *= 10; // Beispielsweise den Schaden um den Faktor 10 erhöhen
            }
        }

        // Mod-Menü im Pause-Menü anzeigen
        public override void OnInGameUI()
        {
            if (GUILayout.Button("Mod Menü öffnen"))
            {
                showMenu = !showMenu;
            }
        }

        // Mod-Menü im Hauptmenü anzeigen
        public override void OnMainMenuUI()
        {
            if (GUILayout.Button("Mod Menü öffnen"))
            {
                showMenu = !showMenu;
            }
        }
    }
}
