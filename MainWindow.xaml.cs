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
using System.IO;
using System.Resources;
using System.Drawing;
using System.Windows.Media.Animation;

namespace Quran100
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ********************* Images ******************************************************
        private int currentImage = -1;
        string[] imgs =  new string[100];
        string[] sounds = new string[100];
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 100; i++)
            {
                if (i <=8) imgs[i] = "00" + (i+1) + ".gif";
                else if (i >= 9 && i <= 98) imgs[i] = "0" + (i+1) + ".gif";
                else if (i ==99) imgs[i] = "100.gif";
            }
            for (int i = 0; i < 100; i++)
            {
                if (i <= 8) sounds[i] = "00" + (i + 1) + ".mp3";
                else if (i >= 9 && i <= 98) sounds[i] = "0" + (i + 1) + ".mp3";
                else if (i == 99) sounds[i] = "100.mp3";
            }
        }
        
        private void showImage(string img)
        {
            Image1.Source = new BitmapImage(new Uri(img, UriKind.Relative));
        }
        
        // **********************************************************************************


        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            if (imgs.Length > 0)
            {
                currentImage = currentImage == imgs.Length - 1 ? 0 : ++currentImage;
                showImage(imgs[currentImage]);
            }
            player.Stop();
            btn_Play.IsEnabled = true;
            btn_Stop.IsEnabled = false;
            btn_Previous.IsEnabled = true;
            btn_Resume.IsEnabled = false;
            btn_Pause.IsEnabled = false;
        }
        private void btn_Previous_Click_1(object sender, RoutedEventArgs e)
        {
            if (imgs.Length > 0)
            {
                currentImage = currentImage == 0 ? imgs.Length - 1 : --currentImage;
                showImage(imgs[currentImage]);
            }
            player.Stop();
            btn_Resume.IsEnabled = false;
            btn_Pause.IsEnabled = false;
            btn_Stop.IsEnabled = false;

        }
        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {
            playsound(currentImage);
            btn_Pause.IsEnabled = true;
            btn_Stop.IsEnabled = true;
            player.MediaEnded += new EventHandler(image2);
        }

        private void image2(object sender, EventArgs e)
        {
            currentImage = currentImage == imgs.Length - 1 ? 0 : ++currentImage;
            showImage(imgs[currentImage]);
            playsound(currentImage);
        }

        
        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            btn_Resume.IsEnabled = false;
            btn_Pause.IsEnabled = false;
        }
        private void btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
            btn_Resume.IsEnabled = true;
        }
        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Resume_Click(object sender, RoutedEventArgs e)
        {
            if (player.CanPause)
            {
                player.Play();
            }
        }

        // ************* Sound **************************************************************
        MediaPlayer player = new MediaPlayer();
        public void playsound(int path)
        {
                player.Open(new Uri(sounds[path], UriKind.Relative));
                player.Play();
        }

        
        // ********************************************************************************

        // ******************** GO TO *****************************************************
        private void txt_page_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Keyboard.IsKeyDown(Key.Enter))
                {
                    currentImage = int.Parse(txt_page.Text) - 1;
                    showImage(imgs[currentImage]); player.Stop();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter a number between 1 : 100", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error,MessageBoxResult.OK);
                currentImage = -1;
                btn_Play.IsEnabled = false;
                btn_Stop.IsEnabled = false;
                btn_Previous.IsEnabled = false;
                btn_Resume.IsEnabled = false;
                btn_Pause.IsEnabled = false;
            }
        }
        // ********************************************************************************


        // ************************ About *********************************************

        private void btn_about_Click(object sender, RoutedEventArgs e)
        {
            AboutBox1 a1 = new AboutBox1();
            a1.Show();
        }
        
        // ********************************************************************************


    }
}
