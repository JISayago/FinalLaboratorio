﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicios.Core.Cliente.DTO;

namespace XCommerce.Servicios.Core.Cliente
{
    public class ClienteServicio : IClienteServicio
    {
        public void Eliminar(long clienteId)
        {
            using (var baseDatos = new ModeloXCommerceContainer())
            {
                var clienteEliminar = baseDatos.Personas.OfType<AccesoDatos.Cliente>()
                    .FirstOrDefault(x => x.Id == clienteId);

                if (clienteEliminar == null || clienteEliminar.EstaEliminado) throw new Exception("No se encontro el Cliente");

                clienteEliminar.EstaEliminado = true;


                baseDatos.SaveChanges();
            }
        }

        public long Insertar(ClienteDTO clienteDto)
        {
            using (var baseDatos = new ModeloXCommerceContainer())
            {
                var nuevoCliente = new AccesoDatos.Cliente
                {
                    MontoMaximoCtaCte = clienteDto.MontoMaximoCtaCte,
                    Apellido = clienteDto.Apellido,
                    Nombre = clienteDto.Nombre,
                    Dni = clienteDto.Dni,
                    Telefono = clienteDto.Telefono,
                    Celular = clienteDto.Celular,
                    Email = clienteDto.Email,
                    Cuil = clienteDto.Cuil,
                    FechaNacimiento = clienteDto.FechaNacimiento,
                    Foto = clienteDto.Foto,
                    Direccion = new Direccion
                    {
                        Calle = clienteDto.Calle,
                        Numero = clienteDto.Numero,
                        Piso = clienteDto.Piso,
                        Dpto = clienteDto.Dpto,
                        Casa = clienteDto.Casa,
                        Lote = clienteDto.Lote,
                        Barrio = clienteDto.Barrio,
                        Mza = clienteDto.Mza,
                        LocalidadId = clienteDto.LocalidadId
                    }
                };


                baseDatos.Personas.Add(nuevoCliente);

                baseDatos.SaveChanges();

                return nuevoCliente.Id;
            }
        }

        public void Modificar(ClienteDTO clienteDto)
        {
            using (var baseDatos = new ModeloXCommerceContainer())
            {
                var clienteModificar = baseDatos.Personas.OfType<AccesoDatos.Cliente>()
                    .Include(x => x.Direccion)
                    .FirstOrDefault(x => x.Id == clienteDto.Id);

                if (clienteModificar == null)
                    throw new Exception("No se encontro el Cliente");

                clienteModificar.MontoMaximoCtaCte = clienteDto.MontoMaximoCtaCte;
                clienteModificar.Apellido = clienteDto.Apellido;
                clienteModificar.Nombre = clienteDto.Nombre;
                clienteModificar.Dni = clienteDto.Dni;
                clienteModificar.Telefono = clienteDto.Telefono;
                clienteModificar.Celular = clienteDto.Celular;
                clienteModificar.Email = clienteDto.Email;
                clienteModificar.Cuil = clienteDto.Cuil;
                clienteModificar.FechaNacimiento = clienteDto.FechaNacimiento;
                clienteModificar.Foto = clienteDto.Foto;

                clienteModificar.Direccion.Calle = clienteDto.Calle;
                clienteModificar.Direccion.Numero = clienteDto.Numero;
                clienteModificar.Direccion.Piso = clienteDto.Piso;
                clienteModificar.Direccion.Dpto = clienteDto.Dpto;
                clienteModificar.Direccion.Casa = clienteDto.Casa;
                clienteModificar.Direccion.Lote = clienteDto.Lote;
                clienteModificar.Direccion.Barrio = clienteDto.Barrio;
                clienteModificar.Direccion.Mza = clienteDto.Mza;
                clienteModificar.Direccion.LocalidadId = clienteDto.LocalidadId;

                baseDatos.SaveChanges();
            }
        }

