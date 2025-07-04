using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial3_sumaran.Models;

namespace Parcial3_sumaran.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly GestionAcademicaContext _context;

        public CursoController(GestionAcademicaContext context)
        {
            _context = context;
        }

        // GET: api/cursos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
        {
            var cursos = await _context.Cursos.ToListAsync();
            return Ok(new
            {
                mensaje = "Lista de cursos obtenida exitosamente.",
                datos = cursos
            });
        }

        // GET: api/cursos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró el curso con ID {id}."
                });
            }

            return Ok(new
            {
                mensaje = "Curso obtenido exitosamente.",
                datos = curso
            });
        }

        // GET: api/cursos/ciclo/{ciclo}
        [HttpGet("ciclo/{ciclo}")]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursosPorCiclo(string ciclo)
        {
            var cursos = await _context.Cursos
                .Where(c => c.Ciclo == ciclo)
                .ToListAsync();

            if (cursos.Count == 0)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontraron cursos para el ciclo '{ciclo}'."
                });
            }

            return Ok(new
            {
                mensaje = $"Cursos del ciclo '{ciclo}' obtenidos exitosamente.",
                datos = cursos
            });
        }

        // POST: api/cursos
        [HttpPost]
        public async Task<ActionResult<Curso>> CrearCurso([FromBody] Curso curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCurso), new { id = curso.Id }, new
            {
                mensaje = "Curso creado exitosamente.",
                datos = curso
            });
        }

        // PUT: api/cursos/Actualizar/{id}
        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> ActualizarCurso(int id, Curso curso)
        {
            if (id != curso.Id)
            {
                return BadRequest(new
                {
                    mensaje = "El ID del curso no coincide."
                });
            }

            var cursoExistente = await _context.Cursos.FindAsync(id);
            if (cursoExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "Curso no encontrado."
                });
            }

            try
            {
                cursoExistente.Curso1 = curso.Curso1;
                cursoExistente.Creditos = curso.Creditos;
                cursoExistente.HorasSemanal = curso.HorasSemanal;
                cursoExistente.Ciclo = curso.Ciclo;
                cursoExistente.IdDocente = curso.IdDocente;

                _context.Entry(cursoExistente).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    mensaje = "Curso actualizado exitosamente.",
                    datos = cursoExistente
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cursos.Any(e => e.Id == id))
                {
                    return NotFound(new
                    {
                        mensaje = "Curso no encontrado."
                    });
                }
                else
                {
                    throw;
                }
            }
        }


        // DELETE: api/cursos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró el curso con ID {id}."
                });
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Curso eliminado exitosamente."
            });
        }
    }
}
