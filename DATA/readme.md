#Specification Pattern 

TODO:  Filter builder with a fluent interface! 

**Usage**

1. Define custom specification classes that derive from CompositeSpecification<Entity>.
2. Set the Criteria property in the constructor if there is a single condition to filter by.
3. To add additional conditions, use the And() and Or() methods, either within the constructor or elsewhere.

```csharp
public class EntityByNameSpecification : CompositeSpecification<Entity>
{
    public EntityByNameSpecification(string name)
    {
        Criteria = entity => entity.Name == name;
    }
}

public class EntityByAgeSpecification : CompositeSpecification<Entity>
{
    public EntityByAgeSpecification(int age)
    {
        Criteria = entity => entity.Age == age;
    }
}
```

```csharp
var nameSpec = new EntityByNameSpecification("John");
var ageSpec = new EntityByAgeSpecification(30);

var combinedSpec = nameSpec.And(ageSpec); // John AND age 30
var combinedSpecWithOr = nameSpec.Or(ageSpec); // John OR age 30
```