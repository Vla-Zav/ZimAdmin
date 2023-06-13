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
using Excel = Microsoft.Office.Interop.Excel;
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
        /// <summary>
        /// Выбор данных для аналитики
        /// </summary>
        private void SelectedAnalytics()
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
                            
                            currentSeries.Points.AddXY(typesServices.Find(serv.Id_Type).Name,
                                appointments.Where(a => a.Doctors.Specialty == serv.Id_Type).Count());
                            
                            exportPanel.Visibility = Visibility.Collapsed;
                        }
                    }
                    break;
                case 1:
                    {
                        foreach (var doc in doctors)
                        {
                            currentSeries.ChartType = SeriesChartType.Pie;
                            
                            currentSeries.Points.AddXY($"{doc.Last_Name} {doc.First_Name}",
                                appointments.ToList().Where(a => a.Id_Doctor == doc.Id_Doctor).Sum(s => s.Doctors.Types_of_services.Cost));
                        
                            exportPanel.Visibility = Visibility.Visible;
                        }
                    }
                    break;
                case 2:
                    {
                        foreach (var shift in shifts)
                        {
                            currentSeries.ChartType = SeriesChartType.StackedColumn;

                            currentSeries.Points.AddXY(shift.Number,
                                doctors.ToList().Where(d => d.Shift == shift.Id_Shift).Count());
                         
                            exportPanel.Visibility = Visibility.Collapsed;
                        }
                    }
                    break;
            }
        }

        private void cbAnalytics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedAnalytics();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            List<Doctors> doctors = GetDbContext.GetContext().Doctors.OrderBy(d => d.Last_Name).ToList();

            Excel.Application applicationExcel = new Excel.Application();

            Excel.Workbook workbook = applicationExcel.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = applicationExcel.Worksheets.Item[1];

            worksheet.Name = "Варчи";
            worksheet.Cells[1][1] = "Фамилия";
            worksheet.Cells[2][1] = "Имя";
            worksheet.Cells[3][1] = "Отчество";
            worksheet.Cells[4][1] = "Специальность";
            worksheet.Cells[5][1] = "Смена";
            worksheet.Cells[6][1] = "Зарплата";

            int rowsData = 2;
            Excel.Range cellCost;
            foreach (var doctor in doctors)
            {
                Excel.Range range = worksheet.Range[worksheet.Cells[1][rowsData], worksheet.Cells[6][rowsData]];
                worksheet.Cells[1][rowsData] = doctor.Last_Name;
                worksheet.Cells[2][rowsData] = doctor.First_Name;
                worksheet.Cells[3][rowsData] = doctor.Middle_Name;
                worksheet.Cells[4][rowsData] = doctor.Types_of_services.Name;
                worksheet.Cells[5][rowsData] = doctor.Work_shift.Number;
                cellCost = worksheet.Cells[6][rowsData];
                cellCost.NumberFormat = "#,##0₽";
                cellCost.Value = doctor.Appointments.Where(a => a.Id_Doctor == doctor.Id_Doctor).Sum(s => s.Doctors.Types_of_services.Cost);
                worksheet.Cells[6][rowsData] = doctor.Appointments.Where(a => a.Id_Doctor == doctor.Id_Doctor).Sum(s => s.Doctors.Types_of_services.Cost);
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                rowsData++;
            }

            Excel.Range sumCost = worksheet.Range[worksheet.Cells[1][rowsData], worksheet.Cells[5][rowsData]];
            sumCost.Merge();
            sumCost.Value = "Общий доход:";

            cellCost = worksheet.Cells[6][rowsData];

            cellCost.NumberFormat = "#,##0₽";
            cellCost.Formula = $"=SUM(F{rowsData - doctors.Count}:F{rowsData - 1})";
            sumCost.Font.Bold = worksheet.Cells[6][rowsData].Font.Bold = true;

            Excel.Range borderRangeHeader = worksheet.Range[$"A1:F1"];
            borderRangeHeader.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borderRangeHeader.Borders.Weight = Excel.XlBorderWeight.xlThick;

            Excel.Range borderRange = worksheet.Range[$"A2:F{rowsData}"];
            borderRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borderRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

            worksheet.Columns.AutoFit();

            applicationExcel.Visible = true;

            workbook.SaveAs($"Таблица доходов {DateTime.Now.ToShortDateString()}.xlsx");
        }
    }
}