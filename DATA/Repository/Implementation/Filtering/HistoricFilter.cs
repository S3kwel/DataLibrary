using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Filtering;
using DATA.Repository.Abstraction.Helpers;

namespace DATA.Repository.Implementation
{
    public class HistoricFilter<T> : BaseFilter<T>, IHistoricFilter, IFilter<T> where T : HistoricEntity
    {
        internal DateTime? ValidFrom { get; private set; }
        internal DateTime? ValidTo { get; private set; }
        internal Guid? VersionTag { get; private set; }
        internal HistoricFetchMode FetchMode { get; set; }

        #region Fluent API
        public HistoricFilter<T> WithValidFrom(DateTime date)
        {
            ValidFrom = date;
            return this;
        }
        public HistoricFilter<T> WithValidTo(DateTime date)
        {
            ValidTo = date;
            return this;
        }
        public HistoricFilter<T> WithVersionTag(Guid tag)
        {
            VersionTag = tag;
            return this;
        }
        public HistoricFilter<T> WithFetchMode(HistoricFetchMode mode)
        {
            FetchMode = mode;
            return this;
        }
        #endregion

        #region Validation
        private bool IsValidDate(DateTime? date)
        {
            DateTime InvalidDate = DateTime.MinValue;
            return date.HasValue && date.Value != InvalidDate;
        }
        private bool IsValidDateRange()
        {
            if (!IsValidDate(ValidFrom) || !IsValidDate(ValidTo))
                return false;

            return ValidFrom < ValidTo;
        }
        private bool IsValidVersionTag()
        {
            return VersionTag.HasValue && VersionTag != Guid.Empty;
        }
        private bool IsValidFetchMode()
        {
            switch (FetchMode)
            {
                case HistoricFetchMode.AtExactTime:
                    return IsValidDate(ValidFrom);
                case HistoricFetchMode.AllTime:
                    return true;
                case HistoricFetchMode.ActiveBetween:
                case HistoricFetchMode.ActiveWithin:
                case HistoricFetchMode.ActiveThrough:
                    return IsValidDate(ValidFrom) && IsValidDate(ValidTo);
                case HistoricFetchMode.Invalid:
                    return false; // Or true based on your requirement for "Invalid".
                default:
                    throw new InvalidOperationException($"Unknown fetch mode: {FetchMode}");
            }
        }
        new public bool IsValid()
        {
            return IsValidDateRange() && IsValidVersionTag() && IsValidFetchMode();
        }
        #endregion
    }


}
