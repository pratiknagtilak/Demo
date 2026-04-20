using MediatR;

namespace Demo2.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>

    {
        public int Id { get; set; }
    
    }
}
