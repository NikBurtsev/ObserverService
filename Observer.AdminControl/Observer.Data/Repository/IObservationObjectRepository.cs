namespace Observer.Data
{
    public interface IObservationObjectRepository<T>
    {
        public T? Elements { get; set; }

        /// <summary>
        /// Прочитать объект/ы
        /// </summary>
        T Init();

        /// <summary>
        /// Сохранить состояние в файл/БД
        /// </summary>
        /// <returns></returns>
        void Update();
    }
}