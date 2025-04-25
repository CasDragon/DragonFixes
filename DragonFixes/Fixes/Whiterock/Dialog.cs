using BlueprintCore.Blueprints.Configurators.DialogSystem;
using DragonFixes.Util;
using Kingmaker.ElementsSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFixes.Fixes.Whiterock
{
    internal class Dialog
    {
        [DragonFix]
        public static void PatchOne()
        {
            Main.log.Log("Nurah_GibberingSwarm_dialog - Answer_0037 - condition fix");
            AnswerConfigurator.For("5b7e4e36fd50288448b17af8a30d4cc1")
                .ModifyShowConditions(c => c.Operation = Operation.Or)
                .Configure();

            Main.log.Log("Nurah_GibberingSwarm_dialog - Answer_0038 - condition fix");
            AnswerConfigurator.For("8469c9a00a5c56941a8ff3fd08f61126")
                .ModifyShowConditions(c => c.Operation = Operation.Or)
                .Configure();

            Main.log.Log("FakeHellknights_RegillQ2_dialog - Answer_0004 - show once fix");
            AnswerConfigurator.For("11f916a5a31848b44b9ee12e618d0a91")
                .SetShowOnce(true)
                .Configure();

            Main.log.Log("FakeHellknights_RegillQ2_dialog - Answer_0005 - show once fix");
            AnswerConfigurator.For("556c7ece27cedf042a321c36db2e83c5")
                .SetShowOnce(true)
                .Configure();

            Main.log.Log("FakeHellknights_RegillQ2_dialog - Answer_0006 - show once fix");
            AnswerConfigurator.For("b4f7d904019f654489a569ee4f2f5ad5")
                .SetShowOnce(true)
                .Configure();

            Main.log.Log("Christoff_MainDialogue - Cue_0066 - show once fix, prevents HeroldRespect farming");
            CueConfigurator.For("f8ef88aeec7072f4f92c07910951a5df")
                .SetShowOnce(true)
                .Configure();

            Main.log.Log("BE_Manor - Cue_0132 - condition fix");
            CueConfigurator.For("7a70fd00bea53dd4eab9e0b5a4727285")
                .ModifyConditions(c => c.Operation = Operation.Or)
                .Configure();

            Main.log.Log("BE_Manor - Cue_0272 - condition fix");
            CueConfigurator.For("67264e92f015a0c46aa1781b617db1fe")
                .ModifyConditions(c => c.Operation = Operation.Or)
                .Configure();

            Main.log.Log("TheatrePlay_c5_dialog - Cue_0095 - condition fix");
            CueConfigurator.For("8c084e1190451bb4c801a43b087e67ce")
                .ModifyConditions(c => c.Operation = Operation.Or)
                .Configure();

            Main.log.Log("Opon_AreeluLabAgain_c5_dialog - Answer_0007 - condition fix");
            AnswerConfigurator.For("7b880942686e6da49ba404ea513530b2")
                .ModifyShowConditions(c => c.Operation = Operation.Or)
                .Configure();

            Main.log.Log("Irmangaleth_dialogue - Cue_0183 - show once fix");
            CueConfigurator.For("3207080a55b29e4488c33c1e4522d9bc")
                .SetShowOnce(true)
                .Configure();
        }
    }
}
