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
            List<string> ages = await connection.AllAge();

            for (int i = 0; i < names.Count; i++)
            {
                string name = names[i];
                string age = ages[i];

                TextBlock nameTextBlock = new TextBlock
                {
                    Text = name,
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                NameStack.Children.Add(nameTextBlock);

                TextBlock ageTextBlock = new TextBlock
                {
                    Text = age,
                    FontSize = 20,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                    
                };
                AgeStack.Children.Add(ageTextBlock);

                Button edit = new Button
                {
                    
                    Content = "Edit",
                    FontSize = 10,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 10, 0, 0),
                    Width = 100
                };

                

                Button deleteB = new Button
                {
                    Content = "X",
                    FontSize = 10,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0,10,0,0)
                };

                deleteB.Click += async (s, e) =>
                {
                    bool result = await connection.DeletePerson(name); 
                    if (result)
                    {
                        NameStack.Children.Remove(nameTextBlock);
                        AgeStack.Children.Remove(ageTextBlock);
                        deleteButtonStack.Children.Remove(deleteB);
                        EditStack.Children.Remove(edit);
                    }
                    else
                    {
                        MessageBox.Show("Error deleting the person.");
                    }
                };
                deleteButtonStack.Children.Add(deleteB);

                edit.Click += (s, e) =>
                {
                    LoginEditText.Text = "Szerkesztés";
                    CreateButtonn.Visibility = Visibility.Hidden;
                    DeleteAllButonn.Visibility = Visibility.Hidden;
                    NameInput.Text = "";
                    AgeInput.Text = "";


                    Button OK = new Button
                    {
                        Content = "OK",
                        FontSize = 20,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = 60,
                        Height = 50,
                        Margin = new Thickness(300, 0, 0, 0),
                        
                    };

                    OK.Click += async (ss, ee) =>
                    {
                        bool result = await connection.editP(NameInput.Text, Convert.ToInt32(AgeInput.Text));
                        if (result)
                        {
                            NameStack.Children.Clear();
                            AgeStack.Children.Clear();
                            deleteButtonStack.Children.Clear();
                            EditStack.Children.Clear(); 

                            Start();
                        }
                        else
                        {
                            MessageBox.Show("Error editing the person.");
                        }
                    };
                    LoginWindow.Children.Add(OK);
                };

                EditStack.Children.Add(edit);
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
                deleteButtonStack.Children.Clear();
                EditStack.Children.Clear();
                Start();
            }
        }

        async void deleteAll(object s, EventArgs e)
        {
            bool valami = await connection.DeleteAllPerson();
            if (valami)
            {
                MessageBox.Show("Deleted all person");
                NameStack.Children.Clear();
                AgeStack.Children.Clear();
                deleteButtonStack.Children.Clear();
                EditStack.Children.Clear();
                Start();
            }
        }
    }
}
