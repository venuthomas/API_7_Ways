using vT.ApiGraphQL.Models;

namespace vT.ApiGraphQL.Types;

public class UpdatedType: ObjectType<UpdateResponse>
{
    protected override void Configure(IObjectTypeDescriptor<UpdateResponse> descriptor)
    {
        descriptor.Field(f => f.updatedSuccessfully).Type<NonNullType<BooleanType>>();
    }
}