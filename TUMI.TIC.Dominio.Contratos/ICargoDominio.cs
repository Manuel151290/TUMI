﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUMI.TIC.Modelo.Entidades;

namespace TUMI.TIC.Dominio.Contratos
{
    public interface ICargoDominio
    {
        List<Cargo> ListarTodo();
    }
}