using MelonLoader;
using BTD_Mod_Helper;
using CashIDK;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Bloons;
using UnityEngine;

[assembly: MelonInfo(typeof(CashIDK.CashIDK), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace CashIDK;

public class CashIDK : BloonsTD6Mod
{
    // Konfiguration
    private static float moneyMultiplier = 1f;
    private static float fireRateMultiplier = 1f;
    private static bool oneHitEnabled = false;
    private static bool godModeEnabled = false;

    // UI
    private Rect windowRect = new Rect(20, 20, 300, 360);
    private bool showMenu = true;

    public override void OnApplicationStart()
    {
        ModHelper.Msg<CashIDK>("CashIDK loaded!");
    }

    public override void OnUpdate()
    {
        // Menü ein-/ausblenden mit F10
        if (Input.GetKeyDown(KeyCode.F10))
        {
            showMenu = !showMenu;
        }
    }

    public override void OnGUI()
    {
        if (!showMenu) return;

        // UI-Styling: Hacker-Look
        GUI.backgroundColor = Color.black;
        GUI.contentColor = Color.green;
        GUIStyle style = new GUIStyle(GUI.skin.window);
        style.normal.background = Texture2D.blackTexture;
        style.normal.textColor = Color.green;
        style.fontStyle = FontStyle.Bold;
        style.fontSize = 14;

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
        if (InGame.instance == null || InGame.instance.bridge == null) return;

        var sim = InGame.instance.bridge.simulation;
        if (sim == null) return;

        foreach (var tower in sim.towers)
        {
            if (tower?.towerModel == null) continue;

            // Feuerrate anpassen
            foreach (var weapon in tower.towerModel.weapons)
            {
                if (weapon == null) continue;
                weapon.Rate = weapon.originalRate / fireRateMultiplier;
            }
        }
    }

    public override void OnBloonDefeated(Bloon bloon, BloonResult result)
    {
        // Geld-Multiplikator anwenden
        if (result.cash > 0)
            result.cash = (int)(result.cash * moneyMultiplier);

        // One Hit Kill
        if (oneHitEnabled)
            result.damage = bloon.maxHealth;
    }

    public override void OnBloonLeaked(Bloon bloon)
    {
        if (godModeEnabled && InGame.instance != null)
        {
            // Verhindert Lebenabzug bei Leak
            InGame.instance.bridge.SetHealth(InGame.instance.bridge.GetHealth() + bloon.bloonModel.leakDamage);
        }
    }
}