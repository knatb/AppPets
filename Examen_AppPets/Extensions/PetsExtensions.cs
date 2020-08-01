using Examen_AppPets.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Examen_AppPets.Extensions
{
    public static class PetsExtensions
    {
        //parametro opcional
        //extension para un task para poderlo invocar sin necesidad de la palabra await
        //recibe el parametro task que por default lo recibe
        //recibe un booleano para decirle si regreso al contexto
        //recibo una acción si se desea que se ejecute cuando hay una excepcion
        public static async void SafeFileAndForget(this Task pet,
                                                   bool returnToCallingContext,
                                                   Action<Exception> onException = null)
        {
            try
            {
                await pet.ConfigureAwait(returnToCallingContext);
            }
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
}
