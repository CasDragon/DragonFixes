using HarmonyLib;
using Kingmaker;
using Kingmaker.AreaLogic.Etudes;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Patches
{
    [HarmonyPatch]
    public static class EtudeStatusNonsense
    {
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
            if (s.m_AreaPartBeingLoaded == null)
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
    }
}
