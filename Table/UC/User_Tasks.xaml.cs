using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Table.UC
{
    /// <summary>
    /// Логика взаимодействия для User_Tasks.xaml
    /// </summary>
    public partial class User_Tasks : UserControl
    {
        public User_Tasks()
        {
            InitializeComponent();
            AddTestColumns();
            MainGrid.PreviewMouseWheel += MainGrid_PreviewMouseWheel;
        }
        private void MainGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                int delta = e.Delta;
                double scaleFactor = (delta > 0) ? 1.1 : 0.9;
                ScaleTransform scaleTransform = new ScaleTransform(MainGrid.LayoutTransform.Value.M11 * scaleFactor, MainGrid.LayoutTransform.Value.M22 * scaleFactor);
                MainGrid.LayoutTransform = scaleTransform;
                e.Handled = true;
            }
        }
        private void AddTestColumns()
        {
            // Создание StackPanel
            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Height = 30,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            // Добавление столбцов
            Border border = new Border();
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    border = new Border
                    {
                        BorderBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#EFEFEF")),
                        BorderThickness = new Thickness(0, 0, 1, 1),
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        MinWidth = 81
                    };
                }
                else
                {
                    if (i == 1)
                    {
                        border = new Border
                        {
                            BorderBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#EFEFEF")),
                            BorderThickness = new Thickness(0, 0, 1, 1),
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            MinWidth = 210
                        };
                    }
                    else
                    {
                        border = new Border
                        {
                            BorderBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#EFEFEF")),
                            BorderThickness = new Thickness(0, 0, 1, 1),
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            MinWidth = 145
                        };
                    }
                }
                TextBlock textBlock = new TextBlock
                {
                    Text = " Test #" + i,
                    FontSize = 16,
                    Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#EFEFEF"))
                };

                // Добавление TextBlock в Border
                border.Child = textBlock;

                // Добавление Border в StackPanel
                stackPanel.Children.Add(border);
            }

            // Добавление StackPanel в MainGrid
            MainGrid.Children.Add(stackPanel);
        }
    }
}