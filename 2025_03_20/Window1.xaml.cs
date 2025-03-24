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

namespace _2025_03_20
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ServerConnection connection;
        public Window1(ServerConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            Start();
        }
        async void Start()
        {
            List<string> names = await connection.AllName();

            foreach (var name in names)
            {
                TextBlock nameTextBlock = new TextBlock
                {
                    Text = name,
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                NameStack.Children.Add(nameTextBlock);
            }
            List<string> ages = await connection.AllAge();

            foreach (var age in ages)
            {
                TextBlock ageTextBlock = new TextBlock
                {
                    Text = age,
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                AgeStack.Children.Add(ageTextBlock);
            }
        }

        async void createPerson(object s, EventArgs e)
        {
            bool valami = await connection.create(NameInput.Text, Convert.ToInt32(AgeInput.Text));
            if (valami)
            {

                MessageBox.Show("created person");
                NameStack.Children.Clear();
                AgeStack.Children.Clear();
                Start();
            }
        }
    }
}
