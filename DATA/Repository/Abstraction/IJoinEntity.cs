using System.ComponentModel.DataAnnotations;

namespace DATA.Repository.Abstraction
{
    public interface IJoinEntity<T1, T2>
    {

        Guid JoinID { get; }
        Guid Type1ID { get; set; }
        T1 Type1 { get; set; }

        Guid Type2ID { get; set; }
        T2 Type2 { get; set; }
    }
}
