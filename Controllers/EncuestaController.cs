using EncuestaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EncuestaApi.Controllers
{
    public class EncuestaController : ApiController
    {
        EncuestaModel EncuestaData = new EncuestaModel();
        [HttpGet]
        [Route("api/encuesta/ObtenerPreguntas")]
        public IHttpActionResult Marcas()
        {
            var listaPreguntas = EncuestaData.preguntas.ToList();
            return Ok(listaPreguntas);
        }

        [HttpPost]
        [Route("api/encuesta/RegistrarCalificacion/{sucursal}")]
        public IHttpActionResult RegistrarCalificacion(List<encuesta_Detalle> nuevaCalificacion, int sucursal)
        {
            encuesta_header header = new encuesta_header();
            header.Fecha = DateTime.Today;
            header.Sucursal = sucursal;
            EncuestaData.encuesta_header.Add(header);
            EncuestaData.SaveChanges();
            foreach (encuesta_Detalle calificacion in nuevaCalificacion)
            {
                //var headerID = EncuestaData.encuesta_header.FirstOrDefault(e => e.Fecha == DateTime.Today && e.Sucursal == calificacion.Sucursal).id;
                encuesta_Detalle encuesta = new encuesta_Detalle();
                encuesta.idPregunta = calificacion.idPregunta;
                encuesta.Sucursal = calificacion.Sucursal;
                encuesta.Valoracion = calificacion.Valoracion;
                encuesta.Fecha = DateTime.Today;
                encuesta.Comentario = calificacion.Comentario;
                encuesta.idHeader = header.id;
                EncuestaData.encuesta_Detalle.Add(encuesta);
            }
            EncuestaData.SaveChanges();

            return Ok();

        }

        [HttpGet]
        [Route("api/encuesta/ObtenerCantPreguntas/{sucursal}")]
        public IHttpActionResult ObtenerCantPreguntas(int sucursal)
        {
            //var listaPreguntas = EncuestaData.encuesta_header
            //    .Where(eh => eh.Fecha == DateTime.Today && eh.Sucursal == sucursal)
            //    .ToList().Count();
            var contadorPreguntas = EncuestaData.Database
                .SqlQuery<Contador>(@"select count(eh.id) ContadorPreguntas, eh.Sucursal
                                    from encuesta_header eh"
                                    + @" where  DATEPART(ISO_WEEK, CAST( GETDATE() AS Date ))= DATEPART(ISO_WEEK, eh.Fecha)
                                    and eh.Sucursal = " + sucursal + " group by eh.Sucursal");

            return Ok(contadorPreguntas);
        }

        [HttpGet]
        [Route("api/encuesta/ObtenerPuntaje/{sucursal}/{fechaInicio}/{fechaFinal}")]
        public IHttpActionResult ObtenerPuntaje(int sucursal, DateTime fechaInicio, DateTime fechaFinal)
        {

            var Valoraciones = (from Puntajes in EncuestaData.encuesta_Detalle
                                join p in EncuestaData.preguntas on Puntajes.idPregunta equals p.id
                                where Puntajes.Fecha >= fechaInicio && Puntajes.Fecha <= fechaFinal
                                && Puntajes.Sucursal == sucursal
                                orderby Puntajes.Fecha descending
                                select new
                                {
                                    Nombre = p.Nombre,
                                    Valoracion = Puntajes.Valoracion,
                                    Fecha = Puntajes.Fecha,
                                    Cliente = Puntajes.idHeader,
                                    id = Puntajes.id,
                                    Sucursal = Puntajes.Sucursal,
                                    Comentario = Puntajes.Comentario,
                                    idPregunta = p.id

                                }).ToList();




            return Ok(Valoraciones);
        }

        [HttpPut]
        [Route("api/encuesta/updateEncuesta/{id}/{valoracion}")]
        public IHttpActionResult UpdateCalificacion(int id, int valoracion)

        {
            var encuestaEditar = EncuestaData.encuesta_Detalle.FirstOrDefault(e => e.id == id);
            encuestaEditar.Valoracion = valoracion;
            EncuestaData.SaveChanges();

            return Ok();
        }
    }
    }
 