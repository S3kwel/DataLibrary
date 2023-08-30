using DATA.Repository.Implementation;

namespace DATA.Repository.Abstraction
{
    public interface ISingleResult<TFilter, T, TKey>
        where TFilter : Filter<T, TKey>
        where T : BaseEntity<TKey>
    {
        bool IsHistoric { get; }
        RequestStatus Status { get; }
        bool Succeeded { get; }
        T? Value { get; }

        void Deconstruct(out T? Value);
        bool Equals(object? obj);
        bool Equals(SingleResult<TFilter, T, TKey>? other);
        SingleResult<TFilter, T, TKey> Failure(T? value, Filter<T, TKey> filter);
        int GetHashCode();
        SingleResult<TFilter, T, TKey> PartialSucces(T? value, Filter<T, TKey> filter);
        SingleResult<TFilter, T, TKey> Success(T? value, Filter<T, TKey> filter);
        string ToString();
    }
}