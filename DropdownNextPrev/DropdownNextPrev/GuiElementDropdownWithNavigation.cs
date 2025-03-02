using Vintagestory.API.Client;

namespace DropdownNextPrev;

public class GuiElementDropdownWithNavigation : GuiElementDropDown
{
    public GuiElementDropdownWithNavigation(
        ICoreClientAPI capi,
        string[] values,
        string[] names,
        int selectedIndex,
        SelectionChangedDelegate onSelectionChanged,
        ElementBounds bounds,
        CairoFont font,
        bool multiSelect = false)
        : base(capi, values, names, selectedIndex, onSelectionChanged,
            bounds,
            font, multiSelect)
    {
    }

    public bool OnLeftArrowClick()
    {
        if (listMenu.SelectedIndex > 0)
        {
            SetSelectedIndex(listMenu.SelectedIndex - 1);
        }
        else
        {
            SetSelectedIndex(listMenu.Values.Length - 1);
        }

        onSelectionChanged?.Invoke(SelectedValue, true);

        return true;
    }

    public bool OnRightArrowClick()
    {
        if (listMenu.SelectedIndex < listMenu.Values.Length - 1)
        {
            SetSelectedIndex(listMenu.SelectedIndex + 1);
        }
        else
        {
            SetSelectedIndex(0);
        }

        onSelectionChanged?.Invoke(SelectedValue, true);

        return true;
    }
}