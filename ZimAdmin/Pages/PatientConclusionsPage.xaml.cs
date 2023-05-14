using System;
using System.Collections.Generic;
using System.IO;
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
using ZimAdmin.Classes;
using ZimAdmin.Entitys;
using Word = Microsoft.Office.Interop.Word;

namespace ZimAdmin.Pages
{
    /// <summary>
    /// Логика взаимодействия для PatientConclusionsPage.xaml
    /// </summary>
    public partial class PatientConclusionsPage : Page
    {
        private Patients currentPatient = new Patients();
        public PatientConclusionsPage(Patients patients)
        {
            InitializeComponent();
            currentPatient = patients;
            dgCounclusion.ItemsSource = GetDbContext.GetContext().Conclusions.Where(p => p.id_Patient == patients.id_Patient).ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ManageClass.getFrame.GoBack();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            Conclusions conclusion = (Conclusions)dgCounclusion.SelectedItem;

            if (conclusion == null )
            {
                MessageBox.Show("Выберете заключение для составления документа");
                return;
            }

            try
            {

                Word.Application application = new Word.Application();

                Word.Document document = application.Documents.Add();

                Word.Paragraph headerParagraph = document.Paragraphs.Add();
                Word.Range headerRange = headerParagraph.Range;
                headerRange.Text = $"Приём {conclusion.Doctors.Types_of_services.Name}а";
                headerRange.Font.Name = "Times New Roman";
                headerRange.Font.Size = 24;
                headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                Word.Paragraph patientParagraph = document.Paragraphs.Add();
                Word.Range patientRange = headerParagraph.Range;
                patientRange.Text = $"ФИО пациента: {conclusion.Patients.Last_Name} {conclusion.Patients.First_Name} {conclusion.Patients.Middle_Name}." +
                    $"Дата рождения: {conclusion.Patients.Bithday.ToShortDateString()}.";
                patientRange.Font.Size = 14;
                patientRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                Word.Paragraph doctorParagraph = document.Paragraphs.Add();
                Word.Range doctorRange = headerParagraph.Range;
                doctorRange.Text = $"ФИО врача: {conclusion.Doctors.Last_Name} {conclusion.Doctors.First_Name} {conclusion.Doctors.Middle_Name}.\n";

                Word.Paragraph complaintParagraph = document.Paragraphs.Add();
                Word.Range complaintRange = headerParagraph.Range;
                complaintRange.Text = $"Жалобы:\n{conclusion.Complaint}.\n";

                Word.Paragraph lifeHistoryParagraph = document.Paragraphs.Add();
                Word.Range lifeHistoryRange = headerParagraph.Range;
                lifeHistoryRange.Text = $"Анемниз жизни:\n{conclusion.Life_History}.\n";

                Word.Paragraph objectiveStatusParagraph = document.Paragraphs.Add();
                Word.Range objectiveStatusRange = headerParagraph.Range;
                objectiveStatusRange.Text = $"Объективный статус:\n{conclusion.Objective_Status}.\n";

                Word.Paragraph diagnosisParagraph = document.Paragraphs.Add();
                Word.Range diagnosisRange = headerParagraph.Range;
                diagnosisRange.Text = $"Диагноз:\n{conclusion.Diagnosis}.\n";

                Word.Paragraph treatmentParagraph = document.Paragraphs.Add();
                Word.Range treatmentRange = headerParagraph.Range;
                treatmentRange.Text = $"Лечение:\n{conclusion.Treatment}.\n";

                Word.Paragraph datetimeParagraph = document.Paragraphs.Add();
                Word.Range datetimeRange = headerParagraph.Range;
                datetimeRange.Text = $"Дата и время заключения: {conclusion.DataTime_Conclusion}\n";

                Word.Paragraph datetimeIssueParagraph = document.Paragraphs.Add();
                Word.Range datetimeIssueRange = headerParagraph.Range;
                datetimeIssueRange.Text = $"Дата выдачи заключение: {DateTime.Today.ToShortDateString()}\n";

                Word.Paragraph signatureParagraph = document.Paragraphs.Add();
                Word.Range signatureRange = headerParagraph.Range;
                signatureRange.Text = "Подпись врача: __________";
                signatureRange.Font.Bold = 1;

                application.Visible = true;

                document.SaveAs2($"Заключение от {DateTime.Today.ToShortDateString()} В.{conclusion.Doctors.Last_Name} П.{conclusion.Patients.Last_Name} №{conclusion.id_Counclusion}.docx");
                document.SaveAs2($"Заключение от {DateTime.Today.ToShortDateString()} В.{conclusion.Doctors.Last_Name} П.{conclusion.Patients.Last_Name} №{conclusion.id_Counclusion}.pdf", Word.WdExportFormat.wdExportFormatPDF);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка работы с документом", MessageBoxButton.OK, MessageBoxImage.Warning); }
        }
    }
}