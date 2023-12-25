using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Table.UC
{
    /// <summary>
    /// Логика взаимодействия для User_Table.xaml
    /// </summary>
    public partial class User_Table : UserControl
    {
        public class TaskItem
        {
            public string SourceImageTask { get; set; }
            public string NameTask { get; set; }
            public UIElement DynamicTasks { get; set; }
        }
        public User_Table()
        {
            InitializeComponent();
            CreateTasks();
            AddTestColumns();
            MainGrid.PreviewMouseWheel += MainGrid_PreviewMouseWheel;
            return;
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
            for (int i = 0; i < 9; i++)
            {
                if (i == 0)
                {
                    border = new Border
                    {
                        BorderBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#EFEFEF")),
                        BorderThickness = new Thickness(0, 0, 1, 1),
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        MinWidth = 200
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
                        MinWidth = 106
                    };
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
        private void CreateTasks()
        {
            Grid grid = new Grid();
            StackPanel MainStackPanel = new StackPanel(); // 0
            MainStackPanel.Orientation = Orientation.Horizontal;
            MainStackPanel.Children.Add(CreateBorder("Не требуется", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel.Children.Add(CreateBorder("Принято", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel.Children.Add(CreateBorder("Готово", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel.Children.Add(CreateBorder("Делать", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel.Children.Add(CreateBorder("Выкинуто", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel.Children.Add(CreateBorder("Пока стоп", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel.Children.Add(CreateBorder("В работе", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel.Children.Add(CreateBorder("Исправить", "Карачурин А.Ф.", "Карачурин А.Ф."));
            StackPanel MainStackPanel2 = new StackPanel(); // 1
            MainStackPanel2.Orientation = Orientation.Horizontal;
            MainStackPanel2.Children.Add(CreateBorder("Не требуется", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel2.Children.Add(CreateBorder("Принято", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel2.Children.Add(CreateBorder("Готово", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel2.Children.Add(CreateBorder("Делать", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel2.Children.Add(CreateBorder("Выкинуто", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel2.Children.Add(CreateBorder("Пока стоп", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel2.Children.Add(CreateBorder("В работе", "Карачурин А.Ф.", "Карачурин А.Ф."));
            MainStackPanel2.Children.Add(CreateBorder("Исправить", "Карачурин А.Ф.", "Карачурин А.Ф."));
            List<TaskItem> taskItems = new List<TaskItem>
            {
                new TaskItem { SourceImageTask = "/images/myxa_vector.png", NameTask = "Задача 1", DynamicTasks = MainStackPanel },
                new TaskItem { SourceImageTask = "/images/myxa_vector.png", NameTask = "Задача 2", DynamicTasks = MainStackPanel2 },
            };
            LV_Tasks.ItemsSource = taskItems;
        }

        private Border CreateBorder(string state, string ispolnitel, string otvetstvenny)
        {
            TextBlock text1 = null;
            TextBlock text2 = null;
            TextBlock text3 = null;
            StackPanel stackPanel = new StackPanel();
            Border border = new Border();
            switch (state)
            {
                case "Не требуется":
                    text1 = CreateTextBlock(14, "#2E5300", state, 5);
                    text2 = CreateTextBlock(14, "#2E5300", ispolnitel, 5, -3);
                    text3 = CreateTextBlock(14, "#2E5300", otvetstvenny, 5, -3);
                    border.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFBAF371"));
                    stackPanel.Children.Add(text1);
                    break;
                case "Принято":
                    text1 = CreateTextBlock(14, "#2E5300", state, 5);
                    text2 = CreateTextBlock(14, "#2E5300", ispolnitel, 5, -3);
                    text3 = CreateTextBlock(14, "#2E5300", otvetstvenny, 5, -3);
                    border.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFBAF371"));
                    stackPanel.Children.Add(text1);
                    break;
                case "Готово":
                    text1 = CreateTextBlock(14, "#FFFFFF", state, 5);
                    text2 = CreateTextBlock(14, "#FFFFFF", ispolnitel, 5, -3);
                    text3 = CreateTextBlock(14, "#FFFFFF", otvetstvenny, 5, -3);
                    border.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#56B600"));
                    stackPanel.Children.Add(text1);
                    break;
                case "Делать":
                    text1 = CreateTextBlock(14, "#FFFFFF", state, 5); // White
                    text2 = CreateTextBlock(14, "#FFFFFF", ispolnitel, 5, -3);
                    text3 = CreateTextBlock(14, "#FFFFFF", otvetstvenny, 5, -3);
                    border.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#00AFEC"));
                    stackPanel.Children.Add(text1);
                    break;
                case "Выкинуто":
                    text1 = CreateTextBlock(14, "#FF8B0000", state, 5); // DarkRed
                    text2 = CreateTextBlock(14, "#FF8B0000", ispolnitel, 5, -3);
                    text3 = CreateTextBlock(14, "#FF8B0000", otvetstvenny, 5, -3);
                    border.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFD3C6"));
                    stackPanel.Children.Add(text1);
                    break;
                case "Пока стоп":
                    text1 = CreateTextBlock(14, "#FFFFFF", state, 5); // White
                    text2 = CreateTextBlock(14, "#FFFFFF", ispolnitel, 5, -3);
                    text3 = CreateTextBlock(14, "#FFFFFF", otvetstvenny, 5, -3);
                    border.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFABABAB"));
                    stackPanel.Children.Add(text1);
                    break;
                case "В работе":
                    text1 = CreateTextBlock(14, "#FFFFFF", state, 5); // White
                    text2 = CreateTextBlock(14, "#FFFFFF", ispolnitel, 5, -3);
                    text3 = CreateTextBlock(14, "#FFFFFF", otvetstvenny, 5, -3);
                    border.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#E7A119"));
                    stackPanel.Children.Add(text1);
                    break;
                case "Исправить":
                    text1 = CreateTextBlock(14, "#FF8B0000", state, 5); // White
                    text2 = CreateTextBlock(14, "#FF8B0000", ispolnitel, 5, -3);
                    text3 = CreateTextBlock(14, "#FF8B0000", otvetstvenny, 5, -3);
                    border.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFA388"));
                    stackPanel.Children.Add(text1);
                    break;
                default:
                    text2 = CreateTextBlock(14, "#F4F4F4", ispolnitel, 5, 10);
                    text3 = CreateTextBlock(14, "#F4F4F4", otvetstvenny, 5, -3);
                    border.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFABABAB"));
                    break;
            }
            stackPanel.Orientation = Orientation.Vertical;
            stackPanel.Children.Add(text2);
            stackPanel.Children.Add(text3);
            border.CornerRadius = new CornerRadius(6);
            border.Margin = new Thickness(5, 5, -3, 5);
            border.MinWidth = 95;
            border.MinHeight = 40;
            border.Child = stackPanel;
            return border;
        }
        private TextBlock CreateTextBlock(int FontSize = 14, string Foreground = "", string Text = "", int x = 0, int y = 0, int z = 0, int w = 0)
        {
            TextBlock text = new TextBlock();
            text.Margin = new Thickness(x, y, z, w);
            text.FontSize = FontSize;
            text.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(Foreground));
            text.Text = Text;
            return text;
        }
    }
}
