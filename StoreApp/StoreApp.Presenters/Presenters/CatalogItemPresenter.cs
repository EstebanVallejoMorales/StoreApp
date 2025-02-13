using AutoMapper;
using StoreApp.Entities;
using StoreApp.Presenters.ViewModels;
using StoreApp.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Presenters.Presenters
{
    public class CatalogItemPresenter : IPresenter<CatalogItemViewModel, CatalogItemViewModel>
    {
        public CatalogItemPresenter()
        {
            
        }

        public IEnumerable<CatalogItemViewModel> Present(IEnumerable<CatalogItemViewModel> data)
        {
            return data;
        }
               
        public CatalogItemViewModel Present(CatalogItemViewModel data)
        {
            return data;
        }
    }
}
