using Cryot2.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cryot2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class image : ContentPage
    {
        // For holding uploaded image
        private Image mysticImage;

        public image()
        {
            InitializeComponent();
            mysticImage = new Image();
        }

        async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                mysticImage.Source = ImageSource.FromStream(() => stream);
            }
            (sender as Button).IsEnabled = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                label.Source = mysticImage.Source;
            });
        }

    }
}