using DATA.Repository.Abstraction;
using System.Linq.Expressions;

namespace DATA.Repository.Implementation
{
    public class Filter<T>
        where T :BaseEntity
    {
        #region Class Properties

        internal DateTime? ValidFrom { get; private set; }
        internal DateTime? ValidTo { get; private set; }
        internal Guid? VersionTag { get; private set; }
        internal HistoricFetchMode FetchMode { get; set; }
        public List<FilterGroup<T>> LogicGroups { get; set; } = new();
        public bool IncludeDeletedRecords { get; private set; } = false;
        public int PageNumber { get;  set; } = 1;
        public int PageSize { get;  set; } = 10;
        public List<Expression<Func<T, bool>>> Includes { get; private set; } = new();
        public List<OrderingCriteria> OrderingCriteria { get; private set; } = new();
        #endregion

        #region Fluent Setters 
        internal Filter<T> WithValidFrom(DateTime date)
        {
            ValidFrom = date;
            return this;
        }
        internal Filter<T> WithValidTo(DateTime date)
        {
            ValidTo = date;
            return this;
        }
        internal Filter<T> WithVersionTag(Guid tag)
        {
            VersionTag = tag;
            return this;
        }
        internal Filter<T> WithFetchMode(HistoricFetchMode mode)
        {
            FetchMode = mode;
            return this;
        }
        public Filter<T> AddLogicGroup(FilterGroup<T> group)
        {
            LogicGroups.Add(group);
            return this;
        }
        public Filter<T> IncludeDeleted()
        {
            IncludeDeletedRecords = true;
            return this;
        }
        public Filter<T> WithPageNumber(int pageNumber)
        {
            PageNumber = pageNumber;
            return this;
        }
        public Filter<T> WithPageSize(int pageSize)
        {
            PageSize = pageSize;
            return this;
        }
        public Filter<T> AddInclude(Expression<Func<T, bool>> include)
        {
            Includes.Add(include);
            return this;
        }
        public Filter<T> AddOrderingCriteria(OrderingCriteria criteria)
        {
            OrderingCriteria.Add(criteria);
            return this;
        }
        #endregion

        #region Validation
        public bool IsValid()
        {
            return IsValidDateRange() && IsValidVersionTag() && IsValidFetchMode();
        }

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
        #endregion

    }
}
