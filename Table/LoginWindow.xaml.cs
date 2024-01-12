using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Table.Code;

namespace Table
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        WorkWindow Work = new WorkWindow();
        public LoginWindow()
        {
            InitializeComponent();
            startAnimationsAsync();
            PostgreSQL.Connect();
            if (Properties.Settings.Default.SaveLogin != "" && Properties.Settings.Default.SavePassword != "")
            {
                txtLogin.Text = Properties.Settings.Default.SaveLogin;
                txtPass.Password = Properties.Settings.Default.SavePassword;
            }
        }

        public float SlideDurationSec { set; get; } = 0.9f;
        private async Task startAnimationsAsync()
        {
            //Wait for Page animation done
            await Task.Delay(TimeSpan.FromMilliseconds((int)SlideDurationSec * 1000));

            //Element Animations
            Animator.FadeIn(MainWindowLogin, 2);
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
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text != "" && txtPass.Password != "") 
            {
                string status = PostgreSQL.Auth("User", txtLogin.Text, txtPass.Password);
                if (status == "true")
                {
                    if (Properties.Settings.Default.user_active != "False")
                    {

                        TextBoxAuth.Text = "Авторизация успешна.";
                        if (checkBoxSave.IsChecked == true)
                        {
                            Properties.Settings.Default.SaveLogin = txtLogin.Text;
                            Properties.Settings.Default.SavePassword = txtPass.Password;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.Reset();
                        }
                        TextBoxAuth.Visibility = Visibility.Visible;
                        checkBoxSave.Visibility = Visibility.Hidden;
                        this.Hide();
                        Work.startAnimationsAsync();
                        Work.Show();
                    }
                    else
                    {
                        TextBoxAuth.Visibility = Visibility.Visible;
                        checkBoxSave.Visibility = Visibility.Hidden;
                        TextBoxAuth.Text = "Аккаунт деактивирован.";
                    }
                }
                else
                {
                    if (status != "error")
                    {
                        TextBoxAuth.Visibility = Visibility.Visible;
                        checkBoxSave.Visibility = Visibility.Hidden;
                        TextBoxAuth.Text = "Неверный логин или пароль.";
                    }
                    else
                    {
                        TextBoxAuth.Visibility = Visibility.Visible;
                        checkBoxSave.Visibility = Visibility.Hidden;
                        TextBoxAuth.Text = "Проблема с подключением.";
                    }
                }
            }
            else
            {
                    TextBoxAuth.Visibility = Visibility.Visible;
                    checkBoxSave.Visibility = Visibility.Hidden;
                    TextBoxAuth.Text = "Введите логин и пароль!";
            }
        }

        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            TextBoxAuth.Visibility = Visibility.Hidden;
            checkBoxSave.Visibility = Visibility.Visible;
            TextBoxAuth.Text = "";
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBoxAuth.Visibility = Visibility.Visible;
            checkBoxSave.Visibility = Visibility.Hidden;
            TextBoxAuth.Text = "Функция в разработке.";
        }
    }
}
