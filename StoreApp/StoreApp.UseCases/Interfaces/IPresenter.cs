using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.UseCases.Interfaces
{
    public interface IPresenter<TInput, TOutput>
    {
        public IEnumerable<TOutput> Present(IEnumerable<TInput> data);
        public TOutput Present(TInput data);
    }
}
