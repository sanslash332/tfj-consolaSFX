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
using System.Windows.Shapes;
using Microsoft.Win32;
using DavyKager;
using logSystem;

namespace consolaSFX
{
    /// <summary>
    /// Interaction logic for sfxAddWindow.xaml
    /// </summary>
    public partial class sfxAddWindow : Window
    {
        private Sfx currentSound;
        private int sfxInt;
        private bool isEditing=false;
        private System.Windows.Forms.Keys sfxkey;


        public sfxAddWindow(int sfxIndex)
        {
            InitializeComponent();
            App.waitingKeyEvent += App_waitingKeyEvent;
            sfxInt = sfxIndex;
            isEditing = true;

            currentSound = App.sfxList[sfxIndex];
            sfxPathTextBox.Text = currentSound.sfxPath;
            this.controlCheckBox.IsChecked = currentSound.inclControl;
            this.shiftCheckBox.IsChecked = currentSound.inclShift;
            this.altCheckBox.IsChecked = currentSound.inclAlt;
            this.sfxkey = currentSound.sfxKey;

            if(currentSound.sfxKey!=null)
            {
                this.keyButton.Content = string.Format("Tecla rápida: {0} presione para editar.", currentSound.sfxKey.ToString());

            }
        }

        public sfxAddWindow()
        {
            InitializeComponent();
            App.waitingKeyEvent += App_waitingKeyEvent;
        }
        
        private void updateKeyButtonText()
        {
            if (sfxkey == System.Windows.Forms.Keys.Escape)
            {

                this.keyButton.Content = "tecla rápida: ninguna. Presione para editar";


            }
            else
            {
                this.keyButton.Content = string.Format("Tecla rápida: {0} presione para editar.", sfxkey.ToString());
            }


        }

        private void App_waitingKeyEvent(System.Windows.Forms.Keys obj)
        {
            Tolk.Speak("tecla asignada ", true);

            sfxkey = obj;
            try
            {
                keyButton.Dispatcher.Invoke(new Action(updateKeyButtonText));
            }
            catch(Exception e)
            {
                LogWriter.escribir("Error capturando tecla: "+ e.Message + ": " + e.StackTrace.ToString());

                
            }
            
        }

        
        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "seleccione efecto a cargar";
            op.Filter = "archivos de audio|*.mp3;*.wav;*.ogg;*.flac";
            if(op.ShowDialog()==true)
            {
                sfxPathTextBox.Text = op.FileName;

            }
            
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            currentSound = new Sfx(sfxPathTextBox.Text,sfxkey,(bool)controlCheckBox.IsChecked,(bool)shiftCheckBox.IsChecked,(bool)altCheckBox.IsChecked);
if(isEditing)
            {
                App.sfxList[sfxInt] = currentSound;
            }
            else
            {
                App.sfxList.Add(currentSound);
            }

            this.Close();

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void keyButton_Click(object sender, RoutedEventArgs e)
        {
            App.capturingNextKey = true;
            Tolk.Speak("presione tecla para asignar al sonido. presione escape para no asignar nada ", true);


        }
    }
}
