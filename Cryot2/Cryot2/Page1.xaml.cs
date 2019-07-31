using Cryot2.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cryot2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        // Button Lists for health, power, and speed
        private List<Button> powerBar;
        private List<Button> healthBar;
        private List<Button> speedBar;

        // Hold the current values of each power bar or button
        private int inputPower;
        private int inputHealth;
        private int inputSpeed;

        // Hold current value of buffs
        private int count_jewels;
        private int count_wands;
        private int count_potions;

        // For holding uploaded image
        private Image mysticImage;

        public Page1()
        {
            InitializeComponent();

            // Initialize button lists and current values
            powerBar = new List<Button>();
            healthBar = new List<Button>();
            speedBar = new List<Button>();
            inputPower = 0;
            inputHealth = 0;
            inputSpeed = 0;
            mysticImage = new Image();

            // Populate button lists
            populateButtons();
        }

        // Method adds buttons to individual button lists
        private void populateButtons()
        {
            // Power Level Buttons
            powerBar.Add(pb1);
            powerBar.Add(pb2);
            powerBar.Add(pb3);
            powerBar.Add(pb4);
            powerBar.Add(pb5);
            powerBar.Add(pb6);

            // Health Level Buttons
            healthBar.Add(hb1);
            healthBar.Add(hb2);
            healthBar.Add(hb3);
            healthBar.Add(hb4);
            healthBar.Add(hb5);
            healthBar.Add(hb6);

            // Speed Level Buttons
            speedBar.Add(sb1);
            speedBar.Add(sb2);
            speedBar.Add(sb3);
            speedBar.Add(sb4);
        }

        // Listens to any button click from a power bar, changes views as needed
        private void bar_button_clicked(Object sender, EventArgs e)
        {
            // Gather data and flags from binding context
            string data = ((Button)sender).BindingContext as string;
            List<Button> inputs = new List<Button>();
            char type = data[0];
            string indexString = data.Substring(1);
            int index = Convert.ToInt32(indexString);

            // Select proper list of buttons to change
            if (type == 'p')
            {
                inputs = powerBar;
                inputPower = index + 1;
                Device.BeginInvokeOnMainThread(() =>
                {
                    note_power.Text = (index + 1).ToString();
                });
            }
            else if (type == 'h')
            {
                inputs = healthBar;
                inputHealth = index + 1;
                Device.BeginInvokeOnMainThread(() =>
                {
                    note_health.Text = (index + 1).ToString();
                });
            }
            else // type == s
            {
                inputs = speedBar;
                inputSpeed = index + 1;
                Device.BeginInvokeOnMainThread(() =>
                {
                    note_speed.Text = (index + 1).ToString();
                });
            }

            // Changes views based on current button click
            Device.BeginInvokeOnMainThread(() =>
            {
                int i = 0;
                while (i < inputs.Count())
                {
                    if (i <= index)
                    {
                        inputs[i].BackgroundColor = System.Drawing.Color.FromArgb(192,255,0,0);
                        inputs[i].Text = i == index ? (i + 1).ToString() : "";
                        //inputs[i].Margin = i < 6 ? new Thickness(0, 0, 1, 0) : inputs[i].Margin;
                    }
                    else
                    {
                        inputs[i].BackgroundColor = System.Drawing.Color.FromArgb(192, 0, 0, 0); ;
                        inputs[i].Text = "";
                        //inputs[i].Margin = i < 6 ? new Thickness(0, 0, 1, 0) : inputs[i].Margin;
                    }
                    i++;
                }
            });

        }

        // Listens to any button click from a power bar, changes views as needed
        private void increment_button_clicked(Object sender, EventArgs e)
        {
            // Gather data and flags from binding context
            string data = ((ImageButton)sender).BindingContext as string;
            char type = data[0];
            string actionString = data.Substring(1);
            int action = Convert.ToInt32(actionString);

            // Select proper label and value to change based on binding
            if (type == 'j')
            {
                if (action == 1)
                {
                    count_jewels++;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        note_jewels.Text = count_jewels.ToString();
                    });
                }
                else
                {
                    count_jewels--;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        note_jewels.Text = count_jewels.ToString();
                    });
                }
            }
            else if (type == 'w')
            {
                if (action == 1)
                {
                    count_wands++;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        note_wands.Text = count_wands.ToString();
                    });
                }
                else
                {
                    count_wands--;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        note_wands.Text = count_wands.ToString();
                    });
                }
            }
            else // type == p
            {
                if (action == 1)
                {
                    count_potions++;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        note_potions.Text = count_potions.ToString();
                    });
                }
                else
                {
                    count_potions--;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        note_potions.Text = count_potions.ToString();
                    });
                }
            }
        }

        // Change color of arrow button to red when clicked
        private void change_arrow_red(Object sender, EventArgs e)
        {
            string context = ((ImageButton)sender).BindingContext as string;
            string actionString = context.Substring(1);
            int action = Convert.ToInt32(actionString);
            // Gather data and flags from binding context
            Device.BeginInvokeOnMainThread(() =>
            {
                ((ImageButton)sender).Source = action == 1 ? "up_arrow_red.png" : "down_arrow_red.png";
            });
        }

        // Change color of arrow button to black when clicked
        private void change_arrow_black(Object sender, EventArgs e)
        {
            string context = ((ImageButton)sender).BindingContext as string;
            string actionString = context.Substring(1);
            int action = Convert.ToInt32(actionString);
            // Gather data and flags from binding context
            Device.BeginInvokeOnMainThread(() =>
            {
                ((ImageButton)sender).Source = action == 1 ? "up_arrow.png" : "down_arrow.png";
            });
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
                MysticBackgroundImage.Source = mysticImage.Source;
            });
        }

    }
}