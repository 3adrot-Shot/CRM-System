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
    /// Логика взаимодействия для User_Messages.xaml
    /// </summary>
    public partial class User_Messages : UserControl
    {
        public User_Messages()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_Search_watermark != null) {
                if (string.IsNullOrEmpty(TB_Search.Text))
                    TB_Search_watermark.Visibility = Visibility.Visible;
                else
                    TB_Search_watermark.Visibility = Visibility.Hidden;
            }
        }
        private void TextBoxMes_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_Message_watermark != null)
            {
                if (string.IsNullOrEmpty(TB_Message.Text))
                    TB_Message_watermark.Visibility = Visibility.Visible;
                else
                    TB_Message_watermark.Visibility = Visibility.Hidden;
            }
        }
        private void TB_Message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Обработка текста сообщения
            }
        }
        private void TB_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Обработка текст бокса поиска
            }
        }
    }
}
