using Demo2.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Dapper;  
using Demo2.Models;
using MediatR;

namespace Demo2.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly DapperContext _context;

        public GetAllProductsHandler(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT Id, Name, Price FROM Products";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Product>(query); 
        }
    }
}
