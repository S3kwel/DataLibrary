using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Pagination;

namespace DATA.Repository.Abstraction
{
    public record SingleResult<TFilter, T>(T? Value) : ISingleResult<TFilter, T>
    where TFilter : Filter<T>
    where T : BaseEntity
    { 
        public T? Value { get; private set; } = Value;
        private Filter<T> _filter { get; } = new Filter<T>();

        public RequestStatus Status { get; private set; } = RequestStatus.NEW;
        public bool Succeeded { get; private set; } = false;
        public bool IsHistoric { get; private set; } = false;

        public SingleResult(T? value, Filter<T> filter, RequestStatus status) : this(value)
        {
            Succeeded = (status == RequestStatus.SUCCEEDED || status == RequestStatus.SUCCEEDED_WITH_ERRORS);
            Status = status;
            _filter = filter;
            IsHistoric = filter.FetchMode != HistoricFetchMode.Invalid;
        }

        public SingleResult<TFilter, T> Success(T? value, Filter<T> filter)
        {
            return new SingleResult<TFilter, T>(value, filter, RequestStatus.SUCCEEDED);
        }

        public SingleResult<TFilter, T> PartialSucces(T? value, Filter<T> filter)
        {
            return new SingleResult<TFilter, T>(value, filter, RequestStatus.SUCCEEDED_WITH_ERRORS);
        }

        public SingleResult<TFilter, T> Failure(T? value, Filter<T> filter)
        {
            return new SingleResult<TFilter, T>(value, filter, RequestStatus.FAILED);
        }

    }

    public class Result<TFilter, T>
        where TFilter : Filter<T>
        where T : BaseEntity
    {
        public PaginationMetadata? Pagination { get; set; }


        private readonly Filter<T> _filter;
        public List<SingleResult<TFilter, T>>? Values { get; set; }
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
        public Result(TFilter filter, RequestStatus status, List<SingleResult<TFilter, T>>? results)
        {


            Values = results; 
            _filter = filter; 
            Status = status;
        }
        public Result<TFilter, T> AddValue(SingleResult<TFilter, T> result)
        {


            if (Values == null)
                Values = new(); 

            Values.Add(result);
            return this;
        }
    }


}
