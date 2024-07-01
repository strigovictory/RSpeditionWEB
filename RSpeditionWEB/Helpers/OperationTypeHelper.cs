namespace RSpeditionWEB.Helpers
{
    public static class OperationTypeHelper
    { 
        public static Dictionary<int, EventType> OperationTypes_GpsTracker
            => new Dictionary<int, EventType>
            {
                        {1, EventType.Склад },
                        {2, EventType.Тягач },
                        {3, EventType.Ремонт },
                        {4, EventType.Поломка },
                        {5, EventType.Списание },
                        {6, EventType.Sim },
                        {7, EventType.Прицеп }
            };

        public static Dictionary<EventType, int> OperationTypesReversed_GpsTracker
    => new Dictionary<EventType, int>
    {
                        {EventType.Склад, 1 },
                        {EventType.Тягач, 2 },
                        {EventType.Ремонт, 3 },
                        {EventType.Поломка, 4 },
                        {EventType.Списание, 5 },
                        {EventType.Sim, 6 },
                        {EventType.Прицеп, 7 }
    };

        public static Dictionary<int, EventType> OperationTypes_SimCard
            => new Dictionary<int, EventType>
            {
                        {1,  EventType.Склад},
                        {2,  EventType.Тягач},
                        {3,  EventType.Трекер},
                        {4,  EventType.Сотрудник},
                        {5,  EventType.Поломка},
                        {6,  EventType.Списание}
            };

        public static Dictionary<EventType, int> OperationTypesReversed_SimCard
            => new Dictionary<EventType, int>
            {
                {EventType.Склад, 1},
                {EventType.Тягач, 2},
                {EventType.Трекер, 3},
                {EventType.Сотрудник, 4},
                {EventType.Поломка, 5},
                {EventType.Списание, 6}
            };
    }
}
