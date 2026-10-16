using System;
using System.Drawing;
using Ucu.Poo.Cognitive;

namespace Ucu.Poo.PipesAndFilters.Filters
{
    public class FilterCognitive : IFilterConditional
    {
        // Private set es porque la clase define por si misma en la logica que valor tiene el Result.
        public bool Result { get; private set; }
        private CognitiveFace faceApi;
        public FilterCognitive()
        {
            string subscriptionKey = Environment.GetEnvironmentVariable("AZURE_FACE_SUBSCRIPTION_KEY");
            if (string.IsNullOrWhiteSpace(subscriptionKey))
            {
                Console.WriteLine("Falta la variable de entorno AZURE_FACE_SUBSCRIPTION_KEY'.");
                return;
            }

            this.faceApi = new CognitiveFace();
        }

        public IPicture Filter(IPicture image)
        {
            PictureProvider provider = new PictureProvider();
            provider.SavePicture(image, @"imagen.png");
            CognitiveFace.RecognitionResult result = faceApi.Recognize(@"imagen.png", true);
            if (result.Success && result.FaceFound) // Recognize procesó la imagen y encontró una cara
            {
                this.Result = true;
                return provider.GetPicture(@"imagen_r_.png"); // Recognize agrega '_r_' como sufijo al archivo
            }
            else // Recognize no pudo procesar la imagen.
            {
                this.Result = false;
                return image;
            }
        }
    }
}