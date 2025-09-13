using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Api.Helpers;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
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
                showMenu = !showMenu;
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

            windowRect = GUI.Window(0, windowRect, DrawWindow, "💻 Hacker Mod Menu", style);
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
            var ingameInstance = Instances.InGame;
            if (ingameInstance == null || ingameInstance.bridge == null) return;

            if (godModeEnabled)
            {
                // Beispiel: evtl. Leben setzen wenn API es zulässt
                // ingameInstance.bridge.SetHealth(ingameInstance.bridge.GetMaxHealth());
            }
        }
    }
}
