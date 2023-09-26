using Spike.Models;

namespace Spike
{
    public static class Seed
    {
        public static List<DocumentV1> DocumentsV1 = new() {
            new DocumentV1() {  Description = "TEST DESCRIPTION", IsDeleted = false, Title = "DOCUMENT 1", VersionTag = Guid.NewGuid(), Id = Guid.NewGuid() },
            new DocumentV1() {  Description = "TEST DESCRIPTION", IsDeleted = false, Title = "DOCUMENT 2", VersionTag = Guid.NewGuid(), Id = Guid.NewGuid() },
            new DocumentV1() {  Description = "TEST DESCRIPTION", IsDeleted = false, Title = "DOCUMENT 3", VersionTag = Guid.NewGuid(), Id = Guid.NewGuid() },
            new DocumentV1() {  Description = "TEST DESCRIPTION", IsDeleted = false, Title = "DOCUMENT 4", VersionTag = Guid.NewGuid(), Id = Guid.NewGuid() },
            new DocumentV1() {  Description = "TEST DESCRIPTION", IsDeleted = false, Title = "DOCUMENT 5", VersionTag = Guid.NewGuid(), Id = Guid.NewGuid() },
            new DocumentV1() {  Description = "TEST DESCRIPTION", IsDeleted = false, Title = "DOCUMENT 6", VersionTag = Guid.NewGuid(), Id = Guid.NewGuid() },
            new DocumentV1() {  Description = "TEST DESCRIPTION", IsDeleted = false, Title = "DOCUMENT 7", VersionTag = Guid.NewGuid(), Id = Guid.NewGuid() },
            new DocumentV1() {  Description = "TEST DESCRIPTION", IsDeleted = false, Title = "DOCUMENT 8", VersionTag = Guid.NewGuid() ,Id = Guid.NewGuid()}
        };

        public static List<AuthorV1> AuthorV1 = new() {
            new AuthorV1(){ FirstName = "Dustin", LastName = "Hickman", IsDeleted = false, VersionTag = Guid.NewGuid(), Id = Guid.NewGuid() },
            new AuthorV1(){ FirstName = "TEST", LastName = "TEST", IsDeleted = false, VersionTag = Guid.NewGuid(), Id = Guid.NewGuid()},
            new AuthorV1(){ FirstName = "TEST2", LastName = "TEST2", IsDeleted = false, VersionTag = Guid.NewGuid(), Id = Guid.NewGuid() },
            new AuthorV1(){ FirstName = "TEST3", LastName = "TEST3", IsDeleted = false, VersionTag = Guid.NewGuid(), Id = Guid.NewGuid() },
            new AuthorV1(){ FirstName = "TEST4", LastName = "TEST4", IsDeleted = false, VersionTag = Guid.NewGuid(), Id = Guid.NewGuid() },
            new AuthorV1(){ FirstName = "TEST5", LastName = "TEST5", IsDeleted = false, VersionTag = Guid.NewGuid(), Id = Guid.NewGuid() },
        };
    }
}
