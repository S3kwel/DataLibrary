using DATA.Repository.Abstraction.Helpers;
using DATA.Repository.Abstraction.Models;
using DATA.Repository.Implementation;

namespace DATA.Repository.Abstraction.Filtering
{
    public interface ISingleResult<T>
        where T : IBaseEntity
    {
        bool IsHistoric { get; }
        RequestStatus Status { get; }
        bool Succeeded { get; }
        T? Value { get; }

        void Deconstruct(out T? Value);
        bool Equals(object? obj);
        bool Equals(SingleResult<T>? other);
        SingleResult<T> Failure(T? value, BaseFilter<T> filter);
        int GetHashCode();
        SingleResult<T> PartialSucces(T? value, BaseFilter<T> filter);
        SingleResult<T> Success(T? value, BaseFilter<T> filter);
        string ToString();
    }
}