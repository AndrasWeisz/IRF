using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fejlesztesi_Mintak.MNBServiceReference;
using Fejlesztesi_Mintak.Entities;
using System.Xml;
using System.Web.UI.DataVisualization.Charting;

namespace Fejlesztesi_Mintak
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        public Form1()
        {
            InitializeComponent();
            PrintChart();

            GetXmlData(GetRates());

            dataGridView1.DataSource = Rates;
        }

        private void PrintChart()
        {
            dataGridView1.DataSource = Rates;

            var series = dataGridView1.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = dataGridView1.Legends[0];
            legend.Enabled = false;

            var chartArea = dataGridView1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private string GetRates()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;

            return result;
        }

        private void GetXmlData(string result)
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement item in xml.DocumentElement)
            {
                var date = item.GetAttribute("date");

                var rate = (XmlElement)item.ChildNodes[0];

                var unit = int.Parse(rate.GetAttribute("unit"));

                var currency = rate.GetAttribute("curr");

                var value = decimal.Parse(rate.InnerText);

              

                Rates.Add(new RateData()
                {
                    Date = DateTime.Parse(date),
                    Currency = currency,
                    Value = unit != 0 
                    ? value / unit
                    : 0
                }) ;
                
                
            }
            
        }
    }

}
