using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Examen_AppPets.Data
{
    public static class Constants
    {
        //constante para abrir nuestro archivo en SQLite en modo lectura-escritura
        //crearlo si no existe y sea accesible en modo multihilo
        public const SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite |
                                             SQLite.SQLiteOpenFlags.Create |
                                             SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                //forma la ruta completa donde se guardara el archivo de sqlite
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, "Examen_AppPetsSQLite.db3");
            }
        }
    }
}
