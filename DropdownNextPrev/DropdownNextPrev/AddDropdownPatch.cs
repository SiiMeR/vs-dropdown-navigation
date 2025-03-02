using Cairo;
using HarmonyLib;
using Vintagestory.API.Client;
using GuiComposerHelpers = Vintagestory.API.Client.GuiComposerHelpers;

namespace DropdownNextPrev;

public static class AddDropdownPatch
{
    [HarmonyPatch(typeof(GuiComposerHelpers))]
    [HarmonyPatch("AddDropDown")]
    [HarmonyPatch(new[]
    {
        typeof(GuiComposer),
        typeof(string[]),
        typeof(string[]),
        typeof(int),
        typeof(SelectionChangedDelegate),
        typeof(ElementBounds),
        typeof(string)
    })]
    public static bool Prefix(
        ref GuiComposer __result,
        GuiComposer composer,
        string[] values,
        string[] names,
        int selectedIndex,
        SelectionChangedDelegate onSelectionChanged,
        ElementBounds bounds,
        string key = null)
    {
        if (composer == null || composer.Composed)
        {
            return true;
        }

        AddNavigationArrows(composer, values, names, selectedIndex, onSelectionChanged, bounds,
            CairoFont.WhiteSmallText(),
            key);

        __result = composer;
        return false;
    }

    [HarmonyPatch(typeof(GuiComposerHelpers))]
    [HarmonyPatch("AddDropDown")]
    [HarmonyPatch(new[]
    {
        typeof(GuiComposer),
        typeof(string[]),
        typeof(string[]),
        typeof(int),
        typeof(SelectionChangedDelegate),
        typeof(ElementBounds),
        typeof(CairoFont),
        typeof(string)
    })]
    public static bool Prefix2(
        ref GuiComposer __result,
        GuiComposer composer,
        string[] values,
        string[] names,
        int selectedIndex,
        SelectionChangedDelegate onSelectionChanged,
        ElementBounds bounds,
        CairoFont font,
        string key = null)
    {
        if (composer == null || composer.Composed)
        {
            return true;
        }

        AddNavigationArrows(composer, values, names, selectedIndex, onSelectionChanged, bounds, font, key);

        __result = composer;
        return false;
    }

    private static void AddNavigationArrows(GuiComposer composer, string[] values, string[] names, int selectedIndex,
        SelectionChangedDelegate onSelectionChanged, ElementBounds bounds, CairoFont font, string key)
    {
        var dropdown = new GuiElementDropdownWithNavigation(composer.Api, values, names, selectedIndex,
            onSelectionChanged,
            bounds, font);
        var leftArrowBounds = ElementBounds.Fixed(0 - 23, 0, 20, bounds.fixedHeight);
        var rightArrowBounds = ElementBounds.Fixed(bounds.fixedWidth + 5, 0, 20, bounds.fixedHeight);

        var buttonFont = CairoFont.WhiteMediumText().WithWeight(FontWeight.Bold)
            .WithOrientation(EnumTextOrientation.Center).WithFontSize(20);
        var buttonStyle = EnumButtonStyle.Normal;
        composer.AddInteractiveElement(
                dropdown, key).BeginChildElements()
            .AddButton("<", dropdown.OnLeftArrowClick, leftArrowBounds, buttonFont, buttonStyle)
            .AddButton(">", dropdown.OnRightArrowClick, rightArrowBounds, buttonFont, buttonStyle)
            .EndChildElements()
            ;
    }
}