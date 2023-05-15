using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZimAdmin.Classes
{
    internal class CharsBlocker
    {
        /// <summary>
        /// Метод блокирует возможность ввода в поле первым символом нуля
        /// </summary>
        /// <param name="text">Текстовое поле ввода</param>
        /// <param name="e">Нажатая клавиша</param>
        public void zeroFirstBlocker(string text, KeyEventArgs e)
        {
            if (text.Length == 0)
                if (e.Key == Key.D0)
                    e.Handled = true;
        }

        /// <summary>
        /// Метод блокирует возможность ввода пробела
        /// </summary>
        /// <param name="e">Нажатая клавиша</param>
        public void spaceBlocker(KeyEventArgs e)
        { 
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        /// <summary>
        /// Метод блокирует возможность стирания данных из полоя
        /// </summary>
        /// <param name="e"></param>
        public void backSpaceBlocker(KeyEventArgs e)
        {
            if(e.Key == Key.Back)
                e.Handled = true;
        }

        /// <summary>
        /// Метод блокирует возможность стирания данных из полоя
        /// </summary>
        /// <param name="e"></param>
        public void backSpaceBlocker(KeyEventArgs e)
        {
            if (e.Key == Key.Back)
                e.Handled = true;
        }

        /// <summary>
        /// Метод позволяет вводить только цифры
        /// </summary>
        /// <param name="e">Нажатая клавиша</param>

        public void onlyNumbers(TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"[0-9]");
        }

        public void onlyLetters(TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = !Char.IsLetter(e.Text, 0);
            }catch { }
        }

        /// <summary>
        /// Метод блокирует возможность ввода спецсимволов
        /// </summary>
        /// <param name="e">Нажатая клавиша</param>
        public void specialCharsBlocker(TextCompositionEventArgs e)
        {
            //Оператор try необходим для избежания фатальной ошибки ввода символа
            try
            {
                if (!Char.IsLetter(e.Text, 0) && !Char.IsNumber(e.Text, 0))
                    e.Handled = true;
            }catch { }
        }
    }
}
