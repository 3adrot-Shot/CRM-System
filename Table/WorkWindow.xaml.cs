using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Table
{
    public partial class WorkWindow : Window
    {
        UserControl UC_Home = null;
        UserControl UC_Tasks = null;
        UserControl UC_Table = null;
        UserControl UC_Projects = null;
        UserControl UC_Messages = null;
        UserControl UC_Admin_Control = null;
        UserControl UC_Settings = null;

        public WorkWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window1_Loaded);
            this.SourceInitialized += new EventHandler(Window1_SourceInitialized);
            this.StateChanged += new EventHandler(Window_StateChanged);
            UC_Home = new UC.User_Home();
            UC_Tasks = new UC.User_Tasks();
            UC_Table = new UC.User_Table();
            UC_Projects = new UC.User_Projects();
            UC_Messages = new UC.User_Messages();
            UC_Admin_Control = new UC.Admin_Control();
            UC_Settings = new UC.User_Settings();
            GridMain.Children.Add(UC_Tasks);
            //Task.Run(()=>AsyncUpdate());
        }
        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            btnScale_Click(null, null);
        }

        void Window1_SourceInitialized(object sender, EventArgs e)
        {
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnScale_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                this.ResizeMode = ResizeMode.CanResizeWithGrip;
                WindowState = WindowState.Normal;
                btnScale.Content = "▢"; // Max
                CornerWindow.CornerRadius = new CornerRadius(1);
            }
            else
            {
                if (WindowState == WindowState.Normal)
                {
                    this.ResizeMode = ResizeMode.NoResize;
                    WindowState = WindowState.Maximized;
                    btnScale.Content = "▣"; // Min
                    CornerWindow.CornerRadius = new CornerRadius(0);
                }
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle);
                System.Drawing.Rectangle workingArea = screen.WorkingArea;
                this.MaxHeight = workingArea.Height;
            }
            else
            {
                this.MaxHeight = double.PositiveInfinity;
            }
        }
        private void LV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "LVI_Home":
                    usc = UC_Home;
                    GridMain.Children.Add(usc);
                    HideStackPanel.Begin();
                    break;
                case "LVI_Tasks":
                    usc = UC_Tasks;
                    GridMain.Children.Add(usc);
                    HideStackPanel.Begin();
                    break;
                case "LVI_Table":
                    usc = UC_Table;
                    GridMain.Children.Add(usc);
                    HideStackPanel.Begin();
                    break;
                case "LVI_Projects":
                    usc = UC_Projects;
                    GridMain.Children.Add(usc);
                    HideStackPanel.Begin();
                    break;
                case "LVI_Messages":
                    usc = UC_Messages;
                    GridMain.Children.Add(usc);
                    HideStackPanel.Begin();
                    break;
                case "LVI_Admin_Control":
                    usc = UC_Admin_Control;
                    GridMain.Children.Add(usc);
                    HideStackPanel.Begin();
                    break;
                case "LVI_Settings":
                    usc = UC_Settings;
                    GridMain.Children.Add(usc);
                    HideStackPanel.Begin();
                    break;
                case "LVI_SignOut":
                    Application.Current.Shutdown();
                    break;
                default:
                    break;
            }
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnScale_Click(null,null);
        }

        private void Tg_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (nav_pnl.Width == 46) 
            {
                ShowStackPanel.Begin();
            }
            if (nav_pnl.Width == 170)
            {
                HideStackPanel.Begin();
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UC_Tasks = new UC.User_Tasks();
            UC_Home = new UC.User_Home();
            UC_Projects = new UC.User_Projects();
            UC_Messages = new UC.User_Messages();
            UC_Admin_Control = new UC.Admin_Control();
            UC_Table = new UC.User_Table();
            UC_Settings = new UC.User_Settings();
            GridMain.Children.Clear();
            GridMain.Children.Add(UC_Tasks);
        }
        private void AsyncUpdate()
        {
            while (true)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    TextBlock_MouseDown(null, null);
                });
                Thread.Sleep(1000);
            }
        }
    }
}
