using Examen_AppPets.Extensions;
using Examen_AppPets.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_AppPets.Data
{
    public class PetsDatabase
    {
        //clase para que al conectar nuestra base de datos no se bloquee la app
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });
        //nos regresa la conexión de la base de datos SQLite
        static SQLiteAsyncConnection Database => lazyInitializer.Value;

        static bool initialized = false;

        public PetsDatabase()
        {
            InitializeAsync().SafeFileAndForget(false);
        }
       

        async Task InitializeAsync()
        {
            if (!initialized)
            {                                  //como setter
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(PetModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(PetModel)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }
        public Task<List<PetModel>> GetAllPetsAsync()
        {
            return Database.Table<PetModel>().ToListAsync();
        }

        public Task<List<PetModel>> GetPetsNotDoneAsync()
        {
            return Database.QueryAsync<PetModel>($"SELECT * FROM [{typeof(PetModel).Name}] WHERE [Done] = 0");
        }

        public Task<PetModel> GetPetAsync(int id)
        {
            return Database.Table<PetModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SavePetAsync(PetModel pet)
        {
            if (pet.ID != 0)
            {
                return Database.UpdateAsync(pet);
            }
            else
            {
                return Database.InsertAsync(pet);
            }
        }
        public Task<int> DeletePetAsync(PetModel pet)
        {
            return Database.DeleteAsync(pet);
        }
    }
}
