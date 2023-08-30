using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Pagination;

namespace DATA.Repository.Abstraction
{
    public record SingleResult<TFilter, T, TKey>(T? Value) : ISingleResult<TFilter, T, TKey>
    where TFilter : Filter<T, TKey>
    where T : BaseEntity<TKey>
    { 
        public T? Value { get; private set; } = Value;
        private Filter<T, TKey> _filter { get; } = new Filter<T, TKey>();

        public RequestStatus Status { get; private set; } = RequestStatus.NEW;
        public bool Succeeded { get; private set; } = false;
        public bool IsHistoric { get; private set; } = false;

        public SingleResult(T? value, Filter<T, TKey> filter, RequestStatus status) : this(value)
        {
            Succeeded = (status == RequestStatus.SUCCEEDED || status == RequestStatus.SUCCEEDED_WITH_ERRORS);
            Status = status;
            _filter = filter;
            IsHistoric = filter.FetchMode != HistoricFetchMode.Invalid;
        }

        public SingleResult<TFilter, T, TKey> Success(T? value, Filter<T, TKey> filter)
        {
            return new SingleResult<TFilter, T, TKey>(value, filter, RequestStatus.SUCCEEDED);
        }

        public SingleResult<TFilter, T, TKey> PartialSucces(T? value, Filter<T, TKey> filter)
        {
            return new SingleResult<TFilter, T, TKey>(value, filter, RequestStatus.SUCCEEDED_WITH_ERRORS);
        }

        public SingleResult<TFilter, T, TKey> Failure(T? value, Filter<T, TKey> filter)
        {
            return new SingleResult<TFilter, T, TKey>(value, filter, RequestStatus.FAILED);
        }

    }

    public class Result<TFilter, T, TKey>
        where TFilter : Filter<T,TKey>
        where T : BaseEntity<TKey>
    {
        public PaginationMetadata? Pagination { get; set; }


        private readonly Filter<T, TKey> _filter;
        public List<SingleResult<TFilter, T, TKey>>? Values { get; set; }
        public RequestStatus Status { get; private set; } = RequestStatus.NEW;
        public bool Succeeded
        {
            get
            {
                if (Values != null && Values.Any(v => v.Status == RequestStatus.FAILED))
                {
                    if (Values.All(v => v.Status == RequestStatus.FAILED))
                    {
                        Status = RequestStatus.FAILED;
                        return false;
                    }
                    else
                    {
                        Status = RequestStatus.SUCCEEDED_WITH_ERRORS;
                        return true;
                    }
                }
                return false;
            }
        }
        public bool IsHistoric
        {
            get
            {
                if (_filter == null)
                    return false;

                if (_filter.FetchMode != HistoricFetchMode.Invalid)
                {
                    return true;
                }
                return false;
            }
        }
        public Result(TFilter filter, RequestStatus status, List<SingleResult<TFilter, T, TKey>>? results)
        {


            Values = results; 
            _filter = filter; 
            Status = status;
        }
        public Result<TFilter, T, TKey> AddValue(SingleResult<TFilter, T, TKey> result)
        {


            if (Values == null)
                Values = new(); 

            Values.Add(result);
            return this;
        }
    }


}
