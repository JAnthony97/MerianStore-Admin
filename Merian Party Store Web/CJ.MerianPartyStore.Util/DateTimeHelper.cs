using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.Util
{
    public class DateTimeHelper
    {
        public static DateTime PeruDateTime
        {
            get { return DateTime.UtcNow.AddHours(-5); }
        }

        public static DateTime FromString(String sDateTime, bool Date, bool Time)
        {
            String Format = "";

            if (Date)
                Format += "dd/MM/yyyy";

            if (Time)
            {
                if (!String.IsNullOrWhiteSpace(Format))
                    Format += " ";

                Format += "HH:mm";
            }

            return DateTime.ParseExact(sDateTime, Format, CultureInfo.InvariantCulture);
        }

        public static String ToString(DateTime objDateTime, bool Date, bool Time)
        {
            String Format = "";

            if (Date)
                Format += "dd/MM/yyyy";

            if (Time)
            {
                if (!String.IsNullOrWhiteSpace(Format))
                    Format += " ";

                Format += "HH:mm";
            }

            return objDateTime.ToString(Format);
        }

        public static String MonthToString(int Month)
        {
            switch (Month)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Setiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
                default:
                    return null;
            }
        }

        public static int CalculateAge(DateTime Birthday)
        {
            int Age = (PeruDateTime.Year - Birthday.Year);
            if (PeruDateTime.AddYears(-Age) < Birthday.Date)
                Age--;

            return Age;
        }
    }
}
