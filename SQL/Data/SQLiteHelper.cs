using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQL.Models;
using System.Threading.Tasks;

namespace SQL.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath) 
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Persona>().Wait();
        }

        public Task <int> SavePersonaAsync(Persona person) 
        {
            if (person.IdPersona != 0)
            {
                return db.UpdateAsync(person);
            }
            else
            {
                return db.InsertAsync(person);
            }
        } 

        public Task<int> DeletePersonaAsync(Persona persona)
        {
            return db.DeleteAsync(persona);
        }

        /// <summary>
        /// Recuperar todas las personas
        /// </summary>
        /// <returns></returns>
        public Task<List<Persona>> GetAllPersonaAsync()
        {
            return db.Table<Persona>().ToListAsync();
        }
        /// <summary>
        /// Recupera persona por id
        /// </summary>
        /// <param name="idPersona">Id de la persona que se requiere</param>
        /// <returns></returns>
        public Task<Persona> GetPersonaByIdAsync (int idPersona)
        {
            return db.Table<Persona>().Where(a => a.IdPersona == idPersona).FirstOrDefaultAsync();
        }
    }
}
