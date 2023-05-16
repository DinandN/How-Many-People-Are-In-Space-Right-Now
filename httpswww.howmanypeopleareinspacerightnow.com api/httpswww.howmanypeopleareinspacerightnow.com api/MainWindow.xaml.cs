using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

using Newtonsoft.Json;


namespace httpswww.howmanypeopleareinspacerightnow.com_api
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Person
        {
            public string Craft { get; set; }
            public string Name { get; set; }
        }
        public class DataModel
        {
            public string message { get; set; }
            public int number { get; set; }
            public List<Person> People { get; set; }

            // Add other properties that match the JSON structure
        }

        public MainWindow()
        {
            InitializeComponent();
            FetchApiData();
        }

        private async void FetchApiData()
        {
            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Make a GET request to the API
                    HttpResponseMessage response = await client.GetAsync("http://api.open-notify.org/astros.json");
                    response.EnsureSuccessStatusCode();

                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Update the text block with the response data
                    DataModel data = JsonConvert.DeserializeObject<DataModel>(responseBody);

                    number.Text = "" + data.number ;
                    message.Text = "Message: " + data.message;
                    foreach (Person person in data.People)
                    {
                        crafts.Text +=  $"{person.Craft} \n";
                        names.Text += $"{ person.Name} \n";
                    }


                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "API Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
