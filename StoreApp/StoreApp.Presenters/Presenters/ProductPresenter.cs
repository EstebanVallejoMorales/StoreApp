using AutoMapper;
using StoreApp.Entities;
using StoreApp.Presenters.ViewModels;
using StoreApp.UseCases.Interfaces;

namespace StoreApp.Presenters.Presenters
{
    public class ProductPresenter : IPresenter<Product, ProductViewModel>
    {
        private readonly IMapper _mapper;

        public ProductPresenter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<ProductViewModel> Present(IEnumerable<Product> data)
        {
            IEnumerable<ProductViewModel> productViewModels = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(data);
            return productViewModels;
        }

        public ProductViewModel Present(Product data)
        {
            return _mapper.Map<Product, ProductViewModel>(data);
        }
    }
}
