using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Kingmaker;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonFixes.Patches;

[HarmonyPatch]
public class RuleCollectMetamagicPatch
{
    // Fix for not being able to apply metamagics at level 9 even with completely normal spell & favored metamagic
    
    [HarmonyPatch(typeof(RuleCollectMetamagic), nameof(RuleCollectMetamagic.AddMetamagic))]
    [HarmonyTranspiler]
    static IEnumerable<CodeInstruction> AddMetamagicPatch(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        MethodInfo defaultCostMethod = AccessTools.Method(typeof(MetamagicHelper), nameof(MetamagicHelper.DefaultCost));

        var matcher = new CodeMatcher(instructions, generator)
            .MatchStartForward(
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(OpCodes.Ldfld),
                new CodeMatch(OpCodes.Ldloc_0),
                new CodeMatch(OpCodes.Ldfld),
                new CodeMatch(ci => ci.opcode == OpCodes.Call && (ci.operand as MethodInfo) == defaultCostMethod),
                new CodeMatch(OpCodes.Add),
                new CodeMatch(OpCodes.Ldc_I4_S),
                new CodeMatch(ci => ci.opcode == OpCodes.Ble_S || ci.opcode == OpCodes.Ble),
                new CodeMatch(OpCodes.Ret)
            )
            .ThrowIfInvalid("RuleCollectMetamagic.AddMetamagic patch: pattern not found — method may have changed.");

        // Capture labels on the first matched instruction (ldarg.0) before we remove it,
        // since the earlier `m_SpellLevel >= 10` check's blt.s branches straight in here.
        List<Label> labelsToPreserve = matcher.Instruction.labels;

        matcher.RemoveInstructions(9); // removes ldarg.0 ... ret (the whole 9-instruction block)

        // matcher now sits on the instruction right after the removed block (ldarg.0 for m_SpellLevel + DefaultCost check's caller, index 41 in original)
        matcher.Instruction.labels.AddRange(labelsToPreserve);

        return matcher.InstructionEnumeration();
    }
    
    /*[HarmonyPatch(typeof(RuleCollectMetamagic), nameof(RuleCollectMetamagic.AddMetamagic))]
    [HarmonyPrefix]
    public static bool AddMetamagicPatch(RuleCollectMetamagic __instance, Feature metamagicFeature)
    {
        AddMetamagicFeat component = metamagicFeature.GetComponent<AddMetamagicFeat>();
        if (component == null)
        {
            return false;
        }
        Metamagic metamagic = component.Metamagic;
        __instance.KnownMetamagics.Add(metamagicFeature);
        switch (__instance.m_SpellLevel)
        {
            case < 0:
            case >= 10:
                return false;
        }
        if (__instance.Spell != null && !__instance.SpellMetamagics.Contains(metamagicFeature) && (__instance.Spell.AvailableMetamagic & metamagic) == metamagic)
        {
            __instance.SpellMetamagics.Add(metamagicFeature);
        }
        return false;
    }*/
}