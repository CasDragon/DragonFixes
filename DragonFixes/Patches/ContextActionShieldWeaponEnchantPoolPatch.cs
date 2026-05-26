using HarmonyLib;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

namespace DragonFixes.Patches
{
    [HarmonyPatch]
    public static class ContextActionShieldWeaponEnchantPoolPatch
    {
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(ContextActionShieldWeaponEnchantPool), nameof(ContextActionShieldWeaponEnchantPool.RunAction))]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codeMatcher = new CodeMatcher(instructions);
            var originalMethod = typeof(MechanicsContext).GetProperty(nameof(MechanicsContext.MaybeCaster)).GetMethod;
            var newMethod = typeof(MechanicsContext).GetProperty(nameof(MechanicsContext.MainTarget)).GetMethod;

            var groupMethod = typeof(EntityDataBase).GetMethod(nameof(EntityDataBase.Ensure)).MakeGenericMethod([typeof(UnitPartEnchantPoolData)]);
            var bondMethod = typeof(UnitEntityData).GetMethod(nameof(UnitEntityData.Buffs.SelectFactComponents));

            codeMatcher.MatchStartForward(
                CodeMatch.Calls(originalMethod))
                .Operand = newMethod;

            codeMatcher.MatchStartForward(
                CodeMatch.Calls(groupMethod));

            if (codeMatcher.IsValid)
            {
                codeMatcher.MatchStartBackwards(CodeMatch.IsLdloc());
                if (codeMatcher.IsValid)
                {
                    var newcode = new CodeInstruction(OpCodes.Call, SymbolExtensions.GetMethodInfo(() => GetMaybeCaster));
                    newcode.MoveLabelsFrom(codeMatcher.Instruction);
                    codeMatcher.RemoveInstruction();
                    codeMatcher.Insert(newcode);
                }
            }

            codeMatcher.MatchStartForward(
                CodeMatch.Calls(bondMethod));

            if (codeMatcher.IsValid)
            {
                codeMatcher.MatchStartBackwards(CodeMatch.IsLdloc());
                if (codeMatcher.IsValid)
                {
                    var newc = new CodeInstruction(OpCodes.Call, SymbolExtensions.GetMethodInfo(() => GetMaybeCaster));
                    newc.MoveLabelsFrom(codeMatcher.Instruction);
                    codeMatcher.RemoveInstruction();
                    codeMatcher.Insert(newc);
                }
            }

            return codeMatcher.InstructionEnumeration();
        }

        public static UnitEntityData GetMaybeCaster(ContextAction action)
        {
            return action.Context.MaybeCaster;
        }
        /*[HarmonyPrefix]
        [HarmonyPatch(typeof(ContextActionShieldWeaponEnchantPool), nameof(ContextActionShieldWeaponEnchantPool.RunAction))]
        public static bool RunAction_Patch(ContextActionShieldWeaponEnchantPool __instance)
        {

        }*/
    }
}
