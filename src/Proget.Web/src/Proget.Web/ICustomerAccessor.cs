namespace Proget.Web;

public interface ICustomerAccessor<TId> where TId : struct, IParsable<TId>
{
    TId? GetFromHeader(string headerName);
    TId? GetFromToken(string token);
}