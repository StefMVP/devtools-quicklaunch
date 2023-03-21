using System;
using System.Windows;
using System.Windows.Controls;

namespace AppQuickLaunch
{
    public static class UiUtils
    {
        public static Grid GetMainGrid()
        {
            try
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        return (window as MainWindow).MainGrid;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static TabControl GetMainTabControl()
        {
            try
            {
                var mainGrid = GetMainGrid();
                foreach (object child in mainGrid?.Children)
                {
                    if (child is TabControl)
                    {
                        return child as TabControl;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static TabItem GetTabItemByTabHeader(string tabHeaderName)
        {
            try
            {
                var tabControl = GetMainTabControl();
                foreach (object item in tabControl?.Items)
                {
                    if (item is TabItem)
                    {
                        var tabItem = item as TabItem;
                        if (tabItem.Header.ToString() == tabHeaderName)
                        {
                            return tabItem;
                        }
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static ScrollViewer GetScrollViewerByTabHeader(string tabHeaderName)
        {
            try
            {
                var tabItem = GetTabItemByTabHeader(tabHeaderName);
                if (tabItem?.Content is ScrollViewer)
                {
                    var scrollViewer = tabItem.Content as ScrollViewer;
                    return scrollViewer;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static Grid GetTabGridByTabHeader(string tabHeaderName)
        {
            try
            {
                var scrollViewer = GetScrollViewerByTabHeader(tabHeaderName);
                if (scrollViewer?.Content is Grid)
                {
                    return scrollViewer.Content as Grid;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static TextBox GetTextBoxByName(string tabHeaderName, string textBoxName)
        {
            try
            {
                var tabGrid = GetTabGridByTabHeader(tabHeaderName);
                foreach (object child in tabGrid?.Children)
                {
                    if (child is TextBox)
                    {
                        var textBoxItem = child as TextBox;
                        if (textBoxItem.Name.ToString() == textBoxName)
                        {
                            return textBoxItem;
                        }
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static Label GetLabelByName(string tabHeaderName, string labelName)
        {
            try
            {
                var tabGrid = GetTabGridByTabHeader(tabHeaderName);
                foreach (object child in tabGrid?.Children)
                {
                    if (child is Label)
                    {
                        var labelItem = child as Label;
                        if (labelItem.Name.ToString() == labelName)
                        {
                            return labelItem;
                        }
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static Window GetMainWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    return window;
                }
            }
            return null;
        }
    }
}