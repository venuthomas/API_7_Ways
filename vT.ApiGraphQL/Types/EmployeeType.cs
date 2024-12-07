using vT.ApiDomains.Models;

namespace vT.ApiGraphQL.Types;

public class EmployeeType : ObjectType<Employee>
{
    protected override void Configure(IObjectTypeDescriptor<Employee> descriptor)
    {
        descriptor.Field(f => f.ID).Type<NonNullType<IntType>>();
        descriptor.Field(f => f.FirstName).Type<StringType>();
        descriptor.Field(f => f.LastName).Type<StringType>();
        descriptor.Field(f => f.Age).Type<IntType>();
    }
}