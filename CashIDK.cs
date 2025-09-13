using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Api.Helpers.Instances;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Simulation.Bloons;
using UnityEngine;

[assembly: MelonInfo(typeof(CashIDK.CashIDK), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
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
            GUIStyle style = new GUIStyle(GUI.skin.window);
            style.normal.background = Texture2D.blackTexture;
            style.normal.textColor = Color.green;
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 14;

            // Hier: Window mit Stil überladenen Parameter korrekt setzen.
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
            // Hole die Simulation, wenn verfügbar
            var sim = Simulation.Instance;  // oder: Instances.Simulation, je nach API
            if (sim == null) return;

            // Fire rate ändern: -> abhängig davon, was verfügbar ist — evtl. über TowerManager?
            // Hier ein vereinfachter Ansatz, wenn TowerManager existiert:

            var tm = TowerManager.Instance;
            if (tm != null)
            {
                // Ich weiß nicht, ob tm.Towers existiert, das musst du in deiner API prüfen
                // Pseudocode:
                // foreach (var tower in tm.GetAllTowers())
                // {
                //     foreach (var weapon in tower.towerModel.weapons)
                //         weapon.Rate = weapon.originalRate / fireRateMultiplier;
                // }
            }

            // Godmode: Leben auf max setzen, wenn möglich
            var uts = UnityToSimulation.Instance;
            if (godModeEnabled && uts != null)
            {
                // Prüfe ob GetMaxHealth und SetHealth Methoden existieren
                // Wenn nicht, diesen Teil auskommentieren oder entfernen
                // uts.SetHealth(uts.GetMaxHealth());
            }
        }
    }
}

