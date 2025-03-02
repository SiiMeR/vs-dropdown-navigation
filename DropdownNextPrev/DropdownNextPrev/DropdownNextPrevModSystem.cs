using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace DropdownNextPrev;

public class DropdownNextPrevModSystem : ModSystem
{
    public override void StartClientSide(ICoreClientAPI api)
    {
        var harmony = new Harmony(Mod.Info.ModID);
        harmony.PatchAll();

        var overload1 =
            AccessTools.Method(typeof(GuiComposerHelpers), nameof(GuiComposerHelpers.AddDropDown),
                new[]
                {
                    typeof(GuiComposer),
                    typeof(string[]), typeof(string[]), typeof(int), typeof(SelectionChangedDelegate),
                    typeof(ElementBounds), typeof(string)
                });


        var patch1 = AccessTools.Method(typeof(AddDropdownPatch), nameof(AddDropdownPatch.Prefix));

        harmony.Patch(overload1, new HarmonyMethod(patch1));


        var overload2 =
            AccessTools.Method(typeof(GuiComposerHelpers), nameof(GuiComposerHelpers.AddDropDown),
                new[]
                {
                    typeof(GuiComposer),
                    typeof(string[]), typeof(string[]), typeof(int), typeof(SelectionChangedDelegate),
                    typeof(ElementBounds), typeof(CairoFont), typeof(string)
                });

        var patch2 = AccessTools.Method(typeof(AddDropdownPatch), nameof(AddDropdownPatch.Prefix2));
        harmony.Patch(overload2, new HarmonyMethod(patch2));

        base.StartClientSide(api);
    }
}