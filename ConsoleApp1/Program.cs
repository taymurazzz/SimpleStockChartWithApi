
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ConsoleTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // replace the "demo" apikey below with your own key from https://www.alphavantage.co/support/#api-key
            string QUERY_URL = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=IBM&apikey=VDLN9WT6UKNZPUAM";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(Dictionary<string, object>));

                List<decimal> closingPrices = new List<decimal>();

                foreach (KeyValuePair<string, object> kvp in json_data["Time Series (Daily)"])
                {
                    closingPrices.Add(Convert.ToDecimal( (Convert.ToString(((Dictionary<string, object>)kvp.Value)["4. close"])).Split('.')[0]) );
                    //closingPrices.Add(Convert.ToDecimal(((Dictionary<string, object>)kvp.Value)["4. close"]));

                }

                // Создание новой формы для отображения графика
                Form chartForm = new Form();
                chartForm.Text = "График изменения цены акций IBM";
                chartForm.ClientSize = new System.Drawing.Size(900, 700); // Установка размеров формы

                // Создание элемента управления Chart
                Chart chart = new Chart();
                chart.Dock = DockStyle.Fill;

                // Установка размеров графика
                chart.Size = new System.Drawing.Size(800, 600);

                // Создание серии данных для графика
                Series series = new Series("Цена закрытия");
                series.ChartType = SeriesChartType.Line;

                // Добавление точек на график
                for (int i = 0; i < closingPrices.Count; i++)
                {
                    series.Points.AddXY(i, closingPrices[i]);
                }

                // Создание области графика
                ChartArea chartArea = new ChartArea();
                chart.ChartAreas.Add(chartArea);

                // Добавление серии данных на график
                chart.Series.Add(series);

                // Добавление графика на форму
                chartForm.Controls.Add(chart);

                // Отображение формы с графиком
                Application.Run(chartForm);

            }
        }
    }
}





//using System;
//using System.Runtime.InteropServices;
//using System.Threading;
//namespace ConsoleApp1
//{
//    public  class Program
//    {

//        static void Main(string[] args)
//        {
//            for (int i = 0; i < 10; i++)
//            {
//                Console.SetCursorPosition(0, 0);
//                Console.Write($"\r{i}");
//                Console.SetCursorPosition(0, 1);
//                Console.Write($"\r{i}");
//                Console.SetCursorPosition(0, 2);
//                Console.Write($"\r{i}");

//                Thread.Sleep(1000);
//            }
//        }
//    }
//}
