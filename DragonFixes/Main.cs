﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Utils;
using DragonFixes.Fixes;
using DragonFixes.Util;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using UnityModManagerNet;
using static UnityModManagerNet.UnityModManager;

namespace DragonFixes
{
    public static class Main
    {
        internal static Harmony HarmonyInstance;
        internal static UnityModManager.ModEntry.ModLogger log;
        internal static ModEntry entry;

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            entry = modEntry;
            log = modEntry.Logger;
            modEntry.OnGUI = OnGUI;
            HarmonyInstance = new Harmony(modEntry.Info.Id);
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            return true;
        }

        public static void OnGUI(UnityModManager.ModEntry modEntry)
        {

        }
        [HarmonyPatch(typeof(BlueprintsCache))]
        public static class BlueprintsCaches_Patch
        {
            private static bool Initialized = false;

            [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
            public static void Init_Postfix()
            {
                try
                {
                    if (Initialized)
                    {
                        log.Log("Already initialized blueprints cache.");
                        return;
                    }
                    Initialized = true;

                    log.Log("Adding DragonFix settings");
                    Settings.InitializeSettings();
                    log.Log("Patching blueprints.");
                    Thingy.DoPatches();
                }
                catch (Exception e)
                {
                    log.Log(string.Concat("Failed to initialize.", e));
                }
            }
        }
    }
}
