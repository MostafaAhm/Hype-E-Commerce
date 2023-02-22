using Mapster;
using MapsterMapper;
using Product.Application.Contracts.Infrastructure;
using Product.Application.Contracts.Persistence;
using Product.Application.Resources.Product.Create;
using Product.Application.Resources.Product.Get;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Product.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateProductResponseDto> CreateProduct(CreateProductRequestDto dto)
        {
            var productExists = await _unitOfWork.Products.GetAsync(x => x.Name == dto.Name);

            if(productExists != null)
            {
                return (new CreateProductResponseDto
                {
                    ProductId = productExists.Id,
                    IsSuccess = false,
                    StatusCode = 400,
                    ResponseMessage = "Sorry, this product already exist"
                });
            }

            var product = dto.Adapt<Products>();
            var respose = await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return (new CreateProductResponseDto
            {
                ProductId = respose.Id,
                IsSuccess = true,
                StatusCode = 200,
                ResponseMessage = "Great, Product added successfully"
            });
        }

        public GetProductsResponseDto GetProducts(GetProductsRequestDto dto)
        {
            var data = _unitOfWork.Products.GetPagedListproducts(dto);

            if (data == null)
            {
                return (new GetProductsResponseDto
                {
                    Data = null,
                    IsSuccess= false,
                    StatusCode = 404,
                    ResponseMessage = "Previous request data was not found"
                });
            }

            return new GetProductsResponseDto
            {
                Data = _mapper.Map<List<ProductDto>>(data),
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK,
                ResponseMessage = "Request has been completed successfully"
            };
        }
    }
}
