//------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using Ucu.Poo.PipesAndFilters.Filters;
using Ucu.Poo.PipesAndFilters.Pipes;

namespace Ucu.Poo.PipesAndFilters
{
    /// <summary>
    /// Programa principal.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Punto de entrada al programa principal.
        /// </summary>
        public static void Main()
        {
            IPipe pipeNull = new PipeNull();
            IPipe pipeSerial2 = new PipeSerial(new FilterNegative(), pipeNull);
            IPipe pipeSerial1 = new PipeSerial(new FilterGrayscale(), pipeSerial2);

            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"luke.jpg");

            IPicture result = pipeSerial1.Send(picture);
            provider.SavePicture(result, @"result.jpg");

            //Ejercicio 2
            //Se crea un filtro que guarda la imagen en un archivo y se agrega a la cadena de pipes en el medio para que guarde la imagen con la escala de grises.
            IFilter saveFilter = new FilterSave(@"result2.jpg");
            PipeSerial pipeSerial3 = new PipeSerial(saveFilter, pipeSerial2);
            pipeSerial1 = new PipeSerial(new FilterGrayscale(), pipeSerial3);
            result = pipeSerial1.Send(picture);
            provider.SavePicture(result, @"result3.jpg");

            //Ejercicio 3
            IFilter filterDiscord = new FilterDiscord();
            IPipe discordPipe = new PipeSerial(filterDiscord, pipeNull);
            discordPipe.Send(picture);
        }
    }
}