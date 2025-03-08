namespace Proget.Web.Customer;

internal sealed class CustomerAccessor<TId> : ICustomerAccessor<TId> where TId : struct, IParsable<TId>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomerAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public TId? GetFromHeader(string headerName)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            return null;
        }

        var result = httpContext.Request.Headers.TryGetValue(headerName, out var customerIdHeader);
        if (!result)
        {
            return null;
        }

        result = TId.TryParse(customerIdHeader, CultureInfo.InvariantCulture, out var customerId);
        if (!result)
        {
            return null;
        }

        return customerId;
    }

    public TId? GetFromToken(string token)
    {
        throw new NotImplementedException();
    }
}