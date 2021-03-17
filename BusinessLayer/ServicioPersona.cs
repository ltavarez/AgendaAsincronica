using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.ViewModel;
using Database;
using System.Data.Entity;

namespace BusinessLayer
{
    public class ServicioPersona : IServicio<Persona>
    {
        private AgendaContext context;

        public ServicioPersona()
        {
            context = new AgendaContext();
        }

        public async Task<bool> Add(Persona item)
        {
            try
            {
                context.Persona.Add(item);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await context.Persona.FirstOrDefaultAsync(c => c.Id == id);

                if(entity == null)
                {
                    return false;
                }

                context.Persona.Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Edit(Persona item)
        {
            try
            {
                var entity = await context.Persona.Include(x=> x.TipoContacto).FirstOrDefaultAsync(c => c.Id == item.Id);

                if (entity == null)
                {
                    return false;
                }

                context.Entry(entity).CurrentValues.SetValues(item);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Persona>> GetAll()
        {
            return await context.Persona.ToListAsync();
        }

        public async Task<List<PersonaViewModel>> GetAllViewModel()
        {
            return await context.Persona.Select(s => new PersonaViewModel {
                Id = s.Id,
                Nombre = s.Nombre,
                Apellido = s.Apellido,
                Telefono = s.Telefono,
                TipoDeContacto = s.TipoContacto.Name
            }).ToListAsync();
        }

        public async Task<Persona> GetById(int id)
        {
            return await context.Persona.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
