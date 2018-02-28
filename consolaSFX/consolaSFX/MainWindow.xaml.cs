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
using IrrKlang;
using DavyKager;

namespace consolaSFX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IrrKlang.ISoundDeviceList deviceList;

        public MainWindow()
        {
            InitializeComponent();
            Tolk.Load();

            sfxListbox.ItemsSource = App.sfxList;
             deviceList = new ISoundDeviceList(SoundDeviceListType.PlaybackDevice);
            for(int i = 0; i< deviceList.DeviceCount;i++)
            {
                comboDevices.Items.Add(deviceList.getDeviceDescription(i));
                comboDevices.Items.Refresh();
            }
            App.engine.SoundVolume = Properties.Settings.Default.volumen;
            
            
            volumeSlider.Value = App.engine.SoundVolume;
            
            
            

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Tolk.Unload();
            App.Current.Shutdown();

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            sfxAddWindow addWin = new sfxAddWindow();
            addWin.Owner = this;
            addWin.ShowDialog();
            sfxListbox.Items.Refresh();


            sfxListbox.UpdateLayout();

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if(sfxListbox.SelectedIndex>=0)
            {
                sfxAddWindow addWin = new sfxAddWindow(sfxListbox.SelectedIndex);
                addWin.Owner = this;
                addWin.ShowDialog();


            }
            sfxListbox.Items.Refresh();
            sfxListbox.UpdateLayout();

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(sfxListbox.SelectedIndex>=0 && sfxListbox.SelectedIndex<App.sfxList.Count())
            {
                App.sfxList.RemoveAt(sfxListbox.SelectedIndex);
                sfxListbox.Items.Refresh();
            }
        }

        private void sfxListbox_KeyDown(object sender, KeyEventArgs e)
        {
if(e.Key== Key.Space||e.Key== Key.Enter)
            {
                Tolk.Speak("click con indice: "+sfxListbox.SelectedIndex.ToString(), true);

                if(sfxListbox.SelectedIndex>=0)
                {
                    App.engine.Play2D(App.sfxList[sfxListbox.SelectedIndex].sfxPath);

                }
            }
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            App.engine.SoundVolume = (float)e.NewValue;
            App.engine.Play2D("b.wav");
            Properties.Settings.Default.volumen = (float)e.NewValue;
            Properties.Settings.Default.Save();



        }

        private void sfxListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tolk.Speak(App.sfxList[sfxListbox.SelectedIndex].listString,true);
        }

        private void comboDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.engine = new ISoundEngine(SoundOutputDriver.AutoDetect, SoundEngineOptionFlag.DefaultOptions, deviceList.getDeviceID(comboDevices.SelectedInde  x));
        }
    }
}
