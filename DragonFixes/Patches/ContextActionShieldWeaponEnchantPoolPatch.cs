using HarmonyLib;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Designers;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Owlcat.Runtime.Core.Logging;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

namespace DragonFixes.Patches
{
    [HarmonyPatch]
    public static class ContextActionShieldWeaponEnchantPoolPatch
    {
        /*[HarmonyTranspiler]
        [HarmonyPatch(typeof(ContextActionShieldWeaponEnchantPool), nameof(ContextActionShieldWeaponEnchantPool.RunAction))]
        //[HarmonyDebug]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codeMatcher = new CodeMatcher(instructions);
            var originalMethod = typeof(MechanicsContext).GetProperty(nameof(MechanicsContext.MaybeCaster)).GetMethod;
            var newMethod = typeof(MechanicsContext).GetProperty(nameof(MechanicsContext.MainTarget)).GetMethod;

            var groupMethod = typeof(UnitPartActivatableAbility).GetMethod(nameof(UnitPartActivatableAbility.GetGroupSize));
            var bondMethod = typeof(ContextDurationValue).GetMethod(nameof(ContextDurationValue.Calculate));

            codeMatcher.MatchStartForward(
                CodeMatch.Calls(originalMethod))
                .Operand = newMethod;

            codeMatcher.MatchStartForward(
                CodeMatch.Calls(groupMethod));

            if (codeMatcher.IsValid)
            {
                codeMatcher.Advance(-4);
                if (codeMatcher.Instruction.IsLdloc())
                {
                    var newcode = new CodeInstruction(OpCodes.Call, SymbolExtensions.GetMethodInfo((ContextAction action) => GetMaybeCaster(action)));
                    newcode.MoveLabelsFrom(codeMatcher.Instruction);
                    codeMatcher.RemoveInstruction();
                    codeMatcher.Insert(newcode);
                    codeMatcher.Insert(new CodeInstruction(OpCodes.Ldarg_0));
                }
            }

            codeMatcher.MatchStartForward(
                CodeMatch.Calls(bondMethod));

            if (codeMatcher.IsValid)
            {
                codeMatcher.Advance(2);
                if (codeMatcher.Instruction.IsLdloc())
                {
                    var newc = new CodeInstruction(OpCodes.Call, SymbolExtensions.GetMethodInfo((ContextAction action) => GetMaybeCaster(action)));
                    newc.MoveLabelsFrom(codeMatcher.Instruction);
                    codeMatcher.RemoveInstruction();
                    codeMatcher.Insert(newc);
                    codeMatcher.Insert(new CodeInstruction(OpCodes.Ldarg_0));
                }
            }

            return codeMatcher.InstructionEnumeration();
        }

        public static UnitEntityData GetMaybeCaster(ContextAction action)
        {
            return action.Context.MaybeCaster;
        }*/
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ContextActionShieldWeaponEnchantPool), nameof(ContextActionShieldWeaponEnchantPool.RunAction))]
        public static bool RunAction_Patch(ContextActionShieldWeaponEnchantPool __instance)
        {

            UnitEntityData maybeCaster = __instance.Context.MaybeCaster;
            var target = __instance.Context.MainTarget.Unit;
            if (maybeCaster == null || target == null)
            {
                return false;
            }
            UnitPartEnchantPoolData unitPartEnchantPoolData = target.Ensure<UnitPartEnchantPoolData>();
            unitPartEnchantPoolData.ClearEnchantPool(__instance.EnchantPool);
            ItemEntityWeapon itemEntityWeapon;
            if (!RestrictionsHelper.CheckHasShield(target))
            {
                itemEntityWeapon = null;
            }
            else
            {
                ItemEntityShield maybeShield = target.Body.SecondaryHand.MaybeShield;
                itemEntityWeapon = ((maybeShield != null) ? maybeShield.WeaponComponent : null);
            }
            ItemEntityWeapon itemEntityWeapon2 = itemEntityWeapon;
            if (itemEntityWeapon2 == null)
            {
                return false;
            }
            int num = maybeCaster.Ensure<UnitPartActivatableAbility>().GetGroupSize(__instance.Group);
            if (itemEntityWeapon2.Enchantments.Any<ItemEnchantment>())
            {
                num += GameHelper.GetItemEnhancementBonus(itemEntityWeapon2);
            }
            Rounds rounds = __instance.DurationValue.Calculate(__instance.Context);
            foreach (AddBondProperty addBondProperty in maybeCaster.Buffs.SelectFactComponents<AddBondProperty>())
            {
                if (addBondProperty.EnchantPool == __instance.EnchantPool && !itemEntityWeapon2.HasEnchantment(addBondProperty.Enchant))
                {
                    num -= addBondProperty.Enchant.EnchantmentCost;
                    unitPartEnchantPoolData.AddEnchant(itemEntityWeapon2, __instance.EnchantPool, addBondProperty.Enchant, __instance.Context, rounds);
                }
            }
            int num2 = Math.Min(5, num);
            if (num2 > 0)
            {
                BlueprintItemEnchantment blueprintItemEnchantment = __instance.DefaultEnchantments[num2 - 1];
                unitPartEnchantPoolData.AddEnchant(itemEntityWeapon2, __instance.EnchantPool, blueprintItemEnchantment, __instance.Context, rounds);
            }
            return false;
        }
    }
}
