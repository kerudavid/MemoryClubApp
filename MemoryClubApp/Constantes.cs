using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryClubApp
{
    public class Constantes
    {
        public string cliente = "Cliente";
        public string colaborador = "Colaborador";
        public bool HasInternet { get; set; }
        public string GetCliente()
        {
            return this.cliente;
        }
        public string GetColaborador()
        {
            return this.colaborador;
        }
    }


}
