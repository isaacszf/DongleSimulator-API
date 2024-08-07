using Shared.Exceptions;
using Shared.Exceptions.Base;
using Sqids;

namespace DongleSimulator.Application.Extensions;

public static class SqidsExtension
{
    public static long DecodeLong(this SqidsEncoder<long> sqids, string id)
    {
        try
        {
            return sqids.Decode(id).Single();
        }
        catch (InvalidOperationException)
        {
            throw new NotFoundException(ResourceExceptionMessages.ITEM_NOT_FOUND);
        }
    }
}