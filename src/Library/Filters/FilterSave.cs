using System;
using System.Drawing;

namespace Ucu.Poo.PipesAndFilters.Filters
{
    /// <summary>
    /// Filtro que guarda la imagen en un archivo.
    /// </summary>
    public class FilterSave : IFilter
    {
        private PictureProvider p;
        private string path;
        public FilterSave(string path)
        {
            this.p = new PictureProvider();
            this.path = path;
        }
        public IPicture Filter (IPicture image)
        {
            this.p.SavePicture(image, this.path);
            return image;
        }
    }
}