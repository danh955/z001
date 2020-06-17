using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DDD1.UI.Uwp2004
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private ObservableCollection<NavLink> _navLinks = new ObservableCollection<NavLink>()
        {
            new NavLink() { Label = "People", Symbol = Windows.UI.Xaml.Controls.Symbol.People  },
            new NavLink() { Label = "Globe", Symbol = Windows.UI.Xaml.Controls.Symbol.Globe },
            new NavLink() { Label = "Message", Symbol = Windows.UI.Xaml.Controls.Symbol.Message },
            new NavLink() { Label = "Mail", Symbol = Windows.UI.Xaml.Controls.Symbol.Mail },
        };

        public ObservableCollection<NavLink> NavLinks
        {
            get { return _navLinks; }
        }

        public void SplitViewPage()
        {
            this.InitializeComponent();
        }

        private void togglePaneButton_Click(object sender, RoutedEventArgs e)
        {
            if (Window.Current.Bounds.Width >= 640)
            {
                if (splitView.IsPaneOpen)
                {
                    splitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                    splitView.IsPaneOpen = false;
                }
                else
                {
                    splitView.IsPaneOpen = true;
                    splitView.DisplayMode = SplitViewDisplayMode.Inline;
                }
            }
            else
            {
                splitView.IsPaneOpen = !splitView.IsPaneOpen;
            }
        }

        private void PanePlacement_Toggled(object sender, RoutedEventArgs e)
        {
            var ts = sender as ToggleSwitch;
            if (ts.IsOn)
            {
                splitView.PanePlacement = SplitViewPanePlacement.Right;
            }
            else
            {
                splitView.PanePlacement = SplitViewPanePlacement.Left;
            }
        }

        private void NavLinksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is NavLink link)
            {
                if (link.Symbol == Symbol.GlobalNavigationButton)
                {
                    splitView.IsPaneOpen = !splitView.IsPaneOpen;
                }
                else
                {
                    content.Text = link.Label + " Page";
                }
            }
        }

        private void displayModeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            splitView.DisplayMode = (SplitViewDisplayMode)Enum.Parse(typeof(SplitViewDisplayMode), (e.AddedItems[0] as ComboBoxItem).Content.ToString());
        }

        private void paneBackgroundCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var colorString = (e.AddedItems[0] as ComboBoxItem).Content.ToString();

            VisualStateManager.GoToState(this, colorString, false);
        }

        private void GlobalNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private void ToBlankPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1));
        }
    }

    public class NavLink
    {
        public string Label { get; set; }
        public Symbol Symbol { get; set; }
    }
}