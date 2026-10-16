using System;

namespace Ucu.Poo.PipesAndFilters
{
    public interface IFilterConditional : IFilter
    {
        bool Result { get; }
    }
}