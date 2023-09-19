using DATA.Repository.Abstraction.Filtering;
using DATA.Repository.Abstraction.Helpers;
using DATA.Repository.Abstraction.Models;
using DATA.Repository.Implementation;

namespace DATA.Repository.Abstraction
{
    public record SingleResult<T>(T? Value) : ISingleResult<T>
    where T : IBaseEntity
    { 
        public T? Value { get; private set; } = Value;
        private IFilter<T> _filter { get; } = new BaseFilter<T>();
        public RequestStatus Status { get; private set; } = RequestStatus.NEW;
        public bool Succeeded { get; private set; } = false;
        public bool IsHistoric { get; private set; } = false;

        public SingleResult(T? value, IFilter<T> filter, RequestStatus status) : this(value)
        {
            Succeeded = (status == RequestStatus.SUCCEEDED || status == RequestStatus.SUCCEEDED_WITH_ERRORS);
            Status = status;
            _filter = filter;
            IsHistoric = filter is IHistoricFilter;
        }

        public SingleResult<T> Success(T? value, BaseFilter<T> filter)
        {
            return new SingleResult<T>(value, filter, RequestStatus.SUCCEEDED);
        }

        public SingleResult<T> PartialSucces(T? value, BaseFilter<T> filter)
        {
            return new SingleResult<T>(value, filter, RequestStatus.SUCCEEDED_WITH_ERRORS);
        }

        public SingleResult<T> Failure(T? value, BaseFilter<T> filter)
        {
            return new SingleResult<T>(value, filter, RequestStatus.FAILED);
        }

    }

    public interface IFilter<T> where T : IBaseEntity
    {
    }

    public interface IResult<T> where T: IBaseEntity
    {
        RequestStatus Status { get; set; }
        public bool Succeeded { get;}
        public string Message { get; set; } 
    }

    public class Result<TFilter, T>: IResult<T>
        where TFilter : BaseFilter<T>
        where T : IBaseEntity
    {
        public PaginationMetadata? Pagination { get; set; }


        private readonly BaseFilter<T>? _filter;
        public string Message { get; set; } = string.Empty;
        public List<SingleResult<T>>? Values { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.NEW;
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
       
        public Result(TFilter? filter, RequestStatus status, List<SingleResult<T>>? results)
        {
            Values = results; 
            _filter = filter; 
            Status = status;
        }
        public Result<TFilter, T> AddValue(SingleResult<T> result)
        {


            if (Values == null)
                Values = new(); 

            Values.Add(result);
            return this;
        }
    }


}
