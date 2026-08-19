using HarmonyLib;
using Kingmaker;
using Kingmaker.AreaLogic.Etudes;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonLibrary.Utils;

namespace DragonFixes.Patches
{
    /*[HarmonyPatch]
    [HarmonyPatchCategory("emergencyareaetudethingy")]
    public static class EtudeStatusNonsense
    {
        private const string SettingName = "emergencyareaetudethingy";

        private const string SettingDescription =
            "DO NOT ENABLE WITHOUT BEING TOLD TO, THIS WILL BREAK OTHER THINGS!!!!!\nThis is a workaround for edge cases where "
            + "an area is loaded before the etude it is looking for loads. The only known time this has happened so far is "
            + "with the Act 3 Demon mythic quest, where you are supposed to be kidnapped. To use, toggle on, load into the "
            + "area effected, and then toggle this setting off.\n"
            + "AGAIN, DO NOT ENABLE WITHOUT BEING TOLD TO, IT BREAKS OTHER THINGS";

        public static void ChangePatchStatus(bool enabled)
        {
            if (enabled)
            {
                Main.log.Log("EtudeStatusNonsense enabled");
                Main.HarmonyInstance.PatchCategory("emergencyareaetudethingy");
            }
            else
            {
                Main.log.Log("EtudeStatusNonsense disabled");
                Main.HarmonyInstance.UnpatchCategory("emergencyareaetudethingy");
            }
        }

        [DragonSetting(SettingCategories.None, SettingName, SettingDescription, typeof(EtudeStatusNonsense), nameof(ChangePatchStatus), false)]
        [DragonConfigure]
        private static void ApplyPatch()
        {
            ChangePatchStatus(false);
            SettingsAction.SetSettingToggle(SettingName, false);
        }
        
        [HarmonyPatch(typeof(EtudeStatus), nameof(EtudeStatus.CheckCondition)), HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Trans(IEnumerable<CodeInstruction> instructions)
        {
            var m = AccessTools.PropertyGetter(typeof(Etude), nameof(Etude.IsPlaying));
            foreach (var inst in instructions)
            {
                if (inst.Calls(m))
                {
                    inst.operand = ((Func<Etude, bool>)IsPlayingOrToBePlayed).Method;
                }
                yield return inst;
            }
        }
        [ThreadStatic]
        private static HashSet<BlueprintEtude>? m_CurrentSearchedSet;
        private static bool IsPlayingOrToBePlayed(Etude etude)
        {
            var s = Game.Instance.Player.EtudesSystem;
            if (s.m_AreaPartBeingLoaded == null || etude.IsActive)
            {
                return etude.IsActive;
            }
            else
            {
                m_CurrentSearchedSet ??= new();
                if (m_CurrentSearchedSet.Contains(etude.Blueprint))
                {
                    // Recursive Dependencies ._.
                    return etude.IsActive;
                }
                m_CurrentSearchedSet.Add(etude.Blueprint);
                var tree = s.Etudes;
                var b = tree.EtudeCanPlay(etude);
                var e = etude;
                var ret = false;
                while (b)
                {
                    if (e.Parent == null)
                    {
                        ret = true;
                        break;
                    }
                    else
                    {
                        e = e.Parent;
                        b = tree.EtudeCanPlay(e);
                    }
                }
                m_CurrentSearchedSet.Remove(etude.Blueprint);
                return ret;
            }
        }
    }*/
}