        public IEnumerable<ClienteDTO> ObtenerCliente(string cadenaBuscar)
        {
            using (var baseDatos = new ModeloXCommerceContainer())
            {
                return baseDatos.Personas.OfType<AccesoDatos.Cliente>()
                    .AsNoTracking()
                    .Include(x => x.Direccion)
                    .Include(x => x.Direccion.Localidad)
                    .Where(x => !x.EstaEliminado && (x.Nombre.Contains(cadenaBuscar)
                                || x.Apellido.Contains(cadenaBuscar)
                                || x.Dni == (cadenaBuscar)
                                || x.Email == (cadenaBuscar)))

                    .Select(x => new ClienteDTO
                    {
                        Id = x.Id,
                        MontoMaximoCtaCte = x.MontoMaximoCtaCte,
                        Apellido = x.Apellido,
                        Nombre = x.Nombre,
                        Dni = x.Dni,
                        Telefono = x.Telefono,
                        Celular = x.Celular,
                        Email = x.Email,
                        Cuil = x.Cuil,
                        FechaNacimiento = x.FechaNacimiento,
                        Foto = x.Foto,
                        EstaEliminado = x.EstaEliminado,
                        Calle = x.Direccion.Calle,
                        Numero = x.Direccion.Numero,
                        Piso = x.Direccion.Piso,
                        Dpto = x.Direccion.Dpto,
                        Casa = x.Direccion.Casa,
                        Lote = x.Direccion.Lote,
                        Barrio = x.Direccion.Barrio,
                        Mza = x.Direccion.Mza,
                        LocalidadId = x.Direccion.LocalidadId,
                        ProvinciaId = x.Direccion.Localidad.ProvinciaId
                    }).ToList();
            }
        }

        public IEnumerable<ClienteDTO> ObtenerClienteEliminado(string cadenaBuscar)
        {
            using (var baseDatos = new ModeloXCommerceContainer())
            {
                return baseDatos.Personas.OfType<AccesoDatos.Cliente>()
                    .AsNoTracking()
                    .Include(x => x.Direccion)
                    .Include(x => x.Direccion.Localidad)
                    .Where(x => x.EstaEliminado && (x.Nombre.Contains(cadenaBuscar)
                                || x.Apellido.Contains(cadenaBuscar)
                                || x.Dni == (cadenaBuscar)
                                || x.Email == (cadenaBuscar)))

                    .Select(x => new ClienteDTO
                    {
                        Id = x.Id,
                        MontoMaximoCtaCte = x.MontoMaximoCtaCte,
                        Apellido = x.Apellido,
                        Nombre = x.Nombre,
                        Dni = x.Dni,
                        Telefono = x.Telefono,
                        Celular = x.Celular,
                        Email = x.Email,
                        Cuil = x.Cuil,
                        FechaNacimiento = x.FechaNacimiento,
                        Foto = x.Foto,
                        EstaEliminado = x.EstaEliminado,
                        Calle = x.Direccion.Calle,
                        Numero = x.Direccion.Numero,
                        Piso = x.Direccion.Piso,
                        Dpto = x.Direccion.Dpto,
                        Casa = x.Direccion.Casa,
                        Lote = x.Direccion.Lote,
                        Barrio = x.Direccion.Barrio,
                        Mza = x.Direccion.Mza,
                        LocalidadId = x.Direccion.LocalidadId,
                        ProvinciaId = x.Direccion.Localidad.ProvinciaId
                    }).ToList();
            }
        }

        public ClienteDTO ObtenerClientePorId(long clienteId)
        {
            using (var baseDatos = new ModeloXCommerceContainer())
            {
                return baseDatos.Personas.OfType<AccesoDatos.Cliente>()
                      .AsNoTracking()
                      .Where(x => !x.EstaEliminado)
                      .Include(x => x.Direccion)
                      .Include(x => x.Direccion.Localidad)
                      .Select(x => new ClienteDTO
                      {
                          Id = x.Id,
                          MontoMaximoCtaCte = x.MontoMaximoCtaCte,
                          Apellido = x.Apellido,
                          Nombre = x.Nombre,
                          Dni = x.Dni,
                          Telefono = x.Telefono,
                          Celular = x.Celular,
                          Email = x.Email,
                          Cuil = x.Cuil,
                          FechaNacimiento = x.FechaNacimiento,
                          Foto = x.Foto,
                          EstaEliminado = x.EstaEliminado,
                          Calle = x.Direccion.Calle,
                          Numero = x.Direccion.Numero,
                          Piso = x.Direccion.Piso,
                          Dpto = x.Direccion.Dpto,
                          Casa = x.Direccion.Casa,
                          Lote = x.Direccion.Lote,
                          Barrio = x.Direccion.Barrio,
                          Mza = x.Direccion.Mza,
                          LocalidadId = x.Direccion.LocalidadId,
                          ProvinciaId = x.Direccion.Localidad.ProvinciaId
                      }
                      ).FirstOrDefault(x => x.Id == clienteId);
            }
        }
    }
}
