using Demo2.Models;
using MediatR;

namespace Demo2.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
