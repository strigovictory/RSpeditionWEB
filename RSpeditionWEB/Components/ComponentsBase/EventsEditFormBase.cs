namespace RSpeditionWEB.Components.ComponentsBase
{
    public abstract class EventsEditFormBase<T, V> : TrucksEmployeesChoiseBase<T> where T : class, ICloneable, new()
    {
        // Коллекция всех событий (операций) по экземпляра типа V
        [Parameter]
        public List<T> HistoryEvents { get; set; }

        [Parameter]
        public List<KitEventTypeView> KitEventTypes { get; set; }

        [Parameter]
        public V EventSource { get; set; }

        // Дата начала события
        protected DateTime? EventsStartDate { get; set; }

        // Дата начала события
        protected DateTime? EventsFinishDate { get; set; }

        // Последнее событие - если текущее событие первое в цепочке событий, то значение будет равно null
        protected T TEventLast { get; set; }

        // Событие, предшествующее текущему событию - если текущее событие первое в цепочке событий, то значение будет равно null
        protected T TEventPrevious { get; set; }

        // Событие, следующее за текущим событием - если текущее событие последнее в цепочке событий, то значение будет равно null
        protected T TEventNext { get; set; }

        protected bool isLastEvent => this.TEventNext == null || this.TEventNext == default;
        protected bool isFirstEvent => this.TEventPrevious == null || this.TEventPrevious == default;
        protected abstract DateTime? startPrevious { get; }
        protected abstract DateTime? startNext { get; }
        protected abstract DateTime? finishNext { get; }
        protected abstract DateTime? startLast { get; }

        // Сообщения валидации дат
        protected string ValidationMessageStartDate { get; set; }
        protected string ValidationMessageFinishDate { get; set; }

        // Утверждения для валидации дат событий   
        // - 
        private Func<DateTime?, DateTime?, bool> StartNewEventDateLaterStartLastEventDate
            = delegate (DateTime? start, DateTime? startLast) { if (start != null && startLast != null) return startLast?.Date <= start?.Date; else return true; };
        // -
        private Func<DateTime?, DateTime?, bool> FinishEventDateLaterStartEventDate
            = delegate (DateTime? start, DateTime? finish) { if (start != null && finish != null) return finish?.Date >= start?.Date; else return true; };
        // - 
        private Func<DateTime?, DateTime?, bool> StartEventDateLaterStartEventDatePreviousEvent
        = delegate (DateTime? startPrevious, DateTime? start) { if (startPrevious != null && start != null) return start?.Date >= startPrevious?.Date; else return true; };
        // - 
        private Func<DateTime?, DateTime?, bool> FinishDateNextEventLaterFinishDateEvent
        = delegate (DateTime? finishNext, DateTime? finish) { if (finishNext != null && finish != null) return finishNext?.Date >= finish?.Date; else return true; };
        // - 
        private Func<bool, DateTime?, bool> FinishDateIsNullIfEventIsLast
        = delegate (bool isLastEvent, DateTime? finish) { if (isLastEvent) return finish == null; else return true; };
        // - 
        private Func<DateTime?, bool> StartDateIsNotNull
        = delegate (DateTime? start) { return start != null && start != default && start != DateTime.MinValue; };

        // - Если редактируется предпоследнее событие - когда дата окончания следующего не установлена
        private Func<DateTime?, DateTime?, DateTime?, bool> StartDateNextEventLaterFinishDateEventIfNextIsLast
        = delegate (DateTime? startNext, DateTime? finishNext, DateTime? finish) { 
            if (finish != null && startNext != null && finishNext == null ) return startNext.Value.Date >= finish?.Date; else return true; };

        /// <summary>
        /// Метод для преверки валидности дат события по отношению друг к другу
        /// </summary>
        protected void CheckPropDates()
        {
            this.ValidationMessageStartDate = string.Empty;
            this.ValidationMessageFinishDate = string.Empty;
            // - 
            if (this.IsCreate && !this.StartNewEventDateLaterStartLastEventDate(this.EventsStartDate, this.startLast))
                this.ValidationMessageStartDate += " Дата начала нового события не может быть раньше даты начала последнего события!";
            // - 
            if (!this.FinishEventDateLaterStartEventDate(this.EventsStartDate, this.EventsFinishDate))
            {
                this.ValidationMessageStartDate += " Дата начала события не может быть позже даты его окончания !";
                this.ValidationMessageFinishDate += " Дата окончания события не может быть раньше даты его начала !";
            }
            // - 
            if (!this.StartEventDateLaterStartEventDatePreviousEvent(this.startPrevious, this.EventsStartDate))
                this.ValidationMessageStartDate += " Дата начала события не может быть раньше даты начала предыдущего события !";
            // - 
            if (!this.FinishDateNextEventLaterFinishDateEvent(this.finishNext, this.EventsFinishDate))
                this.ValidationMessageFinishDate += " Дата окончания события не может быть позже даты окончания последующего события !";
            // - 
            if (!this.FinishDateIsNullIfEventIsLast(this.isLastEvent, this.EventsFinishDate))
                this.ValidationMessageFinishDate += " Дата окончания у последнего события должна быть открытой!";
            // -
            if(!this.StartDateIsNotNull(this.EventsStartDate))
                this.ValidationMessageStartDate += " Дата начала события обязательна для заполнения !";
            // -
            if (!this.StartDateNextEventLaterFinishDateEventIfNextIsLast(this.startNext, this.finishNext, this.EventsFinishDate))
                this.ValidationMessageFinishDate += " Дата окончания предпоследнего события не может быть позже даты старта последнего события !";
        }
    }
}
