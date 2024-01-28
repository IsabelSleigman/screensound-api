using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Service;
using ScreenSound.Shared.Modelos.Entities;

namespace ScreenSound.API.Endpoints
{
    public static class GeneroExtensions
    {
        #region Endpoint Generos
        public static void AddEndPointGeneros(this WebApplication app)
        {
            app.MapPost("/Generos", ([FromServices] ServiceBase<Genero> service, [FromBody] GeneroRequest generoReq) =>
            {
                service.Adicionar(RequestToEntity(generoReq));
            });


            app.MapGet("/Generos", ([FromServices] ServiceBase<Genero> service) =>
            {
                return EntityListToResponseList(service.Listar());
            });

            app.MapGet("/Generos/{nome}", ([FromServices] ServiceBase<Genero> service, string nome) =>
            {
                var genero = service.BuscarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (genero is not null)
                {
                    var response = EntityToResponse(genero!);
                    return Results.Ok(response);
                }
                return Results.NotFound("Gênero não encontrado.");
            });

            app.MapDelete("/Generos/{id}", ([FromServices] ServiceBase<Genero> service, int id) =>
            {
                var genero = service.BuscarPor(a => a.Id == id);
                if (genero is null)
                {
                    return Results.NotFound("Gênero para exclusão não encontrado.");
                }
                service.Excluir(genero);
                return Results.NoContent();
            });
        }
        #endregion
        private static Genero RequestToEntity(GeneroRequest generoRequest)
        {
            return new Genero() { Nome = generoRequest.nome, Descricao = generoRequest.descricao };
        }

        private static ICollection<GeneroResponse> EntityListToResponseList(IEnumerable<Genero> generos)
        {
            return generos.Select(a => EntityToResponse(a)).ToList();
        }

        private static GeneroResponse EntityToResponse(Genero genero)
        {
            return new GeneroResponse(genero.Id, genero.Nome!, genero.Descricao!);
        }
    }
}
