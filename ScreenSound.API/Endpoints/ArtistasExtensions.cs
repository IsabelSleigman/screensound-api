using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Modelos;
using ScreenSound.Service;

namespace ScreenSound.API.Endpoints
{
    public static class ArtistasExtensions
    {
        public static void AddEndPointArtistas(this WebApplication app)
        {
            #region Endpoint Artistas
            app.MapGet("/Artistas", ([FromServices] ServiceBase<Artista> service) =>
            {
                var listaDeArtistas = service.Listar();
                if (listaDeArtistas is null)
                {
                    return Results.NotFound();
                }
                var listaDeArtistaResponse = EntityListToResponseList(listaDeArtistas);
                return Results.Ok(listaDeArtistaResponse);
            });

            app.MapGet("/Artistas/{nome}", ([FromServices] ServiceBase<Artista> service, string nome) =>
            {
                var artista = service.BuscarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));

                if (artista is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(EntityToResponse(artista));
            });

            app.MapPost("/Artistas", ([FromServices] ServiceBase<Artista> service, [FromBody] ArtistaRequest artistaRequest) =>
            {
                var artista = new Artista(artistaRequest.nome, artistaRequest.bio);
                service.Adicionar(artista);
                return Results.Ok();
            });

            app.MapDelete("/Artistas/{id}", ([FromServices] ServiceBase<Artista> service, int id) =>
            {
                var artista = service.BuscarPor(a => a.Id == id);

                if (artista is null)
                {
                    return Results.NotFound();
                }
                service.Excluir(artista);
                return Results.NoContent();
            });

            app.MapPut("/Artistas", ([FromServices] ServiceBase<Artista> service, [FromBody] ArtistaRequestEdit artistaRequestEdit) =>
            {
                var artistaParaAtualizar = service.BuscarPor(a => a.Id == artistaRequestEdit.Id);

                if (artistaParaAtualizar is null)
                {
                    return Results.NotFound();
                }
                artistaParaAtualizar.Nome = artistaRequestEdit.nome;
                artistaParaAtualizar.Bio = artistaRequestEdit.bio;

                service.Editar(artistaParaAtualizar);
                return Results.Ok();
            });
            #endregion

            static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
            {
                return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
            }

            static ArtistaResponse EntityToResponse(Artista artista)
            {
                return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
            }
        }
    }
}
