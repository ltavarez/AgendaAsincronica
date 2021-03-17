using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using System.Data.Entity;

namespace BusinessLayer
{
    public class ServicioTipoContacto : IServicio<TipoContacto>
    {
        private AgendaContext context;

        public ServicioTipoContacto()
        {
            context = new AgendaContext();
        }

        public Task<bool> Add(TipoContacto item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(TipoContacto item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TipoContacto>> GetAll()
        {
            return await context.TipoContacto.ToListAsync();
        }

        public async Task<TipoContacto> GetById(int id)
        {
            return await context.TipoContacto.FirstOrDefaultAsync(c=> c.Id == id);
        }
    }
}
