namespace RSpeditionWEB.Helpers
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Метод для проверки принадлежности даты (год + месяц + день) к определенному периоду
        /// </summary>
        /// <param name="str">Первоначальная строка</param>
        /// <returns>Коллекция строк</returns>
        public static bool IsInsidePeriod(this DateTime date, DateTime begin, DateTime end)
            => (date >= begin && date <= end) ? true : false;

        /// <summary>
        /// Метод для форматирования DateTime
        /// </summary>
        ///<param name="date">Дата, подлежащая форматированию</param>        
        /// <returns>отформатированная строка</returns>
        public static string FormatingDate(this DateTime date) => string.Concat(date.Day.Addzero(),
                                                                                    ".",
                                                                                    date.Month.Addzero(),
                                                                                    ".",
                                                                                    date.Year.AddzeroForYear()
                                                                                    );

        public static string FormatingDateODATA(this DateTime date) => string.Concat(date.Year.AddzeroForYear(),
                                                                                    "-",
                                                                                    date.Month.Addzero(),
                                                                                    "-",
                                                                                    date.Day.Addzero()
                                                                                    );

        public static string FormatingTime(this DateTime date) => string.Concat(date.Hour.Addzero(),
                                                                                ":",
                                                                                date.Minute.Addzero(),
                                                                                ":",
                                                                                date.Second.Addzero()
                                                                                );


        /// <summary>
        /// Метод для форматирования DateTime
        /// </summary>
        ///<param name="date">Дата, подлежащая форматированию</param>        
        /// <returns>отформатированная строка</returns>
        public static string FormatingDateTime(this DateTime date) => string.Concat(date.Day.Addzero(),
                                                                                    ".",
                                                                                    date.Month.Addzero(),
                                                                                    ".",
                                                                                    date.Year.AddzeroForYear(),
                                                                                    " ",
                                                                                    date.Hour.Addzero(),
                                                                                    ":",
                                                                                    date.Minute.Addzero()
                                                                                    );

        /// <summary>
        /// Метод форматирует число - добавляет ноль впереди числа, если оно меньше десяти
        /// </summary>
        /// <param name="datePart">Число, подлежащее форматированию</param>
        /// <returns></returns>
        private static string Addzero(this int datePart) => (datePart < 10) ? "0" + datePart.ToString() : datePart.ToString();

        /// <summary>
        /// Метод форматирует число - добавляет необходимое число нолей впереди числа
        /// </summary>
        /// <param name="datePart">Число, подлежащее форматированию</param>
        /// <returns>отформатированная строка</returns>
        public static string AddzeroForYear(this int datePart)
        {
            string result = string.Empty;

            if (datePart < 10)
            {
                result = string.Concat("000", datePart.ToString());
            }
            else if (datePart < 100)
            {
                result = string.Concat("00", datePart.ToString());
            }
            else if (datePart < 1000)
            {
                result = string.Concat("0", datePart.ToString());
            }
            else
                result = datePart.ToString();
            return result;
        }

        /// <summary>
        /// Метод для получения коллекции месяцев в формате строковой коллекции
        /// </summary>
        /// <param name="collection">Коллекция лет</param>
        public static void GetListMonths(this List<string> collection)
        {
            collection.Clear();

            for (int i = 0; i < 12; i++)
            {
                var month = (int)MonthsBinary.Январь << i;

                collection.Add(Enum.GetName(typeof(MonthsBinary), month));
            }
        }

        /// <summary>
        /// Метод для получения строкового представления заданного значения перечисления-месяца 
        /// </summary>
        /// <param name="monthEnum">Заданное значение перечисления-месяца </param>
        public static string GetStringMonth(this MonthsBinary monthEnum) => Enum.GetName(typeof(MonthsBinary), monthEnum) ?? string.Empty;

        /// <summary>
        /// Метод для получения значения перечисления-месяца на основании его индекса
        /// </summary>
        /// <param name="num">Порядковый номер месяца в году </param>
        public static MonthsBinary GetMonthsBinaryByNumInYear(this int num) => (MonthsBinary)Enum.GetValues(typeof(MonthsBinary)).GetValue(num - 1);

        /// <summary>
        /// Метод для получения коллекции лет в рамках заданного периода
        /// </summary>
        /// <param name="collection">Коллекция месяцев</param>
        public static List<int> GetListYears(this List<int> collection, int start = 1990, int end = 2040)
        {
            collection.Clear();

            for (int i = start; i <= end; i++)
            {
                collection.Add(i);
            }

            return collection;
        }

        /// <summary>
        /// Метод для преобразования даты в строку в формате ДД.ММ.ГГ
        /// </summary>
        /// <param name="date">Дата, подлежащая преобразованию</param>
        /// <returns>Строковое представление даты</returns>
        public static string GetStringDateTime(this DateTime date) => string.Concat(date.Day.Addzero(),
                                                                              CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator,
                                                                              date.Month.Addzero(),
                                                                              CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator,
                                                                              date.Year.AddzeroForYear());


        /// <summary>
        /// Метод для получения двух дат в пределах одного дня: xx.xx.xx 00:00:01 - xx.xx.xx 23:59:59
        /// </summary>
        /// <param name="day">Заданный день</param>
        public static string GetStringSomeDay(this DateTime day) => string.Concat(day.Year.AddzeroForYear(),
                                                                                  "-", 
                                                                                  day.Month.Addzero(),
                                                                                  "-",
                                                                                  day.Day.Addzero());


        /// <summary>
        /// Метод конвертирует строковое значение месяца в цифровое значение 
        /// </summary>
        /// <param name="month">Строковое значение месяца</param>
        /// <returns></returns>
        public static string GetStringMonth(this int month)
        {
            string result = string.Empty;

            switch (month)
            {
                case 1:
                    result = "Январь";
                    break;
                case 2:
                    result = "Февраль";
                    break;
                case 3:
                    result = "Март";
                    break;
                case 4:
                    result = "Апрель";
                    break;
                case 5:
                    result = "Май";
                    break;
                case 6:
                    result = "Июнь";
                    break;
                case 7:
                    result = "Июль";
                    break;
                case 8:
                    result = "Август";
                    break;
                case 9:
                    result = "Сентябрь";
                    break;
                case 10:
                    result = "Октябрь";
                    break;
                case 11:
                    result = "Ноябрь";
                    break;
                case 12:
                    result = "Декабрь";
                    break;
                default:
                    result = "Январь";
                    break;
            }

            return result;
        }

        /// <summary>
        /// Метод для определения, находится или нет указанная дата внутри заданного временного периода - учитывается только год, месяц и день
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="start">Начало периода</param>
        /// <param name="end">Окончание периода</param>
        /// <returns>Истина, если находится в пределах временного интервала</returns>
        public static bool IsDateInsideDayPeriod(this DateTime date, DateTime start, DateTime end)
        {
            var dateTrancated = new DateTime(year: date.Year,
                                             month: date.Month,
                                             day: date.Day,
                                             hour: 0,
                                             minute: 0,
                                             second: 0,
                                             millisecond: 0);

            var startTrancated = new DateTime(year: start.Year,
                                              month: start.Month,
                                              day: start.Day,
                                              hour: 0,
                                              minute: 0,
                                              second: 0,
                                              millisecond: 0);

            var endTrancated = new DateTime(year: end.Year,
                                              month: end.Month,
                                              day: end.Day,
                                              hour: 0,
                                              minute: 0,
                                              second: 0,
                                              millisecond: 0);

            return dateTrancated >= startTrancated && dateTrancated <= endTrancated;
        }

        /// <summary>
        /// Метод для установки времени даты в начало дня
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns>Отредактированная дата</returns>
        public static DateTime SetDateTimeToDaysBegin(this DateTime date) =>
                                                                            new DateTime(year: date.Year,
                                                                                         month: date.Month,
                                                                                         day: date.Day,
                                                                                         hour: 0,
                                                                                         minute: 0,
                                                                                         second: 0,
                                                                                         millisecond: 0);

        public static string FormatDateForLineChart(this DateTime date)
            => date.ToString("yyyy-MM-dd 00:00:00");
    }
}
