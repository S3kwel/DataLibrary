using DATA.Repository.Abstraction.Helpers;
using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Models;

namespace DATA.Repository.Abstraction.Filtering
{
    public interface ISingleResult<T>
        where T : BaseEntity
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