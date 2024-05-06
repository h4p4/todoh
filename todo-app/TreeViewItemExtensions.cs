namespace todo_app
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public static class TreeViewItemExtensions
    {
        public static readonly DependencyProperty NestingLevelProperty = DependencyProperty.RegisterAttached(
            "NestingLevel", typeof(int), typeof(TreeViewItem), new PropertyMetadata(-1));

        public static int GetNestingLevel(DependencyObject element)
        {
            return (int)element.GetValue(NestingLevelProperty);
        }

        public static void SetNestingLevel(DependencyObject element, int value)
        {
            element.SetValue(NestingLevelProperty, value);
        }
    }
}