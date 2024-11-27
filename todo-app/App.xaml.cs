namespace todo_app
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TreeViewItem), UIElement.PreviewMouseRightButtonDownEvent,
                new RoutedEventHandler(ForceSelectItem));

            base.OnStartup(e);
        }

        //private void RoutedEventHandler(object sender, RoutedEventArgs e)
        //{
        //    var item = (TreeViewItem)sender;
        //    var control = GetSelectedTreeViewItemParent(item);

        //    if (control is TreeView)
        //    {
        //        TreeViewItemExtensions.SetNestingLevel(item, 0);
        //        return;
        //    }

        //    if (control is not TreeViewItem parentItem)
        //        return;

        //    var parentNestingLevel = TreeViewItemExtensions.GetNestingLevel(parentItem);
        //    if (parentNestingLevel == 0)
        //    {
        //        TreeViewItemExtensions.SetNestingLevel(item, 1);
        //        return;
        //    }

        //    TreeViewItemExtensions.SetNestingLevel(item, parentNestingLevel + 1);
        //}

        private void ForceSelectItem(object sender, RoutedEventArgs e)
        {
            ((TreeViewItem)sender).IsSelected = true;
        }
    }
}