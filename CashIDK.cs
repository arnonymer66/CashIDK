using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
// Wichtig: "Instances" ist kein Namespace → using static!
using static BTD_Mod_Helper.Api.Helpers.Instances;

using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Simulation.Bloons;
using UnityEngine;

// ❗ Wenn du `ModHelperData.cs` hast, verwende diese Zeile:
[assembly: MelonInfo(typeof(CashIDK.CashIDK), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
// ❗ Wenn nicht, verwende stattdessen diese:
// [assembly: MelonInfo(typeof(CashIDK.CashIDK), "CashIDK", "1.0.0", "arnonymer66)]

[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace CashIDK
{
    public class CashIDK : BloonsTD6Mod
    {
        private static float moneyMultiplier = 1f;
        private static float fireRateMultiplier = 1f;
        private static bool oneHitEnabled = false;
        private static bool godModeEnabled = false;

        private Rect windowRect = new Rect(20, 20, 300, 360);
        private bool showMenu = true;

        public override void OnApplicationStart()
        {
            ModHelper.Msg<CashIDK>("CashIDK loaded!");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                showMenu = !showMenu;
            }
        }

        public override void OnGUI()
        {
            if (!showMenu) return;

            GUI.backgroundColor = Color.black;
            GUI.contentColor = Color.green;

            GUIStyle style = new GUIStyle(GUI.skin.window)
            {
                normal = { background = Texture2D.blackTexture, textColor = Color.green },
                fontStyle = FontStyle.Bold,
                fontSize = 14
            };

            windowRect = GUI.Window(
                id: 0,
                clientRect: windowRect,
                func: DrawWindow,
                text: "💻 Hacker Mod Menu",
                style: style
            );
        }

        private void DrawWindow(int windowID)
        {
            GUILayout.Label("💰 Geld-Multiplikator:");
            if (GUILayout.Button("1x")) moneyMultiplier = 1f;
            if (GUILayout.Button("2x")) moneyMultiplier = 2f;
            if (GUILayout.Button("5x")) moneyMultiplier = 5f;
            if (GUILayout.Button("10x")) moneyMultiplier = 10f;

            GUILayout.Space(10);

            GUILayout.Label("⚡ Tower-Schussrate:");
            GUILayout.Label($"x{fireRateMultiplier:F1}");
            fireRateMultiplier = GUILayout.HorizontalSlider(fireRateMultiplier, 0.1f, 100f);

            GUILayout.Space(10);

            if (GUILayout.Button(oneHitEnabled ? "☠️ One Hit: AN" : "☠️ One Hit: AUS"))
                oneHitEnabled = !oneHitEnabled;

            GUILayout.Space(10);

            if (GUILayout.Button(godModeEnabled ? "🛡️ God Mode: AN" : "🛡️ God Mode: AUS"))
                godModeEnabled = !godModeEnabled;

            GUILayout.Space(10);
            GUILayout.Label("F10 zum Menü ein-/ausblenden");

            GUI.DragWindow();
        }

        public override void OnLateUpdate()
        {
            if (InGame.instance == null || InGame.instance.bridge == null) return;

            // Beispiel: Zugriff auf Türme über InGame.instance?
            // Du müsstest evtl. manuell an TowerManager ran, wenn das unterstützt wird

            // Beispielhafte Nutzung – auskommentiert bis du Zugriff auf Tower bekommst
            /*
            foreach (var tower in InGame.instance.bridge.GetAllTowers())
            {
                foreach (var weapon in tower.towerModel.weapons)
                {
                    weapon.Rate = weapon.originalRate / fireRateMultiplier;
                }
            }
            */

            // Godmode (nicht implementiert → placeholder)
            if (godModeEnabled)
            {
                // Leider ist kein offizieller Weg dokumentiert, um Leben zu setzen.
                // Du könntest hier ggf. InGame.instance.bridge.SetHealth(...) nutzen, falls verfügbar.
            }
        }

        // OnBloonDefeated entfernt, weil BloonResult nicht mehr existiert in aktuellen APIs
        // Stattdessen müsstest du alternative Methoden nutzen (z. B. Hooks auf bloon events)
    }
}
