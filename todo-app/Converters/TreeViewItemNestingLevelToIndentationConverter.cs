namespace todo_app.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    internal class TreeViewItemNestingLevelToIndentationConverter : IValueConverter
    {
        private const double INDENT_SIZE = 19.0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not TreeViewItem item)
                return Binding.DoNothing;

            var level = GetNestingLevel(item);

            if (level == -1)
                return Binding.DoNothing;

            var parameterString = parameter.ToString();

            switch (parameterString)
            {
                case "NegativeLeft":
                    return new Thickness(-(level * INDENT_SIZE), 0, level * INDENT_SIZE, 0);
                case "NegativeRight":
                    return new Thickness(0, 0, -(level * INDENT_SIZE), 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameter));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private static ItemsControl? GetSelectedTreeViewItemParent(TreeViewItem item)
        {
            var parent = VisualTreeHelper.GetParent(item);
            while (parent is not (TreeViewItem or TreeView or null))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as ItemsControl;
        }

        private int GetNestingLevel(TreeViewItem item)
        {
            var nestingLevel = TreeViewItemExtensions.GetNestingLevel(item);

            if (nestingLevel != -1)
                return nestingLevel;

            //TODO: вынести всю логику с выставлением уровня вложенности в событие типа лоадед для айтема.
            var control = GetSelectedTreeViewItemParent(item);

            if (control is TreeView)
            {
                TreeViewItemExtensions.SetNestingLevel(item, 0);
                return 0;
            }

            if (control is not TreeViewItem parentItem)
            {
                TreeViewItemExtensions.SetNestingLevel(item, -1);
                return -1;
            }

            var parentNestingLevel = TreeViewItemExtensions.GetNestingLevel(parentItem);
            if (parentNestingLevel == 0)
            {
                TreeViewItemExtensions.SetNestingLevel(item, 1);
                return 1;
            }

            TreeViewItemExtensions.SetNestingLevel(item, parentNestingLevel + 1);
            return parentNestingLevel + 1;
        }
    }
}