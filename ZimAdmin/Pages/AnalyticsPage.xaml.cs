using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZimAdmin.Classes;
using ZimAdmin.Entitys;

namespace ZimAdmin.Pages
{
    /// <summary>
    /// Логика взаимодействия для AnalyticsPage.xaml
    /// </summary>
    public partial class AnalyticsPage : Page
    {
        public AnalyticsPage()
        {
            InitializeComponent();
            chartsServices.ChartAreas.Add(new ChartArea("Main"));
            cbAnalytics.ItemsSource = Enum.GetValues(typeof(AnalyticsTypes));

            var currentSeries = new Series("Appointment")
            {
                IsValueShownAsLabel = true
            };
            chartsServices.Series.Add(currentSeries);

            cbAnalytics.SelectedIndex = 0;
        }
        private void selectedAnalytics()
        {
            Series currentSeries = chartsServices.Series.FirstOrDefault();
            currentSeries.Points.Clear();

            DbSet<Types_of_services> typesServices = GetDbContext.GetContext().Types_of_services;
            DbSet<Appointments> appointments = GetDbContext.GetContext().Appointments;
            DbSet<Doctors> doctors = GetDbContext.GetContext().Doctors;
            DbSet<Work_shift> shifts = GetDbContext.GetContext().Work_shift;

            List<Appointments> appointmentsList = appointments.ToList();


            switch (cbAnalytics.SelectedIndex)
            {
                case 0:
                    {
                        foreach (var serv in typesServices)
                        {
                            currentSeries.ChartType = SeriesChartType.Doughnut;
                            
                            currentSeries.Points.AddXY(typesServices.Find(serv.id_Type).Name,
                                appointments.Where(a => a.Doctors.Specialty == serv.id_Type).Count());
                        }
                    }
                    break;
                case 1:
                    {
                        foreach (var doc in doctors)
                        {
                            currentSeries.ChartType = SeriesChartType.Pie;
                            
                            currentSeries.Points.AddXY($"{doc.Last_Name} {doc.First_Name}",
                                appointments.ToList().Where(a => a.id_Doctor == doc.id_Doctor).Sum(s => s.Doctors.Types_of_services.Cost));
                        }
                    }
                    break;
                case 2:
                    {
                        foreach (var shift in shifts)
                        {
                            currentSeries.ChartType = SeriesChartType.Column;

                            currentSeries.Points.AddXY(shift.Number,
                                doctors.ToList().Where(d => d.Shift == shift.id_Shift).Count());
                        }
                    }
                    break;
            }
        }

        private void cbAnalytics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedAnalytics();
        }
    }
}