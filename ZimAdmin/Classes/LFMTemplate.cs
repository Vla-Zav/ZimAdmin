using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimAdmin.Entitys;

namespace ZimAdmin.Classes
{
    public class LFMTemplate
    {
        public static bool LFMComplite(string lastName, string firstName, string middleName, StringBuilder errors)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                errors.AppendLine("Фамилия не указана");
                return false;
            }
            else if (lastName.Length < 2)
            {
                errors.AppendLine("Фамилия слишком короткая (минимум 2 буквы)");
                return false;
            }
            if (string.IsNullOrWhiteSpace(firstName))
            {
                errors.AppendLine("Имя не указано");
                return false;
            }
            else if (firstName.Length < 2)
            {
                errors.AppendLine("Имя слишком короткое (минимум 2 буквы)");
                return false;
            }
            if (string.IsNullOrWhiteSpace(middleName))
            {
                errors.AppendLine("Отчество не указано");
                return false;
            }
            else if (middleName.Length < 5)
            {
                errors.AppendLine("Отчество слишком короткое (минимум 5 букв)");
                return false;
            }
            return true;
        }
    }
}
