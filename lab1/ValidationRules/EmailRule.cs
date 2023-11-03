﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace lab1.ValidationRules
{
    public class EmailRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
       System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                PageMain.canSave = false;
                return new ValidationResult(false, "Адрес электронной почты не задан! ");
            }

            string email = value.ToString();
            Regex regex = new Regex("^\\S +@\\S +\\.\\S + $");
            if (email.Contains("@") && email.Contains("."))
            {
                PageMain.canSave = true;
                return new ValidationResult(true, null);
            }
            else
            {
                PageMain.canSave = false;
                return new ValidationResult(false,
                   "Адрес электронной почты должен содержать символы @ и(.) точки \n Шаблон адреса: adress@mymail.com");
            }
        }
    }
}
