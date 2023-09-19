using DATA.Repository.Abstraction.Helpers;
using DATA.Repository.Abstraction.Models;

namespace DATA.Repository.Abstraction
{
    public class SimpleResult<T> : IResult<T>
        where T: IBaseEntity
    {
        public RequestStatus Status { get; set; } = RequestStatus.NEW; 
        public bool Succeeded { get {
                return Status != RequestStatus.NEW || Status != RequestStatus.FAILED;
            
            }}
        public string Message { get; set; } = string.Empty;
    }


}
