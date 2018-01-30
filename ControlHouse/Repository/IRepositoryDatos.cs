using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlHouse.Repository
{
    public interface IRepository<T>
    {
        void Grabar(T datos);
        void Actualizar(T datos);
        void Eliminar(int id);

        T Devolver(int? id);
        List<T> Listar();


    }

   
}
