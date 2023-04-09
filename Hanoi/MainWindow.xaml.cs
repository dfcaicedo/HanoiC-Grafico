using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Hanoi
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stack<Border> torre1 = new Stack<Border>();
        Stack<Border> torre2 = new Stack<Border>();
        Stack<Border> torre3 = new Stack<Border>();
        List<PasosHanoi> pasos = new List<PasosHanoi>();
        int ndisc;
        DispatcherTimer th;
        public MainWindow()
        {
            InitializeComponent();
            th = new DispatcherTimer();
            th.Tick += Th_Tick;

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lista = (ListBox)sender;
            int ndiscos = Convert.ToInt32(((ListBoxItem)lista.SelectedItem).Content.ToString());
            this.ndisc = ndiscos;
      
            th.Interval = TimeSpan.FromMilliseconds(1000 / ndisc);
            //MessageBox.Show(ndiscos.ToString());
            torre1.Clear();
            torre2.Clear();
            torre3.Clear();
            this.Pila1.Children.Clear();
            this.Pila2.Children.Clear();
            this.Pila3.Children.Clear();
            for (int a=ndiscos;a>=1;a--)
            {
                float porce = (float)((a * (this.Width / 3)) / ndiscos);
                int dife = (int)(((this.Width / 3) - porce)/2);
                Border newDisco = new Border();                
                newDisco.Height = (this.Height - 125) / ndiscos;
                newDisco.CornerRadius = new CornerRadius(25);
                switch (a)
                {
                    case 1: newDisco.Background=Brushes.Lavender; break;
                    case 2: newDisco.Background = Brushes.YellowGreen; break;
                    case 3: newDisco.Background = Brushes.SpringGreen; break;
                    case 4: newDisco.Background = Brushes.LightSalmon; break;
                    case 5: newDisco.Background = Brushes.MediumSlateBlue ; break;
                    case 6: newDisco.Background = Brushes.Chocolate; break;
                    case 7: newDisco.Background = Brushes.Gray; break;
                    case 8: newDisco.Background = Brushes.DarkOliveGreen ; break;
                    case 9: newDisco.Background = Brushes.BlueViolet ; break;
                    case 10: newDisco.Background = Brushes.DarkTurquoise ; break;
                    case 11: newDisco.Background = Brushes.LightSteelBlue ; break;
                    case 12: newDisco.Background = Brushes.Tomato ; break;                    
                }
                newDisco.BorderBrush = Brushes.Black;
                newDisco.BorderThickness = new Thickness(1);
                newDisco.Margin = new Thickness(dife+5, 1, dife+5, 1);
                TextBlock texto = new TextBlock();
                texto.Text = a.ToString();
                texto.TextAlignment= TextAlignment.Center;
                texto.VerticalAlignment= VerticalAlignment.Center;

                newDisco.Child = texto;
                torre1.Push(newDisco);

            }
            Pila1.Children.Clear();
            Pila2.Children.Clear();
            Pila3.Children.Clear();
            foreach (Border disco in torre1)
            {
                Pila1.Children.Add(disco);
            }
            foreach (Border disco in torre2)
            {
                Pila2.Children.Add(disco);
            }
            foreach (Border disco in torre3)
            {
                Pila3.Children.Add(disco);
            }
        }
        
        

        private void Th_Tick(object sender, EventArgs e)
        {            
            PasosHanoi paso = pasos[0];
                paso.des.Push(paso.ori.Pop());
                this.Dispatcher.Invoke(
                () =>
                {
                    Pila1.Children.Clear();
                    Pila2.Children.Clear();
                    Pila3.Children.Clear();
                    foreach (Border disco in torre1)
                    {
                        Pila1.Children.Add(disco);
                    }
                    foreach (Border disco in torre2)
                    {
                        Pila2.Children.Add(disco);
                    }
                    foreach (Border disco in torre3)
                    {
                        Pila3.Children.Add(disco);
                    }
                }
                );
            pasos.RemoveAt(0);
            if (pasos.Count == 0)
                th.Stop();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ListaD.SelectedIndex = 0;
        }
        public void hanoi()
        {     
            this.pasos.Clear();
            hanoi(ndisc, torre1, torre3, torre2);
            th.Start();
        }
        public void hanoi(int disco,Stack<Border> ori, Stack<Border> des, Stack<Border> aux)
        {
        
            if (disco == 1)
            {
                pasos.Add(new PasosHanoi { ori = ori,des=des });                
            }
            else
            {
                hanoi(disco - 1, ori, aux, des);
                pasos.Add(new PasosHanoi { ori = ori, des = des });                                
                hanoi(disco - 1, aux, des, ori);                                
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            hanoi();
        }
    }
}
