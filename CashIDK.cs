using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Helpers;
using UnityEngine;

[assembly: MelonInfo(typeof(CashIDK.CashIDK), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace CashIDK
{
    public class CashIDK : BloonsTD6Mod
    {
        private static float fireRateMultiplier = 1f;
        private static bool godModeEnabled = false;
        private Rect windowRect = new Rect(20, 20, 300, 200);
        private bool showMenu = true;

        public override void OnApplicationStart()
        {
            ModHelper.Msg<CashIDK>("CashIDK geladen!");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F10))
                showMenu = !showMenu;
        }

        public override void OnGUI()
        {
            if (!showMenu) return;
            windowRect = GUI.Window(0, windowRect, DrawWindow, "CashIDK Menu");
        }

        private void DrawWindow(int windowID)
        {
            GUILayout.Label("Fire Rate Multiplier");
            GUILayout.Label($"x{fireRateMultiplier:F1}");
            fireRateMultiplier = GUILayout.HorizontalSlider(fireRateMultiplier, 0.1f, 10f);

            GUILayout.Space(10);

            if (GUILayout.Button(godModeEnabled ? "🛡️ God Mode: AN" : "🛡️ God Mode: AUS"))
                godModeEnabled = !godModeEnabled;

            GUILayout.Label("F10 zum Menü ein-/ausblenden");
            GUI.DragWindow();
        }

        public override void OnLateUpdate()
        {
            var ingame = Instances.InGame;
            if (ingame == null || ingame.bridge == null) return;

            if (godModeEnabled)
            {
                // Beispiel: Max Health setzen
                // ingame.bridge.SetHealth(ingame.bridge.GetMaxHealth());
            }
        }
    }
}

