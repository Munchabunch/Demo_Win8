using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo_Win8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            oFlipView_01.SelectedIndex = 0;
            ChangeImgSource(img_01, "i/field.jpeg");
        }

        #region Event Procedures

        private void oFlipView_01_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (oFlipView_01 != null)
            {
                switch (oFlipView_01.SelectedIndex)
                {
                    case 0:
                        rb_Intro.IsChecked = true;
                        break;
                    case 1:
                        rb_Pictures.IsChecked = true;
                        break;
                    case 2:
                        rb_StackPanel.IsChecked = true;
                        break;
                    case 3:
                        rb_Grid.IsChecked = true;
                        break;
                    case 4:
                        rb_Canvas.IsChecked = true;
                        break;
                }
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            var oRadioButton_Selected = sender as RadioButton;

            if (oRadioButton_Selected != null)
            {
                int index = Convert.ToInt32(oRadioButton_Selected.Tag);
                oFlipView_01.SelectedIndex = index;
            }
        }

        #region Pictures tab

        private void btn_ImgA_Click(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(img_01, "i/cat.jpeg");
        }
        private void btn_ImgB_Click(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(img_01, "i/field.jpeg");
        }
        private void btn_ImgC_Click(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(img_01, "i/palmettos.jpeg");
        }
        private async void btn_01_Click(object sender, RoutedEventArgs e)
        {
            // WORKING //

            FileOpenPicker fileOpenPicker = new FileOpenPicker();

            // Filter to include a sample subset of file types
            fileOpenPicker.FileTypeFilter.Add(".wmv");
            fileOpenPicker.FileTypeFilter.Add(".mp4");
            fileOpenPicker.FileTypeFilter.Add(".mp3");
            fileOpenPicker.FileTypeFilter.Add(".wma");
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            // Prompt user to select a file            
            StorageFile file = await fileOpenPicker.PickSingleFileAsync();

            // Ensure a file was selected
            if (file != null)
            {
                // Open the selected file and set it as the MediaElement's source
                IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
                oMediaElement_01.SetSource(stream, file.ContentType);
            }
        }
        private void btn_02_Click(object sender, RoutedEventArgs e)
        {
            oMediaElement_01.Source = new Uri(@".\video\ham.mp4", UriKind.Relative);
            oMediaElement_01.Play();
        }
        private void btn_03_Click(object sender, RoutedEventArgs e)
        {
            oMediaElement_01.Source = new Uri(@".\video\parachute.avi", UriKind.Relative);
            oMediaElement_01.Play();
        }

        private void btn_Pause_Click(object sender, RoutedEventArgs e)
        {
            oMediaElement_01.Pause();
        }

        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {
            oMediaElement_01.Play();
        }

        #endregion Pictures tab

        #endregion Event Procedures

        #region Private Methods

        /// <summary>
        /// Change the source (picture) of an Image control.
        /// </summary>
        /// <param name="img_ToChange">image control</param>
        /// <param name="PathedFileName_NewImg">absolute or relative path and file name of the new image</param>
        private void ChangeImgSource(Image img_ToChange, string PathedFileName_NewImg)
        {
            img_ToChange.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri("ms-appx:///" + PathedFileName_NewImg, UriKind.Absolute));
        }

        private string NormalizePath(string Path_Orig)
        {
            if (Path_Orig.EndsWith("\\"))
            {
                return Path_Orig;
            }
            else
            {
                return Path_Orig + "\\";
            }
        }

        private string GetParentPath(string Path_Orig)
        {
            string NPath_Orig = NormalizePath(Path_Orig);

            if (NPath_Orig.Length < 3)
            {
                return NPath_Orig;
            }
            else
            {
                Int32 i_Pos = NPath_Orig.LastIndexOf("\\", NPath_Orig.Length - 2);
                return NPath_Orig.Substring(0, i_Pos) + "\\";
            }
        }

        /// <summary>
        /// Determine whether a string represents a valid decimal value.
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        private static bool IsTextNumeric(string Text)
        {
            // ToDo: Make sure there is only one decimal point and only one minus sign.

            bool b_OK = true;

            if (Text.Length > 0)
            {
                // Test the first character:
                string c = Text[0].ToString();
                if (!"-0123456789.".Contains(c)) b_OK = false;

                for (int Pos = 1; Pos < Text.Length; Pos++)
                {
                    c = Text[Pos].ToString();
                    if (!"0123456789.".Contains(c)) b_OK = false;
                }

                if (b_OK)
                {
                    // Count the decimal points in the text.
                    int count = Text.Split('.').Length - 1;
                    if (count > 1) b_OK = false;
                }
            }
            return b_OK;
        }

        #endregion Private Methods
    }
}
