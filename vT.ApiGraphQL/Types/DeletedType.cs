using vT.ApiGraphQL.Models;

namespace vT.ApiGraphQL.Types;

public class DeletedType : ObjectType<DeleteResponse>
{
    protected override void Configure(IObjectTypeDescriptor<DeleteResponse> descriptor)
    {
        descriptor.Field(f => f.deletedSuccessfully).Type<NonNullType<BooleanType>>();
    }
}