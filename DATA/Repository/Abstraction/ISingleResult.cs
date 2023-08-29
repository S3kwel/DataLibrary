using DATA.Repository.Implementation;

namespace DATA.Repository.Abstraction
{
    public interface ISingleResult<TFilter, T>
        where TFilter : Filter<T>
        where T : BaseEntity
    {
        bool IsHistoric { get; }
        RequestStatus Status { get; }
        bool Succeeded { get; }
        T? Value { get; }

        void Deconstruct(out T? Value);
        bool Equals(object? obj);
        bool Equals(SingleResult<TFilter, T>? other);
        SingleResult<TFilter, T> Failure(T? value, Filter<T> filter);
        int GetHashCode();
        SingleResult<TFilter, T> PartialSucces(T? value, Filter<T> filter);
        SingleResult<TFilter, T> Success(T? value, Filter<T> filter);
        string ToString();
    }
}